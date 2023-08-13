using UnityEngine;

public class Swing2 : MonoBehaviour
{
    public float angle = 90f; // 최대 회전 각도 (양쪽으로 30도)
    public float addAngle=0f;
    private float lerpTime = 0;
    private float speed = 45f; // 회전 속도 (도/초), 예: 초당 45도 회전

    private void Update()
    {
        lerpTime += Time.deltaTime * speed;
        float currentAngle = Mathf.Lerp(-angle, angle, GetLerpTParam())+addAngle;

        transform.rotation = Quaternion.Euler(0f,  0f,currentAngle);
    }

    float GetLerpTParam()
    {
        return (Mathf.Sin(lerpTime * Mathf.Deg2Rad) + 1) * 0.5f;
    }
}
