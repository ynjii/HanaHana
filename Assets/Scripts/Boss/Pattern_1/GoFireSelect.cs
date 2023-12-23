using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoFireSelect : MonoBehaviour
{
    public List<GameObject> flames = new List<GameObject>();

    void Start()
    {
        ShuffleList<GameObject>(flames);
        StartCoroutine(activateFlame(flames));
    }

    IEnumerator activateFlame(List<GameObject> flames)
    {
        foreach (GameObject flame in flames)
        {
            flame.SetActive(true);
            yield return new WaitForSeconds(2.4f);
            flame.SetActive(false);
        }
    }

    void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
