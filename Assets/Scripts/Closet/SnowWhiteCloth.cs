using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowWhiteCloth : MonoBehaviour
{
    private string selected;
    [SerializeField] private Sprite[] clothes;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("SnowWhiteClothGet") == "true")
        {
            GameObject.Find("closetSnow").gameObject
             .transform.GetChild(0).gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetString("SnowWhiteCloth") == "true")
        {
            GameObject.Find("Player").gameObject.GetComponent<Image>().sprite = clothes[0];
        }
    }

    public void SelectSnow()
    {
        selected = "closetSnow";
    }
    public void SelectBasic()
    {
        selected = "closetBasic";
    }

    public void PutOnSnowWhiteCloth()
    {
        if (PlayerPrefs.GetString("SnowWhiteClothGet") == "true"&&selected== "closetSnow")
        {
            GameObject.Find("Player").gameObject.GetComponent<Image>().sprite = clothes[0];
            PlayerPrefs.SetString("SnowWhiteCloth", "true");
        }
    }
    public void PutOnBasicCloth()
    {
        Debug.Log(selected);
        if(selected == "closetBasic")
        {
            GameObject.Find("Player").gameObject.GetComponent<Image>().sprite = clothes[1];
            PlayerPrefs.SetString("SnowWhiteCloth", "false");
        }
    }
}
