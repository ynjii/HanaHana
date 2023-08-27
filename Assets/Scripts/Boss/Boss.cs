using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;
using DG.Tweening;
public class Boss : MonoBehaviour
{
    public AudioSource[] audioSources;
    public GameObject clearUI;

    [SerializeField] GameObject[] _patternChangeGO;
    private Launch_Fire launcher0_script;
    private Launch_Fire launcher1_script;
    private Launch_Fire guidedMissleLuncher1_script;
    private Launch_Fire guidedMissleLuncher2_script;
    public GameObject[] launchers;
    [SerializeField]
    private List<GameObject> mirrors;
    
    private float hit_timer = 0;
    private float pattern_timer = 0;
    private const float PATTERN_TIME = 10;
    public SpriteRenderer mirror_renderer;
    [SerializeField]
    public float boss_hp;
    private GameObject P_bullet;
    private Fire P_bullet_script;
    [SerializeField]
    private float move_speed;
    private float anim_timer;
    private bool turn_on_anim_timer = false;
    //버드 패턴에서 쓰는 변수라는 뜻에서 말머리 붙임
    private List<Vector3> B_target_positions = new List<Vector3>();
    private int B_current_target_index = 0;

    //고블린 패턴에서 쓰는 변수라는 뜻에서 말머리 붙임
    private List<Vector3> G_target_positions = new List<Vector3>();
    private int G_current_target_index = 0;
    private Animator anim;
    private Animator mirror_anim;
    private Animator naruto_anim;
    public BossState boss_state = new BossState();
    private Vector3 Boss_initial_position = new Vector3(23.05f, 0.27f, 0);
    private List<Vector3> Launcher_initial_position = new List<Vector3>()
    {
        new Vector3(21.9f, -0.25f),
        new Vector3(21.9f, -0.25f)
    };

    //주금
    private bool is_dead = false;
    private bool once=false;
    private void Awake()
    {
        Transform child_transform = this.transform.GetChild(1);
        mirror_anim =child_transform.GetComponent<Animator>();
        child_transform=this.transform.GetChild(2);
        naruto_anim=child_transform.GetComponent<Animator>();

        anim = GetComponent<Animator>();
        launcher0_script = launchers[0].GetComponent<Launch_Fire>();
        launcher1_script = launchers[1].GetComponent<Launch_Fire>();
        guidedMissleLuncher1_script = launchers[2].GetComponent<Launch_Fire>();
        guidedMissleLuncher2_script = launchers[3].GetComponent<Launch_Fire>();

        P_bullet = GameObject.FindWithTag("bullet").GetComponent<Launch_Fire>().fire;
        P_bullet_script = P_bullet.GetComponent<Fire>();

        // 원하는 목표 위치들을 리스트에 추가
        B_target_positions.Add(new Vector3(23.05f, 0.27f, 0));
        B_target_positions.Add(new Vector3(19.93f, 3.08f, 0));
        B_target_positions.Add(new Vector3(17.52f, 2.07f, 0));
        B_target_positions.Add(new Vector3(18.13f, 0.93f, 0));
        B_target_positions.Add(new Vector3(15.58f, -0.69f, 0));
        B_target_positions.Add(new Vector3(16.46f, -2.62f, 0));
        B_target_positions.Add(new Vector3(18.57f, -1.87f, 0));
        B_target_positions.Add(new Vector3(20.06f, -3.58f, 0));
        B_target_positions.Add(new Vector3(23.48f, -1.96f, 0));

        // 원하는 목표 위치들을 리스트에 추가
        G_target_positions.Add(new Vector3(23.05f, 0.27f, 0));
        G_target_positions.Add(new Vector3(22.07f, 3.52f, 0));
        G_target_positions.Add(new Vector3(23.72f, 2.33f, 0));
        G_target_positions.Add(new Vector3(17.22f, -0.66f, 0));
        G_target_positions.Add(new Vector3(18.92f, -2f, 0));
        G_target_positions.Add(new Vector3(21.55f, -1.85f, 0));

        RandomPattern();

    }

    // Update is called once per frame
    void Update()
    {
        if (clearUI.active == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //마우스 왼쪽 버튼을 클릭하면 현재 씬 재시작
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
                
        if (!is_dead) {
            //맞으면 빨간색 처리
            hit_timer += Time.deltaTime;
            if (hit_timer >= 0.15f)
                mirror_renderer.color = new Color(1, 1, 1, 0.7f);
            if (turn_on_anim_timer)
                anim_timer += Time.deltaTime;

            pattern_timer += Time.deltaTime;
            if (pattern_timer >= PATTERN_TIME)
            {
                DeleteCloneObjects();
                WhenPatternChangeSetting();
                RandomPattern();
                naruto_anim.SetBool("pattern_change", true);
                pattern_timer = 0;
                turn_on_anim_timer = true;
            }
            if (anim_timer >= 1f && naruto_anim.GetBool("pattern_change"))
            {
                naruto_anim.SetBool("pattern_change", false);
                anim_timer = 0;
                turn_on_anim_timer = false;
            }

            if (boss_state == BossState.pattern4_1 || boss_state == BossState.pattern4_4)
            {
                PolygonMove();
            }
            else if (boss_state == BossState.pattern4_5)
            {
                ZigZagMove();
            }
        }

        //깼을 때
        if (boss_hp <= 0)
        {
            Vector3 targetPosition = Boss_initial_position;
            // 현재 위치와 목표 위치 사이의 거리 계산
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

            // 목표 위치로 이동
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * move_speed);

            if (!once)
            {
                Dead();
                once = true;
            }
        }      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            mirror_renderer.color = new Color(1, 0.54f, 0.54f, 0.77f);
            boss_hp -= P_bullet_script.bullet_damage;
            hit_timer = 0f;
        }

    }

    private void Pattern1()
    {
        boss_state = BossState.pattern4_1;
        launcher0_script.fires_index = 0;
        launcher1_script.fires_index = 0;
    }

    private void Pattern2()
    {
        boss_state = BossState.pattern4_2;
        //원형 사과
        launcher0_script.fires_index = 1;

        //다이아 사과
        launcher1_script.fires_index = 1;
    }

    private void Pattern3()
    {
        boss_state = BossState.pattern4_3;
        //바람개비
        launcher0_script.fires_index = 2;
        //불꽃놀이
        launcher1_script.fires_index = 2;
    }

    private void Pattern4()
    {
        boss_state = BossState.pattern4_4;
        //원형애플
        launcher0_script.fires_index = 1;
        launcher1_script.fires_index = -1;//아무것도 안 한다는 뜻.
        //조준탄
        launchers[2].SetActive(true);
        launchers[3].SetActive(true);

        guidedMissleLuncher1_script.fires_index = 0;
        guidedMissleLuncher2_script.fires_index = 0;


    }

    private void Pattern5()
    {
        boss_state = BossState.pattern4_5;

        //원형애플(C모양)
        launcher0_script.fires_index = 1;
        //고블린 던지기
        launcher1_script.fires_index = 3;


    }

    private void RandomPattern()
    {
        int rand = Random.Range(0, 5);
        switch (rand)
        {
            case 0:
                Pattern1();
                break;
            case 1:
                Pattern2();
                break;
            case 2:
                Pattern3();
                break;
            case 3:
                Pattern4();
                break;
            case 4:
                Pattern5();
                break;
        }
    }

    private void WhenPatternChangeSetting()
    {
        GetComponent<AudioSource>().Play();
        this.launcher0_script.Cool_Time = 0;
        this.launcher1_script.Cool_Time = 0;
        this.transform.position = Boss_initial_position;
        launchers[2].SetActive(false);
        launchers[3].SetActive(false);
        launchers[0].transform.position = Launcher_initial_position[0];
        launchers[1].transform.position = Launcher_initial_position[1];
    }
    private void ZigZagMove()
    {
        Vector3 targetPosition = G_target_positions[G_current_target_index];
        // 현재 위치와 목표 위치 사이의 거리 계산
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        // 일정 거리 내에 있으면 다음 목표 위치로 변경
        if (distanceToTarget <= 0.1f) // 예시로 0.1f를 사용, 원하는 값으로 조정 가능
        {
            G_current_target_index = (G_current_target_index + 1) % G_target_positions.Count;
            targetPosition = G_target_positions[G_current_target_index];
        }

        // 목표 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * move_speed);

    }

    private void PolygonMove()
    {

        Vector3 targetPosition = B_target_positions[B_current_target_index];
        // 현재 위치와 목표 위치 사이의 거리 계산
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        // 일정 거리 내에 있으면 다음 목표 위치로 변경
        if (distanceToTarget <= 0.1f) // 예시로 0.1f를 사용, 원하는 값으로 조정 가능
        {
            B_current_target_index = (B_current_target_index + 1) % B_target_positions.Count;
            targetPosition = B_target_positions[B_current_target_index];
        }

        // 목표 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * move_speed);

    }

    //짠탄제거
    public void DeleteCloneObjects()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>(); // 씬 내의 모든 게임 오브젝트 가져오기

        foreach (GameObject obj in allObjects)
        {
            if (IsClone(obj))
            {
                Destroy(obj); // 클론 오브젝트 삭제
            }
        }
    }

    private bool IsClone(GameObject obj)
    {
        return obj.name.Contains("(Clone)"); // 이름에 "(Clone)" 문자열이 포함되어 있는지 검사
    }

    private void Dead()
    {
        DeleteCloneObjects();
        is_dead = true;
        launchers[0].SetActive(false);
        launchers[1].SetActive(false);
        if (launchers[2].active || launchers[3].active)
        {
            launchers[2].SetActive(false);
            launchers[3].SetActive(false);
        }
        //쿠광쾅콰광(소리+ 화면흔들림+ 폭발애니메이션: 이미 구현한 코더분들거 쌔벼오기)
        StartCoroutine(PatternChange());
        
        //거울쨍그랑(쨍그랑 애니메이션 후->거울 deactive-> 원형 프리팹 이용해 거울 파편 퍼져나가기)   
        Invoke("mirrorDeactive", 11f);

        //시연용 UI띄우기
        Invoke("showClearUI", 20f);
        //페이드인페이드아웃(이미 구현 쌔벼오기) white ver. -> 씬이동(잠잠해짐 씬으로 이동)
    }


    private void mirrorDeactive()
    {
        audioSources[0].Stop();
        foreach (GameObject obj in _patternChangeGO)
        {
            obj.SetActive(false);
        }
        mirror_anim.SetBool("isDead", true);
        audioSources[1].Play();
        anim.SetBool("isHideEye", false);
        mirror_anim.gameObject.SetActive(false);
        CirclePattern circle_pattern = new CirclePattern();
        foreach (GameObject obj in mirrors)
        {
            obj.SetActive(true);
        }
        circle_pattern.CircleLaunch(mirrors, this.transform,10);
        audioSources[1].Play();
    }

    private void showClearUI()
    {
        clearUI.SetActive(true);
        audioSources[2].Play();        
    }

    IEnumerator PatternChange()
    {
        StopAllCoroutines();
        foreach(GameObject obj in _patternChangeGO)
        {
            obj.SetActive(true);
        }

        // 카메라 shaking
        Camera.main.transform.DOShakePosition(10, 3);

        // 보스 애니메이션 변경
        anim.SetBool("isHideEye", true);

        // 불 스프라이트는 자동 재생

        //폭발사운드
        audioSources[0].loop = true;
        audioSources[0].Play();
        

        // 다음 씬 로드 : 보스 애니메이션 끝나고 이동
        yield return new WaitForSeconds(10);
    }

}
