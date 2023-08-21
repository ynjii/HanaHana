using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpike : MonoBehaviour
{
    public enum actDe{
        Activate,
        Deactivate
    }
    public GameObject[] obstacle;
    [SerializeField]
    private actDe Deact;
    [SerializeField]
    private float waitingTime = 3f;
    [SerializeField]
    private int max=3;
    [SerializeField]
    private int Xmax=1;
    private int[] indexNum;

    void Start()
    {
        switch(Deact)
        {
            case actDe.Activate:
                chooseRandom();
                break;
            case actDe.Deactivate:
                XchooseRandom();
                break;
        }
        
    }

    private void chooseRandom()
    {
        indexNum=new int[obstacle.Length];
        for (int i = 0; i < max; i++)
        {
            indexNum[i] = Random.Range(0, obstacle.Length);
            obstacle[indexNum[i]].SetActive(true);
        }
        for (int i = 0; i < max; i++)
        {
            StartCoroutine(WaitChoose(indexNum[i]));
        }
    }

    private void XchooseRandom()
    {
        indexNum=new int[obstacle.Length];
        for (int i = 0; i < Xmax; i++)
        {
            indexNum[i] = Random.Range(0, obstacle.Length);
            obstacle[indexNum[i]].SetActive(false);
        }
        for (int i = 0; i < Xmax; i++)
        {
            StartCoroutine(XWaitChoose(i));
        }
    }

    IEnumerator WaitChoose(int indexNum)
    {
        yield return new WaitForSeconds(waitingTime);
        obstacle[indexNum].SetActive(false);
    }

    IEnumerator XWaitChoose(int indexNum)
    {
        yield return new WaitForSeconds(waitingTime);
        obstacle[indexNum].SetActive(false);
    }
}
