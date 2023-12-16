using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateController : ParentObstacleController
{
    public enum ObType
    {
        Rotate,
        Rolling,
        Swing,
        RotateDeltaAxis
    }
    public enum ObDirection
    {
        Left,
        Right,
        Diagonal_Right_Up,
        Diagonal_Left_Up
    }
    [SerializeField] private ObDirection obDirection;
    [SerializeField] private ObType obType;
    [SerializeField] private float rotateSpeed=10000f;
    [SerializeField] private float rollingSpeed=11f;
    [SerializeField] private float gravityScale=4f;
    /// <summary>
    /// Swing용 변수
    /// </summary>
    /// <returns></returns>
    
    // 각도
    [SerializeField] private float angle = 30f;
    // 보간에 사용되는 시간
    private float lerpTime = 0;
    // 스윙 속도
    private float speed = 2f;
    /// <summary>
    /// RotateDeltaAxis
    /// </summary>
    /// <returns></returns>
    /// 
    [SerializeField] private float rotateDelta;
    [SerializeField] private Vector3 point;
    [SerializeField] private Collider2D collisionCollider;
    public override IEnumerator Activate()
    {
        switch (obType)
        {
            case ObType.Swing:
                StartCoroutine(Swing());
                break;
            case ObType.Rotate:
                StartCoroutine(Rotate());
                break;
            case ObType.Rolling:
                StartCoroutine(Rolling(obDirection,rollingSpeed,gravityScale));
                break;
            case ObType.RotateDeltaAxis:
                StartCoroutine(RotateDeltaAxis());
                break;
        }
        yield return base.Activate(); // 부모 클래스의 Activate 메서드 실행 
                                      //사실 ismoving과 별개로 움직이기 때문에 이걸 굳이 부모 activate를 실행하지 않아도 되지만 후에 test할때를 위해 그냥 실행하겠음. 
    }

    private void Awake()
    {
        base.Awake();
    }

    IEnumerator Rotate()
    {
        while (true)
        {
            this.gameObject.transform.Rotate(0, 0, -Time.deltaTime * rotateSpeed, Space.Self);
            yield return null;
        }

    }
    IEnumerator Rolling(ObDirection obDirection, float rollingSpeed, float gravityScale)
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        if (rigid != null)
        {
            rigid.bodyType = RigidbodyType2D.Dynamic;
            rigid.gravityScale = gravityScale;
        }

        switch (obDirection)
        {
            case ObDirection.Left:
                rigid.velocity = new Vector3(-rollingSpeed, 0, 0);
                break;
            case ObDirection.Right:
                rigid.velocity = new Vector3(rollingSpeed, 0, 0);
                break;
            case ObDirection.Diagonal_Left_Up:
                rigid.AddForce(new Vector2(-1, 1) * rollingSpeed, ForceMode2D.Impulse);
                break;
            case ObDirection.Diagonal_Right_Up:
                rigid.AddForce(new Vector2(1, 1) * rollingSpeed, ForceMode2D.Impulse);
                break;
        }
        yield return null;
    }
    IEnumerator Swing()
    {
        while (true)
        {
            // lerpTime을 시간에 따라 증가시켜서 보간을 계산
            lerpTime += Time.deltaTime * speed;

            // 스윙 동작 계산 후, 객체의 회전을 업데이트
            transform.rotation = CalculateMovementOfPendulum();
            yield return null;
        }
        
    }
    // 스윙 동작의 회전 값을 계산하는 메서드
    Quaternion CalculateMovementOfPendulum()
    {
        // 전방 각도에서 후방 각도로의 보간을 계산하고 반환
        return Quaternion.Lerp(Quaternion.Euler(Vector3.forward * angle),
                               Quaternion.Euler(Vector3.back * angle),
                               GetLerpTParam());
    }

    // 보간에 사용될 t 매개변수 값을 계산하는 메서드
    private float GetLerpTParam()
    {
        // Mathf.Sin 함수를 사용하여 부드러운 보간 효과를 만듦
        // lerpTime에 따라 Sin 값을 계산하고, 0에서 1 사이의 값으로 변환
        return (Mathf.Sin(lerpTime) + 1) * 0.5f;
    }
    /// <summary>
    /// ex) a_Tree2
    /// 트리거가 감지되면 현재위치에서 point를 더한 위치를 기준으로 rotateDelta만큼 회전
    /// </summary>
    /// <returns></returns>
    IEnumerator RotateDeltaAxis()
    {
        collisionCollider.enabled = true;
        transform.RotateAround(transform.position + point, Vector3.forward, rotateDelta);
        yield return null;
    }
}
