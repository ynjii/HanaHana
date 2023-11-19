using UnityEngine;
using UnityEngine.UI;

public class ImageSwitcher : MonoBehaviour
{
    public Image image1; // 1번 이미지 (Image 컴포넌트)
    public Image image2; // 2번 이미지 (Image 컴포넌트)

    private bool switchImages = false; // 이미지 전환 여부

    private void Start()
    {
        // 처음에는 1번 이미지만 보이게 설정
        image1.enabled = true;
        image2.enabled = false;
    }

    public void SwitchImageOnClick()
    {
        // 이미지 전환 여부 토글
        switchImages = true;

        // 이미지 변경
        image1.enabled = false;
        image2.enabled = true;
    }

    private void Update()
    {
        // 이미지 전환 후에는 이미지 변경을 유지
        if (switchImages)
        {
            image1.enabled = false;
            image2.enabled = true;
        }
    }
}
