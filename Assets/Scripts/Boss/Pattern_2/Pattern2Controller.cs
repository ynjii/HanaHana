using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//보스 패턴 랜덤으로 돌리는 코드입니다.
//한 스크립트에 다 연결했는데 걍 스크립트를 PATTERN OBSTACLE들한테 부착해도 되지 않을까 생각중.
//부모 클래스로 만들면 재활용에 용이할듯합니다. 부모 말고도 script 이름만 맞추면 잘 쓸수 있을지도??

public class Pattern2Controller : MonoBehaviour
{
    private System.Random random = new System.Random();
    private List<System.Action> availablePatterns = new List<System.Action>(); //패턴을 넣습니다. 패턴별로 부모 스크립트에 넣고, 비활성화했다가 활성화 되면 움직이는 식으로 만들었어요 
    public List<GameObject> enemyPatterns = new List<GameObject>();

    private ThrowObj throwObj;


    public List<GameObject> warningPatterns = new List<GameObject>(); //닿으면 죽는 패턴
    public List<GameObject> redFlag = new List<GameObject>(); // 경고 빨간색 창이에요.
    public List<GameObject> pattern4Mirrors = new List<GameObject>(); // 4패턴의 거울들
    public List<GameObject> availableFire2Prefabs = new List<GameObject>(); //패턴 1에 쓰이는 sliding fire

    [SerializeField] Camera camera;

    [SerializeField] GameObject patternChangeGO;
    [SerializeField] GameObject FadeIn;
    [SerializeField] GameObject FadeOut;
    private System.Action previousPattern;

    public Slider slHP; //보스 피받아오기

    private float currentTime; //현재 시간
    private float currentHP;//현재 HP 

    private bool isDone = false;

    private float warningHP;

    public Animator animator;

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
            Pattern4
        };


        //StartCoroutine(startEvent());
        // 첫 번째 패턴 시작
        StartNextPattern();
    }

    private void Update()
    {
        //만약 슬라이더 HP가 0 이하면 시간이 지난것이기 때문에 다음 scene 으로 넘어간다. 
        if (slHP.value <= 0)
        {
            StartCoroutine(PatternChange()); //이거는 전체 보스맵 4개의 패턴에서의 패턴. scene change로 바꾸면 좋을 것 같음.
        }
        // 보스 패턴이 끝나면 다음 패턴 시작
        // 패턴 이름을 수정해야할 것 같다. 구분이 힘듬. 
        // 이 부분은 패턴이 끝나는 조건에 따라 수정되어야 합니다.
        if (slHP.value <= currentHP - 15f && availablePatterns.Count > 0) //15초 지나면, 그리고 가능한 패턴이 남아있다면
        {
            currentHP -= 15f;
            StartNextPattern(); //여기서는 보스맵 2 안의 패턴
        }

        //이거 전체를 60으로 하지 말고 더 길게 잡고 보스가 패턴 바꾸는 동안 더 시간을 주면 좋을 것 같다. 
        if (slHP.value <= warningHP - 11f) //3초 경고 1초동안 피하기 
        {
            animator.SetBool("isCollectEnergy", true); //공주의 애니메이션이 기를 모으는 것처럼 보임. 
            warningHP -= 15f;
            ToggleRandomRedFlag(); //경고 3초동안 하기
        }

        //wraning 뜰때마다 사라짐. (사유 : 억까)
        if (slHP.value <= currentTime - 13f)
        {
            currentTime -= 15f;

            //시작할때 모두 비활성화. 생각해보면 previousPattenr만 비활성화 하면 된다. 
            foreach (GameObject enemyPattern in enemyPatterns)
            {
                enemyPattern.SetActive(false);
            }
        }

    }
    /*
        private IEnumerator startEvent()
        {
            Time.timeScale = 0f; //  시간 멈춤

            FadeOut.SetActive(true);

            yield return new WaitForSecondsRealtime(2f); // 2초 동안 대기 (실제 경과 시간에 영향을 받음)

            Time.timeScale = 1f; // 원래의 timeScale 값으로 복원

        }*/

    //이건 다음 패턴으로 옮김
    private IEnumerator PatternChange()
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
        SceneManager.LoadScene("SnowBoss3");
    }

    //랜덤으로 패턴 시작하기. 과거 나온 패턴은 리스트에서 삭제한다. 
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

    //빨간색 창이 3초간 떠서 경고함. 
    private void ToggleRandomRedFlag()
    {
        int randomIndex = Random.Range(0, warningPatterns.Count); // 랜덤한 인덱스 생성

        // 선택된 경고 패턴 활성화
        redFlag[randomIndex].SetActive(true);

        // 3초 후에 경고 패턴을 다시 비활성화
        StartCoroutine(DisableRedFlagAfterDelay(3f, randomIndex));
    }

    //3초후 경고를 없애고, 함정도 비활성화 한다. 
    private IEnumerator DisableRedFlagAfterDelay(float delay, int randomIndex)
    {
        yield return new WaitForSeconds(delay);

        foreach (GameObject redFlag in redFlag)
        {
            redFlag.SetActive(false);
        }

        //바로 경고 패턴 활성화 (톱날)
        StartCoroutine(EnableWarningPatternAfterDelay(randomIndex));
    }

    //톱날을 활성화 한다. warning pattern이라한건 씬에서 톱날을 warningPattern이라고 했다. 이것도 warningPattern 대신 톱날 (영어롤 모름...) 관련으로 바꾸면 좋을듯.
    private IEnumerator EnableWarningPatternAfterDelay(int randomIndex)
    {

        warningPatterns[randomIndex].SetActive(true);
        yield return new WaitForSeconds(1f); // 2초 대기

        warningPatterns[randomIndex].SetActive(false); // 경고 패턴 비활성화
        animator.SetBool("isCollectEnergy", false);

    }

    //솔직히 패턴을 전체적으로 갈아엎고 싶다. 잘 만든 패턴이라는 생각이 안듬... 운 적 요소보다는 컨트롤이 요하고 재미있었으면 좋겠다. 
    // 패턴 함수들 (구현 필요)
    private void Pattern1()
    {
        // 패턴 1 실행 코드
        float current = slHP.value;
        enemyPatterns[0].SetActive(true);
        StartCoroutine(SlidingFireCoroutine(current));
    }

    //슬라이딩 하는 fire다. 
    //이 함수가 다른 함수에 비해 복잡한 이유는... 공주 animator도 바꿔야하고 spawn도 시켜줘야하기 때문이다. 다른 것들은 enable로 해결봤다. 
    //지금 생각하니까 흠, 여기다 넣지 말고 pattern1 parent obstacle에 넣었어도 좋을 것 같다. 
    private IEnumerator SlidingFireCoroutine(float current)
    {
        while (slHP.value > current - 9f) // 무한 반복 (코루틴을 직접 중지하기 전까지)
        {
            //공주의 애니메이션.
            animator.SetBool("isHitTable", true);
            yield return new WaitForSeconds(Random.Range(2f, 4f)); // 랜덤한 시간 대기
            animator.SetBool("isHitTable", false);

            //총 두가지 패턴이 있는데 가운데에서 시작하거나 양쪽에서 시작하거나.
            int randomPrefabIndex = Random.Range(0, availableFire2Prefabs.Count);
            GameObject selectedFire2Prefab = availableFire2Prefabs[randomPrefabIndex];

            Vector3 fixedPosition = new Vector3(0f, -3f, 0f); // 고정된 위치에서 스폰됨.

            GameObject spawnedFire2 = Instantiate(selectedFire2Prefab, fixedPosition, selectedFire2Prefab.transform.rotation);

            yield return new WaitForSeconds(1f); // 일정 시간 대기

            Destroy(spawnedFire2); // 프리팹 제거
        }
    }


    private void Pattern2()
    {
        // 패턴 2 실행 코드 이경우 스크립트를 obstacle에 추가했다. 
        enemyPatterns[1].SetActive(true);
    }

    private void Pattern3()
    {
        // 패턴 3 실행 코드
        enemyPatterns[2].SetActive(true);
    }

    private void Pattern4()
    {
        // 패턴 4 실행 코드
        float current = slHP.value; //현재
        StartCoroutine(ThrowingGlass(current));

    }
    //ThrowingGlass. 이 패턴이 제일 재미없다. 그냥 나오는  glass를 피하면 되기 때문. (가만히 있어도 됨)
    //이것도 그냥 parent에 부착하면 편했을듯.
    private IEnumerator ThrowingGlass(float current)
    {
        enemyPatterns[3].SetActive(true);

        while (!isDone)
        {
            if (slHP.value <= current - 10f) //한 5초 남았을때 1초 동안 탄막 멈추고 주인공향해 날아감  이때 주인공 안 움직이면 죽음
            {
                enemyPatterns[3].SetActive(false);
                enemyPatterns[4].SetActive(true);
                isDone = true;
            }
            yield return null;
        }
    }
}
