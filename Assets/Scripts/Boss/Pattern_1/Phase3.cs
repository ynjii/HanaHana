using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3 : MonoBehaviour
{
    /*
    1. DownSpike+Mirror Block 활성화
    2. Left, Right Spike 활성화
    3. Block, GoFire 활성화
    */
    public GameObject downSpike;
    public GameObject LeftSpike;
    public GameObject RightSpike;
    public GameObject Block;
    public GameObject GoFire;
    

    void Start()
    {
        StartCoroutine(Pattern1Phase3());
    }

    IEnumerator Pattern1Phase3()
    {
        yield return new WaitForSeconds(2f);
        downSpike.SetActive(true);
        yield return new WaitForSeconds(3f);
        downSpike.SetActive(false);
        LeftSpike.SetActive(true);
        RightSpike.SetActive(true);
        yield return new WaitForSeconds(3f);
        LeftSpike.SetActive(false);
        RightSpike.SetActive(false);
        Block.SetActive(true);
        GoFire.SetActive(true);
        yield return new WaitForSeconds(1.38f);
        Block.SetActive(false);
        GoFire.SetActive(false);
        GoFire.SetActive(true);
        yield return new WaitForSeconds(1.38f);
        Block.SetActive(false);
        GoFire.SetActive(false);
        
    }
}
