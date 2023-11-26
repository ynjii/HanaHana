using UnityEngine;

/*Sin 함수를 이용한 스윙 함수, 각도 조절 가능*/
public class Swing2 : MonoBehaviour
{
    public float angle = 90f; // 최대 회전 각도 (양쪽으로 45도)
    private float lerpTime = 0;
    [SerializeField]
    private float speed = 45f; // 회전 속도 (도/초), 예: 초당 45도 회전

    private void Update()
    {
        lerpTime += Time.deltaTime * speed;
        float currentAngle = Mathf.Lerp(-angle, angle, GetLerpTParam());

        transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
    }

    float GetLerpTParam()
    {
        return (Mathf.Sin(lerpTime * Mathf.Deg2Rad) + 1) * 0.5f;
    }
}
