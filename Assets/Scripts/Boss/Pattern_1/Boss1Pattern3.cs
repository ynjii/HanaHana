using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Pattern3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
    count
     0. 각 gofire을 N번 실행시키고 싶다. : gofire를 inisiate / deactivate 한다. 12/N분마다
    N번 반복
    {
    gofire method
     1. gofire 중 랜덤한 floor 하나를 deactivate시킨다. 

    floor
     2. animation control method를 넣는다.
     3. flame, fire activate, deactivate 시간을 12/N에서 2:1로 배분한다. 
        -animation 기존 초를 나누고, 12*2/N*3를 곱해준다.
     
    gofire method 함수에서
    4. 전체 짠탄 제거 method}
     */
}
