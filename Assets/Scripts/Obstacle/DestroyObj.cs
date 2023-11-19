using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//삭제해도 무방...
//그냥 뭐랑 부딪히면 사라진다. 거울맵 glass 탄막에서 사용했다.  그냥 거울맵은 가서 수정을 할것.
public class DestroyObj : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
    }
}
