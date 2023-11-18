using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerTouchTest : MonoBehaviour
{
    float rightButtonEnd= Screen.width * 0.4167f;
    float leftButtonEnd= Screen.width * 0.2083f;
    float jumpButtonEnd= Screen.width;
    bool isJumping = false;
    Rigidbody2D rigid;
    [SerializeField] private float maxSpeed=5f;
    [SerializeField] private float jumpPower=5f;
    // Start is called before the first frame update
    void Start()
    {
        rigid=GetComponent<Rigidbody2D>();  
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
                Touch touch=Input.GetTouch(0);
                //좌우키 범위에 들어가면 안 됨
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
            if(touch.position.x>=0&& touch.position.x < leftButtonEnd)
            {
                rigid.velocity = new Vector2(maxSpeed * -1, rigid.velocity.y);
            }
            if (touch.position.x >= leftButtonEnd && touch.position.x < rightButtonEnd)
            {
                rigid.velocity = new Vector2(maxSpeed * 1, rigid.velocity.y);
            }
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
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = false;
        }
    }
}
