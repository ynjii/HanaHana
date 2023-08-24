using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pattern2Controller : MonoBehaviour
{
    private System.Random random = new System.Random();
    private List<System.Action> availablePatterns = new List<System.Action>();
    public List<GameObject> enemyPatterns = new List<GameObject>();
    public List<GameObject> warningPatterns = new List<GameObject>(); //닿으면 죽는 패턴
    public List<GameObject> redFlag = new List<GameObject>(); // 경고
    public List<GameObject> availableFire2Prefabs = new List<GameObject>(); //패턴 1에 쓰이는 sliding fire
    private System.Action previousPattern;

    public Slider slHP; //보스 피받아오기
    private float currentHP;

    private float warningHP;

    public Animator animator;

    private void Start()
    {
        currentHP = slHP.maxValue; //슬라이더 시작값 받아오기
        warningHP = slHP.maxValue;

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
        }

        if (slHP.value <= warningHP - 11f) //3초 경고 1초동안 피하기 
        {
            animator.SetBool("isCollectEnergy", true);
            warningHP -= 15f;
            ToggleRandomRedFlag(); //경고 3초동안 하기
        }
    }

    private void StartNextPattern()
    {
        //시작할때 모두 비활성화
        foreach (GameObject enemyPattern in enemyPatterns)
        {
            enemyPattern.SetActive(false);
        }
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

    private void ToggleRandomRedFlag()
    {
        int randomIndex = Random.Range(0, warningPatterns.Count); // 랜덤한 인덱스 생성

        // 선택된 경고 패턴 활성화
        redFlag[randomIndex].SetActive(true);

        // 3초 후에 경고 패턴을 다시 비활성화
        StartCoroutine(DisableRedFlagAfterDelay(3f, randomIndex));
    }

    private IEnumerator DisableRedFlagAfterDelay(float delay, int randomIndex)
    {
        yield return new WaitForSeconds(delay);

        foreach (GameObject redFlag in redFlag)
        {
            redFlag.SetActive(false);
        }

        //바로 함정 패턴 활성화
        StartCoroutine(EnableWarningPatternAfterDelay(randomIndex));
    }

    private IEnumerator EnableWarningPatternAfterDelay(int randomIndex)
    {

        warningPatterns[randomIndex].SetActive(true);
        yield return new WaitForSeconds(1f); // 2초 대기

        warningPatterns[randomIndex].SetActive(false); // 경고 패턴 비활성화
        animator.SetBool("isCollectEnergy", false);

    }

    // 패턴 함수들 (구현 필요)
    private void Pattern1()
    {
        // 패턴 1 실행 코드
        Debug.Log("1");
        enemyPatterns[0].SetActive(true);
        StartCoroutine(SlidingFireCoroutine());
    }

    private IEnumerator SlidingFireCoroutine()
    {
        while (true) // 무한 반복 (코루틴을 직접 중지하기 전까지)
        {
            animator.SetBool("isHitTable", true);
            yield return new WaitForSeconds(Random.Range(2f, 4f)); // 랜덤한 시간 대기
            animator.SetBool("isHitTable", false);

            int randomPrefabIndex = Random.Range(0, availableFire2Prefabs.Count);
            GameObject selectedFire2Prefab = availableFire2Prefabs[randomPrefabIndex];

            Vector3 fixedPosition = new Vector3(0f, -2.3f, 0f); // 고정된 위치

            GameObject spawnedFire2 = Instantiate(selectedFire2Prefab, fixedPosition, selectedFire2Prefab.transform.rotation);

            yield return new WaitForSeconds(1f); // 일정 시간 대기

            Destroy(spawnedFire2); // 프리팹 제거
        }
    }


    private void Pattern2()
    {
        // 패턴 2 실행 코드
        Debug.Log("2");
        enemyPatterns[1].SetActive(true);

    }

    private void Pattern3()
    {
        // 패턴 3 실행 코드
        Debug.Log("3");
        enemyPatterns[2].SetActive(true);
    }

    private void Pattern4()
    {
        // 패턴 4 실행 코드
        Debug.Log("4");
        enemyPatterns[3].SetActive(true);
    }
}
