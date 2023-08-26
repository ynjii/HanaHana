using UnityEngine;
using UnityEngine.UI;

public class SpriteChanger : MonoBehaviour
{
    public GameObject targetObject; // 변경할 오브젝트
    public Sprite newSprite; // 새로운 스프라이트
    public Vector3 newScale = new Vector3(1.5f, 1.5f, 1f); // 새로운 스케일

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = targetObject.GetComponent<SpriteRenderer>();
    }

    public void ChangeSpriteOnClick()
    {
        if (spriteRenderer != null && newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
            targetObject.transform.localScale = newScale; // 스케일 변경
        }
        else
        {
            Debug.LogWarning("SpriteRenderer or newSprite is not assigned.");
        }
    }
}
