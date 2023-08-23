using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pattern2Controller : MonoBehaviour
{
    private System.Random random = new System.Random();
    private List<System.Action> availablePatterns = new List<System.Action>();
    public List<GameObject> warningPatterns = new List<GameObject>(); // RedFlag 오브젝트 리스트
    private System.Action previousPattern;

    public Slider slHP; //보스 피받아오기
    private float currentHP;

    private float warningHP;
    private void Start()
    {
        currentHP = slHP.maxValue; //슬라이더 시작값 받아오기

        // 패턴 리스트 생성 (패턴 이름에 따라 수정 필요)
        availablePatterns = new List<System.Action>
        {
            Pattern1,
            Pattern2,
            Pattern3,
            Pattern4
        };



        // 첫 번째 패턴 시작
        StartNextPattern();
    }

    private void Update()
    {
        // 보스 패턴이 끝나면 다음 패턴 시작
        // 이 부분은 패턴이 끝나는 조건에 따라 수정되어야 합니다.
        if (slHP.value <= currentHP - 15f && availablePatterns.Count > 0) //15초 지나면
        {
            currentHP -= 15f;
            StartNextPattern();
            ToggleRandomWarningPattern
        }
    }

    private void StartNextPattern()
    {
        // 이전 패턴을 제외한 패턴들로 리스트 갱신
        if (previousPattern != null)
        {
            availablePatterns.Remove(previousPattern);
        }

        // 랜덤한 패턴 선택
        int randomIndex = random.Next(availablePatterns.Count);
        System.Action selectedPattern = availablePatterns[randomIndex];

        // 선택된 패턴 실행
        selectedPattern.Invoke();

        // 이전 패턴 업데이트
        previousPattern = selectedPattern;
    }

    private void ToggleRandomWarningPattern()
    {
        int randomIndex = Random.Range(0, warningPatterns.Count); // 랜덤한 인덱스 생성
        for (int i = 0; i < warningPatterns.Count; i++)
        {
            warningPatterns[i].SetActive(i == randomIndex);
        }
    }

    // 패턴 함수들 (구현 필요)
    private void Pattern1()
    {
        // 패턴 1 실행 코드
        Debug.Log("1");
    }

    private void Pattern2()
    {
        // 패턴 2 실행 코드
        Debug.Log("2");
    }

    private void Pattern3()
    {
        // 패턴 3 실행 코드
        Debug.Log("3");
    }

    private void Pattern4()
    {
        // 패턴 4 실행 코드
        Debug.Log("4");
    }
}
