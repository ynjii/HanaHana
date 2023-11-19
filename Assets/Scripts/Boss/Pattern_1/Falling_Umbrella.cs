using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 거울맵 떨어지는 우산: 아이템 닿았나, 안 닿았나만 감지
/// </summary>
public class Falling_Umbrella : MonoBehaviour
{
    //아이템 얻었나 확인 변수
    private bool item_get = false;
    //캡슐화
    public bool Item_get
    {
        get { return item_get; }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //닿았으면 아이템 얻은걸로 처리
        item_get = true;
    }


}



