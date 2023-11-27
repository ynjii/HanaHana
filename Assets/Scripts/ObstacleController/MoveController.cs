using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : ParentObstacleController
{
    public enum Obtype
    {
        MoveToPoints
    }
   
    [SerializeField] Obtype obtype;
    
    [SerializeField] Transform line; // 미리 그려진 선의 Transform
    [SerializeField] float moveSpeed;
    [SerializeField] Transform transform;



    private void Update()
    {
        if (base.IsMoving)
        {
            switch (obtype)
            {
                case Obtype.MoveToPoints:
                    StartCoroutine(MoveToPoints(line, moveSpeed, transform));
                    base.IsMoving = false;
                    break;
            }   
        }
    }

    /// <summary>
    /// 찍은 꼭짓점대로 움직임. //유진
    /// 사용예시: SnowBoss4.cs 
    /// targetPositions: 꼭짓점 리스트
    /// transform: 움직일 오브젝트의 Transform
    /// moveSpeed: 움직일 스피드
    IEnumerator MoveToPoints(Transform line, float moveSpeed, Transform transform)
    {

        int currentTargetIndex = 0;
        private Transform[] waypoints; // 선의 각 점을 저장할 배열
        waypoints = new Transform[line.childCount];
        for (int i = 0; i <line.childCount; i++)
        {
            waypoints[i] = path.GetChild(i);
        }

        while (true)
        {
            
            //위치점 찍어놨던 배열에서 하나씩 꺼냄
            Vector3 targetPosition = targetPositions[currentTargetIndex];
            // 현재 위치와 목표 위치 사이의 거리 계산
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
            // 일정 거리 내에 있으면 다음 목표 위치로 변경
            if (distanceToTarget <= 0.1f) // 예시로 0.1f를 사용, 원하는 값으로 조정 가능
            {
                if (currentTargetIndex == targetPositions.Count - 1)
                {
                    break;
                }
                currentTargetIndex = (currentTargetIndex + 1) % targetPositions.Count;
                targetPosition = targetPositions[currentTargetIndex];
            }

            // 목표 위치로 이동
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
            
            yield return null;
        }
    }
}
