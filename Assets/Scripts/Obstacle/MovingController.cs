using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingController : ParentObstacleController
{
    public enum ObType
    {
        Move, //단방향으로 움직임
        MoveSide //왔다갔다
    }

    public enum ObDirection//★ diagonal이 되는 부분이 있고 아닌 부분이 있음!! 이거 쓸 때 제대로 알고 씁시다. 설명문서에 예전에 추가해두긴 했는데 혹시나 까먹으셨을까봐.. 실수 방지 위해 설명문서 보고 사용하는 습관 들입시다!
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

    // Start is called before the first frame update
    public override IEnumerator Activate()
    {
        switch (obType)
        {
            case ObType.Move:
                StartCoroutine(MoveToTarget());
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

    private void Awake()
    {
        base.Awake();
        initialPosition = transform.position;
    }
}
