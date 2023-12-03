using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingController : ParentObstacleController
{
    public enum ObType
    {
        Move, //단방향으로 움직임
        MoveSide, //왔다갔다
        MoveToPoints//꼭짓점 위치로 이동
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

    /// <summary>
    /// MoveToPoints변수들
    /// </summary>
    [SerializeField] LineRenderer line;
    [SerializeField] Transform _transform;
    [SerializeField] bool repeatLine = false;//라인을 반복해서 움직이는지, 라인의 끝 꼭짓점 가면 끝나는지

    // Start is called before the first frame update
    public override IEnumerator Activate()
    {
        switch (obType)
        {
            case ObType.Move:
                StartCoroutine(MoveToTarget());
                break;
            case ObType.MoveToPoints:
                StartCoroutine(MoveToPoints(line, speed, _transform));
                break;
        }
        yield return base.Activate(); // 부모 클래스의 Activate 메서드 실행 
        //사실 ismoving과 별개로 움직이기 때문에 이걸 굳이 부모 activate를 실행하지 않아도 되지만 후에 test할때를 위해 그냥 실행하겠음. 
    }

    private IEnumerator MoveToTarget()
    {
        Vector3 targetPosition = CalculateTargetPosition(obDirection, distance);

        while (movedDistance < distance)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            movedDistance = Vector3.Distance(initialPosition, newPosition);
            transform.position = newPosition;
            yield return null;
        }
    }

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
    IEnumerator MoveToPoints(LineRenderer line, float moveSpeed, Transform _transform)
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


    private void Awake()
    {
        base.Awake();
        initialPosition = transform.position;
    }
}
