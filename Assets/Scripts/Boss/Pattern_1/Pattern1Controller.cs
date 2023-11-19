using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// pattern1 controller
/// 초단위로 계산해서 아래 phase object 활성/비활성화함
/// 1. phase object를 함수로 만들어서 하나의 스크립트/여러개 스크립트에 넣기
/// 2. 이것도 다시 개선
/// </summary>
public class Pattern1Controller : MonoBehaviour
{
    //public GameObject phases=new List<GameObject>();
    public GameObject pattern1;
    public GameObject pattern2;
    public GameObject pattern3;
    public GameObject pattern4;
    public GameObject pattern5;
    public GameObject endPanel;
    public Rigidbody2D other;

    void Start()
    {
        StartCoroutine(Pattern());
    }

    IEnumerator Pattern()
    {
        pattern1.SetActive(true);
        yield return new WaitForSeconds(12f);
        pattern1.SetActive(false);
        yield return new WaitForSeconds(2f);
        pattern4.SetActive(true);
        yield return new WaitForSeconds(12f);
        pattern4.SetActive(false);
        yield return new WaitForSeconds(2f);
        pattern2.SetActive(true);
        yield return new WaitForSeconds(12f);
        pattern2.SetActive(false);
        other.constraints = RigidbodyConstraints2D.FreezeRotation ;
        yield return new WaitForSeconds(2f);
        pattern3.SetActive(true);
        yield return new WaitForSeconds(12f);
        pattern3.SetActive(false);
        other.constraints = RigidbodyConstraints2D.FreezeRotation ;
        yield return new WaitForSeconds(2f);
        pattern5.SetActive(true);
        yield return new WaitForSeconds(12f);
        pattern5.SetActive(false);
        other.constraints = RigidbodyConstraints2D.FreezeRotation ;
        endPanel.SetActive(true);
        Time.timeScale = 0f; //시간 정지

        /*int[] randomPattern = GenerateRandomPattern(1, 4); 

        for(int i=0;i<randomPattern.Length;i++){
        yield return new WaitForSeconds(2f);
        phases(i).SetActive(true);
        yield return new WaitForSeconds(12f);
        phases(i).SetActive(false);
        }*/
        //첫 인덱스에는 0이 들어있고, 그 다음부터는 1~4을 랜덤하게 재배열한 배열을 만드는 코드를 적어줘.
    }

    /*int[] GenerateRandomPattern(int minValue, int maxValue)
    {
        int[] pattern = new int[maxValue - minValue + 1];

        for (int i = 0; i < pattern.Length; i++)
        {
            pattern[i] = minValue + i;
        }

        ShufflePattern(pattern);

        return pattern;
    }

    void ShufflePattern(int[] pattern)
    {
        Random random = new Random();

        for (int i = pattern.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            int temp = pattern[i];
            pattern[i] = pattern[j];
            pattern[j] = temp;
        }
    }/*
    /*private System.Random random = new System.Random();
    private List<System.Action> availablePatterns = new List<System.Action>();
    [SerializeField] Camera camera;
    [SerializeField] GameObject patternChangeGO;
    private System.Action previousPattern;
    public Animator animator;
    public Slider slHP; //보스 피받아오가
    private float currentTime;
    private float currentHP;
    private float warningHP;

    private void Start()
    {
        currentHP = slHP.maxValue; //슬라이더 시작값 받아오기
        warningHP = slHP.maxValue;
        currentTime = slHP.maxValue;

        // 패턴 리스트 생성 (패턴 이름에 따라 수정 필요)
        availablePatterns = new List<System.Action>
        {
            Pattern1,
            Pattern2,
            Pattern3,
            Pattern4,
            Pattern5
        };

        // 첫 번째 패턴 시작
        StartNextPattern();
    }

    private void Update()
    {
        if (slHP.value <= 0)
        {
            StartCoroutine(PatternChange());
            enemyPattern.SetActive(false);
        }// 보스 패턴이 끝나면 다음 패턴 시작

        if (slHP.value <= currentHP - 12f && availablePatterns.Count > 0) //15초 지나면
        {
            currentHP -= 12f;
            StartNextPattern();
        }
    }

    IEnumerator PatternChange()
    {
        patternChangeGO.SetActive(true);
               // 카메라 shaking
        camera.transform.DOShakePosition(3, 1);

        // 보스 애니메이션 변경
        animator.SetBool("isHideEye", true);

        // 불 스프라이트는 자동 재생
        // 다음 씬 로드 : 보스 애니메이션 끝나고 이동
        yield return new WaitForSeconds(3);
        animator.SetBool("isHideEye", false);
        SceneManager.LoadScene("SnowBoss2");
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

    //페이즈 함수
    private void PatternGo(int i)
    {
        Debug.Log(i);
        enemyPatterns[i].SetActive(true);
    }*/
}
