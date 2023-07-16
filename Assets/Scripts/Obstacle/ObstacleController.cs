using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 움직이는 obstacle들의 부모 스크립트입니다.
/// </summary>
public class ObstacleController : MonoBehaviour
{   
    //추후 define에 넣을 예정
    public enum ObType 
    {
        Move, //단방향으로 움직임
        MoveSide, //왔다갔다
        Rotate, //돌면서 떨어짐
        Floating, //둥둥 떠있는 모션
        ChangeStatus, //레이어랑 tag 바꿈
        ChangeColor //색깔 바꿈
    }

    public enum ObDirection
    {
        still, //안움직임
        Up,
        Down,
        Left,
        Right
    }
    
    [SerializeField]
    private ObType obType; //obstacle의 type을 inspector에서 받아옴.
    [SerializeField]
    private ObDirection obDirection; // direction을 inspector에서 받아옴. (필요할시)

    [SerializeField]
    private bool isCol = false; //트리거로 작동하는지 collision으로 작동하는지

    [SerializeField]
    private float waitingTime = 0f;

    [SerializeField]
    private float distance = 0f; //움직일 거리, obstacle마다 다를 것 같아서 일단은 이렇게 해뒀습니다. 
    
    [SerializeField]
    private float speed = 11f; //속도
    
    [SerializeField]
    private string tagName = "Untagged";

    [SerializeField]
    private int layerIndex = 0;
    
    private bool isMoving = false; //isTrigger 처리된 collider랑 부딪히면 true;
    private Vector3 initialPosition; //움직인 거리를 재기 위해 사용
    private Vector3 movement = Vector3.zero;
    
    private float movedDistance = 0f; 
    private bool moveRight = false; //moveside할때 사용됨

    private float floatingSpeed = 3f; //둥둥 떠다니는 속도
    private float floatingHeight = 0.1f; // 둥둥 떠다니는 높이

    private float rotateSpeed = 10000f;

    private Rigidbody2D rigid;
    private Tilemap tilemap;
    private PolygonCollider2D polygonCollider;


    /// <summary>
    /// isTriggered 처리가 된 collider와 부딪혔을때 
    /// 부딪혔다는 사실을 isTriggred로 알리고, tag도 부딪히면 죽게 Enemy로 바꿔줍니다.
    /// 만약 isTriggred 처리된 collider를 사용하고 싶다면 주의해서 사용해주셔야해요.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision) {
        if(!isCol && collision.gameObject.CompareTag("Player"))
        {   
            StartCoroutine(SetIsmoving(true));
        }
    }

    /// <summary>
    /// 트리거 없이 collisoin에 부딪혓을때 작동하는 경우.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision) {
        if(isCol && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SetIsmoving(true));
        }
    }   
    
    private void Awake(){
        initialPosition = transform.position;
        tilemap = GetComponent<Tilemap>();
        //트리거 없어도 계쏙 움직여야하는 애들은 시작할때 true 처리
        if(obType == ObType.Floating){
            isMoving = true;
        }
        else if(obType == ObType.MoveSide)
        {
            // rayhit 그릴때 사용
            polygonCollider = GetComponent<PolygonCollider2D>();
        }
    }

    private void Update() 
    {
        if(isMoving)
        {
            ObstacleMove(obType);
        }
    }


    private void ObstacleMove(ObType obType){
        switch (obType)  
        {
            case ObType.Move:
                SimpleMove(obDirection, speed);
                break;
            case ObType.MoveSide:
                MoveSide();
                break;
            case ObType.Rotate:
                Rotate();
                break;
            case ObType.Floating:
                StartCoroutine(FloatingCoroutine());
                break;
            case ObType.ChangeStatus:
                ChangeStatus(tagName, layerIndex);
                isMoving = false;
                break;
            case ObType.ChangeColor:
                tilemap.color = new Color(1, 1, 1, 0.8f); //투명하게 바꾸기. 추후 색깔바꿀일 있으면 수정.
                isMoving = false;
                break;
        }
    }
    /// <summary>
    /// 이제 주어진 방향, 속도, 거리만큼 obstacle이 움직입니다. 
    /// </summary>
    private void SimpleMove(ObDirection obDirection, float speed)
    {   
        if(movedDistance > distance){
            isMoving = false;
        }

        switch (obDirection)
        {   
            case ObDirection.still:
                break;
            case ObDirection.Up:
                movement = Vector3.up * speed * Time.deltaTime;
                break;
            case ObDirection.Down:
                movement = Vector3.down * speed * Time.deltaTime;
                break;
            case ObDirection.Left:
                movement = Vector3.left * speed * Time.deltaTime;
                break;
            case ObDirection.Right:
                movement = Vector3.right * speed * Time.deltaTime;
                break;
        }

        transform.position += movement;
        movedDistance = Vector3.Distance(initialPosition, transform.position);
    }

    /// <summary>
    /// 좌우로 왔다갔다 움직임
    /// </summary>
    private void MoveSide()
    {
        // 이동 방향 설정
        float moveDirection = moveRight ? 1f : -1f;

        movement = Vector3.right * moveDirection * speed * Time.deltaTime;
        transform.position += movement;

        // 지형체크
        Vector2 bottomEdge;
        
        if(moveRight) //오른쪽을 향하고 있다면
        {
            bottomEdge = new Vector2(polygonCollider.bounds.max.x, polygonCollider.bounds.min.y - 0.1f);
        }
        else 
        {
            bottomEdge = new Vector2(polygonCollider.bounds.min.x, polygonCollider.bounds.min.y - 0.1f);
        }


        Debug.DrawRay(bottomEdge, Vector2.down, Color.green);
        
        RaycastHit2D rayHit = Physics2D.Raycast(bottomEdge, Vector2.down, 0.5f, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            moveRight = !moveRight;
        }

    }

    /// <summary>
    /// 회전
    /// </summary>
    private void Rotate()
    {
        transform.Rotate(0, 0, -Time.deltaTime * rotateSpeed, Space.Self);     
    }

    /// <summary>
    /// 위아래로 움직이는
    /// </summary>
    private IEnumerator FloatingCoroutine()
    {
        while(true)
        {
            float newY = initialPosition.y + Mathf.Sin(Time.time * floatingSpeed) * floatingHeight;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }
    }

    //tag 와 layer을 변경
    private void ChangeStatus(string tagName, int layerIndex)
    {
        this.gameObject.tag = tagName;
        this.gameObject.layer = layerIndex;
    }

    IEnumerator SetIsmoving(bool n){
        yield return new WaitForSeconds(waitingTime);
        this.isMoving = n;
    }

    /// <summary>
    /// 화면 나가면 죽음
    /// 화면 나오면 살아남
    /// </summary>
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnBecameVisible() {
        gameObject.SetActive(true);
    }

}
