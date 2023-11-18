using UnityEngine;
using UnityEngine.UI;

public class PopupManager2 : MonoBehaviour
{
    public GameObject popup2; // 팝업창 게임 오브젝트

    private void Start()
    {
        // 팝업창 초기화
        popup2.SetActive(false);
    }

    // 팝업창 활성화
    public void ShowPopup()
    {
        popup2.SetActive(true);
        Debug.Log(this.gameObject.name + "에서 ShowPopup()호출" + popup2.gameObject.name + "이 팝업");

    }

    // 팝업창 닫기
    public void ClosePopup()
    {
        popup2.SetActive(false);
        Debug.Log(this.gameObject.name + "에서 ClosePopup()호출" + popup2.gameObject.name + "이 팝업");

    }

}
