using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpike : MonoBehaviour
{
    public GameObject[] obstacle;
    [SerializeField]
    private float waitingTime = 3f;
    private int[] indexNum;

    void Start()
    {
        chooseRandom();
    }

    private void chooseRandom()
    {
        indexNum=new int[obstacle.Length];
        for (int i = 0; i < 3; i++)
        {
            indexNum[i] = Random.Range(0, obstacle.Length);
            obstacle[indexNum[i]].SetActive(true);
        }
        for (int i = 0; i < 3; i++)
        {
            StartCoroutine(WaitChoose(indexNum[i]));
        }
    }

    IEnumerator WaitChoose(int indexNum)
    {
        yield return new WaitForSeconds(waitingTime);
        obstacle[indexNum].SetActive(false);
    }
}
