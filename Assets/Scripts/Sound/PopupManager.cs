using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public GameObject popup; // 팝업창 게임 오브젝트
    public Button yesButton; // 네 버튼
    public Button noButton; // 아니오 버튼

    private void Start()
    {
        // 팝업창 초기화
        popup.SetActive(false);

        // 버튼 클릭 시 동작 설정
        yesButton.onClick.AddListener(YesAction);
        noButton.onClick.AddListener(NoAction);
    }

    // 팝업창 활성화
    public void ShowPopup()
    {
        popup.SetActive(true);
    }

    // 팝업창 닫기
    public void ClosePopup()
    {
        popup.SetActive(false);
    }

    // 네 버튼 동작
    public void YesAction()
    {
        // 네 버튼을 눌렀을 때의 동작 추가
        Debug.Log("네 버튼을 클릭했습니다.");
    }

    // 아니오 버튼 동작
    public void NoAction()
    {
        // 아니오 버튼을 눌렀을 때의 동작 추가
        Debug.Log("아니오 버튼을 클릭했습니다.");
    }
}
