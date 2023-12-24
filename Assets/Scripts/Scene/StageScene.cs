using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageScene : MonoBehaviour
{
    [SerializeField] Image image;
    private void Awake()
    {
        if (PlayerPrefs.GetString("SnowWhiteClear") == "true")
        {
            this.gameObject.GetComponent<Image>().sprite = image.sprite;
        }
    }
}
