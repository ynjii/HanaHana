using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorlalNextTextForKeyboard : MonoBehaviour
{
    PopupText popuptext;
    private void Awake()
    {
        popuptext = GameObject.Find("PopupText_prefab").GetComponent<PopupText>();
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine(Rest());
            popuptext.BTN_NextText();
        }
    }

    IEnumerator Rest()
    {
        yield return new WaitForSeconds(0.1f);
    }
}
