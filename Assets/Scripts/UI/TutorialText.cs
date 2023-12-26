using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 시작 때 튜토리얼 진행 관련 알고리즘. 진행 사항 기록 및 체크
/// saveload 스크립트의 함수를 이용, tutorial playerprefs가 있다.
/// tutorial
/// 0 : 처음 상태, 시작 튜토리얼 나감 ->1
/// 1 : ui 꺼라
/// 2 : tutorialfakeitem이 호출되면 2로 변함, fakeitem 튜토리얼 나감 ->3
/// 3 : ui 꺼라
/// TutoEnd: tutorialflag가 호출되면, 4가 아니라면, flag 튜토리얼 나감->
/// 4:  ui 꺼라
/// </summary>
public class TutorialText : MonoBehaviour
{
    [SerializeField] PopupText popup_text_prefab;

    private List<string> text_list1_1 = new List<string>() { "동화나라가 어긋났어요!", "아이템을 전달해 공주를 진정시키세요!" };
    private List<string> text_list1_2 = new List<string>() { "그거 말고요.", "아이템을 전달하지 못하면 무시무시한 일이 생길 거예요.", "Good Luck!" };
    private List<string> text_list1_3 = new List<string>() { "튜토리얼이 끝났습니다. 이제 모험을 떠나볼까요?" };
    public GameObject SaveLoad;
    public GameObject Button;

    void Start()
    {
        int tutorial_flag = SaveLoad.GetComponent<SaveLoad>().LoadDeathCount("tutorial");

        if (tutorial_flag == 1 || tutorial_flag == 3 || tutorial_flag == 4)
        {
            gameObject.SetActive(false);
        }

        else if (tutorial_flag == 0)
        {

           
            // this.object의 child인 prologue를 찾아서 처리
            Transform prologue = transform.Find("Prologue");

            if (prologue != null)
            {
                // prologue를 활성화하고 timescale을 0으로 만들기
                Button.SetActive(false);
                prologue.gameObject.SetActive(true);
                Time.timeScale = 0f;

                // 2초 뒤에 Destroy 호출
                StartCoroutine(DestroyAfterDelay(prologue.gameObject, 5f));
            }
        }

        else if (tutorial_flag == 2)
        {//튜토리얼 가짜 아이템에 닿으면 나오는 텍스트
            popup_text_prefab.PopupTextList(text_list1_2, true);
            SaveLoad.GetComponent<SaveLoad>().SaveDeathCount("tutorial", 3);
        }
    }
    /// <summary>
    /// 맨마지막, flag 닿으면 모든 튜토리얼 진행 사항을 끄고 간다. 
    /// </summary>
    public void TutoEnd()
    {
        popup_text_prefab.PopupTextList(text_list1_3, true);
        SaveLoad.GetComponent<SaveLoad>().SaveDeathCount("tutorial", 4);
        gameObject.SetActive(false);
    }

    IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Destroy(obj);
        popup_text_prefab.PopupTextList(text_list1_1, true);
        SaveLoad.GetComponent<SaveLoad>().SaveDeathCount("tutorial", 1);
        Button.SetActive(true);
    }

}
