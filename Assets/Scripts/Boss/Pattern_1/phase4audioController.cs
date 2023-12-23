using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phase4audioController : MonoBehaviour
{
    public GameObject spike;
    public GameObject beep;

    void Start()
    {
            StartCoroutine(sound4());
    }

    IEnumerator sound4() {
        for (int i = 0; i < 3; i++) {
            spike.gameObject.SetActive(false);
            beep.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.2f);
            spike.gameObject.SetActive(true);
            beep.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.9f);
            spike.gameObject.SetActive(false);
            beep.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            spike.gameObject.SetActive(true);
            beep.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
        }
    }
}
