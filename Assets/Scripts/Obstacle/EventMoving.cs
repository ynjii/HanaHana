using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMoving : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints; // 움직일 경로의 Waypoint들

    [SerializeField]
    private float moveSpeed = 10.0f; // 이동 속도

    private int currentWaypointIndex = 0;
    private int num = 0; // 몇번 돌았는지.

    private void OnEnable()
    {
        // GameObject가 활성화될 때마다 코루틴 시작. 이거!!! fade in 중복임!!!!! 걍 fadein 스크립트를 따로 만들어야 함!!!
        StartCoroutine(FadeIn());
        num = 0;
        StartCoroutine(MoveAlongPath());
    }

    private IEnumerator MoveAlongPath()
    {
        while (num < 8)
        {
            // 처음 Waypoint로 이동 이거 웨이 포인트 어캐했냐면 빈 obj 선택해서 그냥 끌어다줌.
            //근데 이게... 스크린 별로 차이가 있따보니 그냥 window값 찾아서 바꿀가 생각중. 
            Vector3 targetPosition = waypoints[currentWaypointIndex].position;
            while (Vector3.Distance(transform.position, targetPosition) > 0.001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.unscaledDeltaTime);
                yield return null;
            }

            // 다음 Waypoint로 이동
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            targetPosition = waypoints[currentWaypointIndex].position;
            while (Vector3.Distance(transform.position, targetPosition) > 0.001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.unscaledDeltaTime);
                yield return null;
            }

            num++;
            yield return null;
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        float fadeDuration = 2.0f; // 페이드 인에 걸리는 시간 (초)
        Color originalColor = this.gameObject.GetComponent<SpriteRenderer>().color;

        while (elapsedTime < fadeDuration)
        {
            // 경과 시간에 따른 알파 값 변경
            float alpha = Mathf.Lerp(0f, originalColor.a, elapsedTime / fadeDuration);
            Color newColor = originalColor;
            newColor.a = alpha;

            // 요정(Soul)의 알파 값을 변경하여 페이드 인 효과 생성
            this.gameObject.GetComponent<SpriteRenderer>().color = newColor;

            // 다음 프레임으로 이동
            yield return null;

            // 경과 시간 갱신
            elapsedTime += Time.unscaledDeltaTime;
        }

        // 알파 값 최종 설정
        Color finalColor = originalColor;
        finalColor.a = originalColor.a;
        this.gameObject.GetComponent<SpriteRenderer>().color = finalColor;
    }


}
