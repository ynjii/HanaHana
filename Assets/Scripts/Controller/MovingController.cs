using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingController : ParentObstacleController
{
    public enum ObType
    {
        Move, //단방향으로 움직임
        MoveSide, //왔다갔다
        DrawingLine,//꼭짓점 위치로 선을 그리며 이동
        MoveToTarget, //특정 위치를 받아와서 해당 위치로 이동 shake랑 같이 쓰면 잘 안된다. shake는 위아래값이 고정되어 잇기 때문. 후에 shake도 수정해야할듯.
        BlowAway//닿으면 플레이어 날려버림
    }

    public enum ObDirection
    {
        still, //안움직임
        /// <summary>
        /// Up, Down, Left, Right는 simpleMove용
        /// </summary>
        Up,
        Down,
        Left,
        Right,

        /// <summary>
        /// UpDown, LeftRight은 moveSide랑 Shake용
        /// </summary>
        UpDown,
        LeftRight
    }

    [SerializeField]
    private ObType obType;

    [SerializeField]
    private ObDirection obDirection;

    [SerializeField]
    private float distance = 0f; //움직일 거리

    [SerializeField]
    private float speed = 11f; //속도

    private float movedDistance = 0f;

    private Vector3 initialPosition; //움직인 거리를 재기 위해 사용

    [SerializeField]
    private Transform _transform; //이건 moveto target에서도 사용된다. => 이동시키고 싶은 위치

    /// <summary>
    /// DrawingLine변수들
    /// </summary>
    [SerializeField] LineRenderer line;
    [SerializeField] bool repeatLine = false;//라인을 반복해서 움직이는지, 라인의 끝 꼭짓점 가면 끝나는지

    /// <summary>
    /// BlowAway변수들
    /// </summary>
    private GameObject player; //날아갈 player, collider 충돌 시에 받아온다.
    
    public override IEnumerator Activate()
    {
        switch (obType)
        {
            case ObType.Move:
                StartCoroutine(Move());
                break;
            case ObType.DrawingLine:
                StartCoroutine(DrawingLine(line, speed, _transform));
                break;
            case ObType.MoveToTarget:
                StartCoroutine(MoveToTarget(_transform));
                break;
            case ObType.BlowAway:
                BlowAway(obDirection);
                break;
        }
        yield return base.Activate(); // 부모 클래스의 Activate 메서드 실행 
        //사실 ismoving과 별개로 움직이기 때문에 이걸 굳이 부모 activate를 실행하지 않아도 되지만 후에 test할때를 위해 그냥 실행하겠음. 
    }

    private IEnumerator Move()
    {
        initialPosition = transform.position;
        Vector3 targetPosition = CalculateTargetPosition(obDirection, distance);//움직이는 목표 지점을 계산한다

        while (movedDistance < distance)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            movedDistance = Vector3.Distance(initialPosition, newPosition);
            transform.position = newPosition;
            yield return null;
        }
    }

    //움직이는 목표 지점을 계산한다
    private Vector3 CalculateTargetPosition(ObDirection obDirection, float movement)
    {

        switch (obDirection)
        {
            case ObDirection.Up:
                return initialPosition + Vector3.up * movement;
            case ObDirection.Down:
                return initialPosition + Vector3.down * movement;
            case ObDirection.Left:
                return initialPosition + Vector3.left * movement;
            case ObDirection.Right:
                return initialPosition + Vector3.right * movement;
            default:
                return initialPosition;
        }
    }
    /// <summary>
    /// 찍은 꼭짓점대로 움직임. //유진
    /// 사용예시: SnowBoss4.cs 
    /// targetPositions: 꼭짓점 리스트
    /// transform: 움직일 오브젝트의 Transform
    /// moveSpeed: 움직일 스피드
    IEnumerator DrawingLine(LineRenderer line, float moveSpeed, Transform _transform)
    {
        //배열생성
        Vector3[] path = new Vector3[line.positionCount];
        for (int i = 0; i < line.positionCount; i++)
        {
            // 선의 꼭짓점 가져옴
            path[i] = line.GetPosition(i);
        }
        //시작점
        int currentTargetIndex = 0;
        Vector3 targetPosition = path[currentTargetIndex];

        while (true)
        {
            //타겟 꼭짓점과의 거리재기
            float distanceToTarget = Vector3.Distance(_transform.position, targetPosition);
            //도달했으면
            if (distanceToTarget <= 1.0f)
            {
                //마지막 꼭짓점 도달시 break
                if (!repeatLine)
                {
                    if (currentTargetIndex == path.Length - 1)
                    {
                        break;
                    }
                }

                //다음 꼭짓점으로 타겟변경
                currentTargetIndex = (currentTargetIndex + 1) % path.Length;
                targetPosition = path[currentTargetIndex];
            }
            //이 쪽으로 이동
            _transform.position = Vector3.MoveTowards(_transform.position, targetPosition, Time.deltaTime * moveSpeed);
            //_transform.position = new Vector3(_transform.position.x,_transform.position.y,-1f);
            yield return null;//deltaTime 기다리고 돌아감
        }
    }

    IEnumerator MoveToTarget(Transform tr)
    {
        while (Vector3.Distance(transform.position, tr.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, tr.position, speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
        }
    }

    private void BlowAway(ObDirection obDirection) 
    {
        Rigidbody2D player_rigid = player.GetComponent<Rigidbody2D>();
        switch (obDirection)
        {
            case ObDirection.Up:
                player_rigid.AddForce(new Vector2(0, speed));
                break;

            case ObDirection.Down:
                player_rigid.AddForce(new Vector2(0, -speed));
                break;

            case ObDirection.Left:
                player_rigid.AddForce(new Vector2(-speed, 0));
                break;

            case ObDirection.Right:
                player_rigid.AddForce(new Vector2(speed, 0));
                break;
        }
    }
    private void Awake()
    {
        base.Awake();
        initialPosition = transform.position;
    }
}
