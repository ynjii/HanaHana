using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 터치방식을 바꾼걸 테스트했던 코드입니다
/// </summary>
public class PlayerTouchTest : MonoBehaviour
{
    /// <summary>
    /// 터치 범위 변수
    /// </summary>
    float rightButtonEnd= Screen.width * 0.4167f;
    float leftButtonEnd= Screen.width * 0.2083f;
    float jumpButtonEnd= Screen.width;
    //무한점프 막기위해 점프중인지 체크하는 변수
    bool isJumping = false;
    //리지드바디 받아오는 변수
    Rigidbody2D rigid;
    //최대속력, 최대점프력
    [SerializeField] private float maxSpeed=5f;
    [SerializeField] private float jumpPower=5f;
    // Start is called before the first frame update
    void Start()
    {
        //리지드바디 가져오기
        rigid=GetComponent<Rigidbody2D>();  
        //터치 범위 출력하는 디버깅코드
        Debug.Log("leftButton은 0~" + leftButtonEnd);
        Debug.Log("rightButton은"+leftButtonEnd+"~" + rightButtonEnd);
        Debug.Log("jumpButton은"+rightButtonEnd+"~" + jumpButtonEnd);
    }

    // Update is called once per frame
    void Update()
    {
        // 현재 발생 중인 모든 터치 정보 가져오기
        Touch[] touches = Input.touches;
        // 아래는 각 터치에 대한 처리
        //1. 브레이크
        if (Input.touchCount== 1|| Input.touchCount == 0) //손가락이 1개거나 없어야함
        {
            if (Input.touchCount == 1)//손가락이 있는 경우면
            {
                //그 손가락에 대한 정보 가져옴
                Touch touch=Input.GetTouch(0);
                //좌우키 범위에 들어가면 안 됨(좌우키 누를 때 브레이크하면 안되니까)
                if (!(touch.position.x >= 0 && touch.position.x < rightButtonEnd))
                {
                    //이 때 브레이크 걸어주기
                    rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.0000001f, rigid.velocity.y);
                }
            }
            else if (Input.touchCount == 0)
            {
                //이 때 브레이크 걸어주기
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.0000001f, rigid.velocity.y);
            }
        } 
        //2. 좌우 및 점프키
        foreach (Touch touch in touches)
        {
            //왼쪽이동
            if(touch.position.x>=0&& touch.position.x < leftButtonEnd)
            {
                rigid.velocity = new Vector2(maxSpeed * -1, rigid.velocity.y);
            }
            //오른쪽이동
            if (touch.position.x >= leftButtonEnd && touch.position.x < rightButtonEnd)
            {
                rigid.velocity = new Vector2(maxSpeed * 1, rigid.velocity.y);
            }
            //점프
            if (touch.position.x >= Screen.width*0.5f && touch.position.x < jumpButtonEnd)
            {
                //그리고 점프중일때 또 점프하지못하게 해야함
                if (!isJumping)
                {
                    isJumping = true;
                    rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
                }
            }
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //땅에 닿으면
        if (collision.gameObject.tag == "Platform")
        {
            //점프중이 아닌거임
            isJumping = false;
        }
    }
}
