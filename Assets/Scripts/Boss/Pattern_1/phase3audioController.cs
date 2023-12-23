using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phase3audioController : MonoBehaviour
{
    public GameObject flame;
    public GameObject beep;

    void Start()
    {
            StartCoroutine(sound3());
    }

    IEnumerator sound3() {
        for (int i = 0; i < 6; i++) {
            flame.gameObject.SetActive(false);
            beep.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.4f);
            flame.gameObject.SetActive(true);
            beep.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
        }
    }
}
