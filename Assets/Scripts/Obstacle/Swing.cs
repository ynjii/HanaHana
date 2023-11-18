using UnityEngine;

/// <summary>
/// 스윙(그네질)을 구현하는 Unity 스크립트
/// 예시: 스노우화이트씬 성맵
/// </summary>
public class Swing : MonoBehaviour
{
    // 각도
    public float angle = 0;

    // 보간에 사용되는 시간
    private float lerpTime = 0;

    // 스윙 속도
    private float speed = 2f;

    // 매 프레임마다 호출되는 Unity 업데이트 메서드
    private void Update()
    {
        // lerpTime을 시간에 따라 증가시켜서 보간을 계산
        lerpTime += Time.deltaTime * speed;

        // 스윙 동작 계산 후, 객체의 회전을 업데이트
        transform.rotation = CalculateMovementOfPendulum();
    }

    // 스윙 동작의 회전 값을 계산하는 메서드
    Quaternion CalculateMovementOfPendulum()
    {
        // 전방 각도에서 후방 각도로의 보간을 계산하고 반환
        return Quaternion.Lerp(Quaternion.Euler(Vector3.forward * angle),
                               Quaternion.Euler(Vector3.back * angle),
                               GetLerpTParam());
    }

    // 보간에 사용될 t 매개변수 값을 계산하는 메서드
    float GetLerpTParam()
    {
        // Mathf.Sin 함수를 사용하여 부드러운 보간 효과를 만듦
        // lerpTime에 따라 Sin 값을 계산하고, 0에서 1 사이의 값으로 변환
        return (Mathf.Sin(lerpTime) + 1) * 0.5f;
    }
}
