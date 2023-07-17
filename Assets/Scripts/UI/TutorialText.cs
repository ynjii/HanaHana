using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    [SerializeField] PopupText popup_text_prefab;

    private List<string> text_list1_1 = new List<string>() { "동화나라가 어긋났어요!" };
    private List<string> text_list1_2 = new List<string>() { "아이템을 전달해 공주를 진정시키세요!" };
    void Start()
    {
        popup_text_prefab.PopupTextList(text_list1_1, true);
        popup_text_prefab.PopupTextList(text_list1_2, true);
    }

}
