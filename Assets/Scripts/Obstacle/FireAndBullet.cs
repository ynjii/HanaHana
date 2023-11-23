using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObstacleController;

/// <summary>
/// 프리팹 이동하는 스크립트입니다
/// 총알을 "발사"시키는 것 (x)
/// 총알(본인)이 움직이는 것 (o)
/// </summary>
public class FireAndBullet : MonoBehaviour//SnowWhite씬의 fire에도 쓰이고 boss4의 bullet에도 쓰여서 클래스이름 Fire->FireAndBullet으로 바꿈
{
    //움직이는 방향
    public enum ObType
    {
        down,
        up,
        left,
        right,
        upRight, // 대각선 방향 추가
        upLeft,  // 대각선 방향 추가
        downRight, // 대각선 방향 추가
        downLeft,  // 대각선 방향 추가
    }

    [SerializeField]
    private ObType direction; // 방향
    [SerializeField]
    private float speed; // 속도

    [SerializeField]
    public float bullet_damage; // 총알데미지

    //이게 뭐냐면.. 발사시키는 애가 뭐 발사블록 이렇게 있으면
    //걔에 부딪혀도 부딪힌거로 감지되거든요
    //그래서 걔에 부딪힌거 1, 그리고 또 다른 벽에 부딪힌거 1 해서
    //2일때만 사라져야해서 넣은 변수
    //이 변수 없이 그냥 콜리젼 닿으면 사라지게하면 애가 세상밖도 못나오고 
    //그냥 발사블록 안에서 바로 사라짐 ㅋ;
    private int colCount = 0;

    // Update is called once per frame

    void Update()
    {
        //얘가 발사블록없는 총알이라면 한 번만 맞아도 사라지게 해야함
        if (this.gameObject.CompareTag("bullet") && colCount >= 1)//보스패턴4에서 썼던 총알 태그. 
        {
            Destroy(gameObject);
        }
        if (colCount >= 2)//근데 발사블록 안에서 발사되는 불똥같은애면은 2번 맞았을 때 사라져야함
        {
            Destroy(gameObject);
        }

        //방향따라처리
        switch (direction)
        {
            case ObType.right:
                transform.Translate(transform.right * speed * Time.deltaTime);
                break;
            case ObType.left:
                transform.Translate(-1f * transform.right * speed * Time.deltaTime);
                break;
            case ObType.up:
                transform.Translate(transform.up * speed * Time.deltaTime);
                break;
            case ObType.down:
                transform.Translate(-1f * transform.up * speed * Time.deltaTime);
                break;
            case ObType.upRight:
                transform.Translate((transform.right + transform.up).normalized * speed * Time.deltaTime);
                break;
            case ObType.upLeft:
                transform.Translate((-transform.right + transform.up).normalized * speed * Time.deltaTime);
                break;
            case ObType.downRight:
                transform.Translate((transform.right - transform.up).normalized * speed * Time.deltaTime);
                break;
            case ObType.downLeft:
                transform.Translate((-transform.right - transform.up).normalized * speed * Time.deltaTime);
                break;
        }

    }

    //콜라이더 닿으면 num 증가해줌
    private void OnCollisionEnter2D(Collision2D collision)
    {
        colCount++;
    }

    //화면밖이면 애 없애줌
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
