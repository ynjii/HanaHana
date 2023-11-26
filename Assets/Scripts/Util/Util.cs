using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 자주 쓸 것 같은 메소드 모아놓은 클래스 
/// </summary>
public class Util : MonoBehaviour
{
  
    /// <summary>
    /// 찍은 꼭짓점대로 움직임. //유진
    /// 사용예시: SnowBoss4.cs 
    /// targetPositions: 꼭짓점 리스트
    /// transform: 움직일 오브젝트의 Transform
    /// moveSpeed: 움직일 스피드
    private void MoveToPoints(List<Vector3> targetPositions,  float moveSpeed, Transform transform)
    {
        int currentTargetIndex = 0;
        //위치점 찍어놨던 배열에서 하나씩 꺼냄
        Vector3 targetPosition = targetPositions[currentTargetIndex];
        // 현재 위치와 목표 위치 사이의 거리 계산
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        // 일정 거리 내에 있으면 다음 목표 위치로 변경
        if (distanceToTarget <= 0.1f) // 예시로 0.1f를 사용, 원하는 값으로 조정 가능
        {
            currentTargetIndex = (currentTargetIndex + 1) % targetPositions.Count;
            targetPosition = targetPositions[currentTargetIndex];
        }

        // 목표 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }
    /// <summary>
    /// 애니메이터 바꿔주기
    /// path: 경로명
    /// anim: 바꿔줄 오브젝트의 애니메이터
    /// </summary>
    public void ChangeAnimator(Animator anim, string path)
    {
        anim.runtimeAnimatorController =
            (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load(path,
                typeof(RuntimeAnimatorController)));

    }
}
