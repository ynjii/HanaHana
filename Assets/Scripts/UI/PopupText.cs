using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupText : MonoBehaviour
{
    [SerializeField] private GameObject popup_text_GO;
    [SerializeField] private GameObject fix_panel;
    
    private TextMeshProUGUI popup_text;
    private int currentTextNum;
    private List<string> text_list;

    void Awake()
    {
        currentTextNum = 0;
        popup_text_GO.SetActive(false);
        popup_text = popup_text_GO.transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void PopupTextList(List<string> text_list_input, bool is_fixed)
    {
        popup_text_GO.SetActive(true);
        text_list = text_list_input;
        popup_text.text = text_list[currentTextNum];

        if (is_fixed)
        {
            fix_panel.SetActive(true);
        }
    }

    public void BTN_NextText()
    {
        if (++currentTextNum >= text_list.Count)
        {
            currentTextNum = 0;
            fix_panel.SetActive(false);
            popup_text_GO.SetActive(false);
        }
        popup_text.text = text_list[currentTextNum];
    }
}