using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : ParentObstacleController
{
    public enum Obtype
    {
        MoveToPoints
    }
    /// <summary>
    /// MoveToPoints변수들
    /// </summary>
    [SerializeField] Obtype obtype;
    [SerializeField] LineRenderer line;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform _transform;
    [SerializeField] bool repeatLine = false;//라인을 반복해서 움직이는지, 라인의 끝 꼭짓점 가면 끝나는지

    private void Update()
    {
        if (base.IsMoving)
        {
            switch (obtype)
            {
                case Obtype.MoveToPoints:
                    StartCoroutine(MoveToPoints(line, moveSpeed, _transform));
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
    IEnumerator MoveToPoints(LineRenderer line, float moveSpeed, Transform _transform)
    {
        //배열생성
        Vector3[] path=new Vector3[line.positionCount];
        for (int i = 0; i < line.positionCount; i++)
        {
            // 선의 꼭짓점 가져옴
            path[i] = line.GetPosition(i);
        }
        //시작점
        int currentTargetIndex = 0;
        Vector3 targetPosition = path[currentTargetIndex];

        while (true)
        {
            
            //타겟 꼭짓점과의 거리재기
            float distanceToTarget = Vector3.Distance(_transform.position, targetPosition);
            //도달했으면
            if (distanceToTarget <= 1.0f)
            {
                //마지막 꼭짓점 도달시 break
                if (!repeatLine)
                {
                    if (currentTargetIndex == path.Length - 1)
                    {
                        break;
                    }
                }
                
                //다음 꼭짓점으로 타겟변경
                currentTargetIndex = (currentTargetIndex + 1) % path.Length;
                targetPosition = path[currentTargetIndex];
            }
            //이 쪽으로 이동
            _transform.position = Vector3.MoveTowards(_transform.position, targetPosition, Time.deltaTime * moveSpeed);
            //_transform.position = new Vector3(_transform.position.x,_transform.position.y,-1f);
            yield return null;//deltaTime 기다리고 돌아감
        }
    }
}
