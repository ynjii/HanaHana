using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Boss : MonoBehaviour
{
    
    private Launch_Fire launcher0_script;
    private Launch_Fire launcher1_script;
    private Launch_Fire guidedMissleLuncher1_script;
    private Launch_Fire guidedMissleLuncher2_script;
    public GameObject[] launchers;
    private float hit_timer=0;
    private float pattern_timer=0;
    private const float PATTERN_TIME = 10;
    public SpriteRenderer mirror_renderer;
    [SerializeField]
    public float boss_hp;
    private GameObject P_bullet;
    private Fire P_bullet_script;
    [SerializeField]
    private float move_speed;

    //버드 패턴에서 쓰는 변수라는 뜻에서 말머리 붙임
    private List<Vector3> B_target_positions = new List<Vector3>();
    private int B_current_target_index = 0;

    //고블린 패턴에서 쓰는 변수라는 뜻에서 말머리 붙임
    private List<Vector3> G_target_positions = new List<Vector3>();
    private int G_current_target_index = 0;
    private Animator anim;
    public BossState boss_state=new BossState();
    private Vector3 initial_position = new Vector3(23.05f, 0.27f, 0);
    //랜덤값따라 패턴호출
    //패턴 시작하면 launch_time=0, launch_time+=Time.deltaTime
    //time이 0일때만 랜덤값 다시 넣기. & switch 함수 호출

    //23.05, 0.27: 초기위치
    private void Awake()
    {
        anim=GetComponent<Animator>();  
        launcher0_script = launchers[0].GetComponent<Launch_Fire>();
        launcher1_script = launchers[1].GetComponent<Launch_Fire>();
        guidedMissleLuncher1_script = launchers[2].GetComponent<Launch_Fire>();
        guidedMissleLuncher2_script = launchers[3].GetComponent<Launch_Fire>();

        P_bullet = GameObject.FindWithTag("bullet").GetComponent<Launch_Fire>().fire;
        P_bullet_script=P_bullet.GetComponent<Fire>();

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
        //맞으면 빨간색 처리
        hit_timer += Time.deltaTime;
        if (hit_timer>=0.15f)
            mirror_renderer.color= new Color(1,1,1,0.7f);

        pattern_timer += Time.deltaTime;
        if (pattern_timer >= PATTERN_TIME)
        {
            this.transform.position = initial_position;
            RandomPattern();
            anim.SetBool("pattern_change", true);
            anim.SetBool("pattern_change", false);
            pattern_timer = 0;
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
        WhenPatternChangeSetting();
        boss_state = BossState.pattern4_1;
        launcher0_script.fires_index = 0;
        launcher1_script.fires_index = 0;

        Vector3 targetPosition = B_target_positions[B_current_target_index];
        // 현재 위치와 목표 위치 사이의 거리 계산
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        // 일정 거리 내에 있으면 다음 목표 위치로 변경
        if (distanceToTarget <= 0.1f) // 예시로 0.1f를 사용, 원하는 값으로 조정 가능
        {
            B_current_target_index = (B_current_target_index+ 1) % B_target_positions.Count;
            targetPosition = B_target_positions[B_current_target_index];
        }

        // 목표 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * move_speed);
    }

    private void Pattern2()
    {
        WhenPatternChangeSetting();
        boss_state = BossState.pattern4_2;
        //원형 사과
        launcher0_script.fires_index = 1;

        //다이아 사과
        launcher1_script.fires_index = 1;
    }

    private void Pattern3()
    {
        WhenPatternChangeSetting();
        boss_state = BossState.pattern4_4;
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

    private void Pattern5()
    {
        WhenPatternChangeSetting();
        boss_state = BossState.pattern4_5;

        //원형애플(C모양)
        launcher0_script.fires_index = 1;
        //고블린 던지기
        launcher1_script.fires_index = 3;

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
        launchers[2].SetActive(false);
        launchers[3].SetActive(false);
        launchers[0].transform.position = new Vector3(-2.153016f, -1.04f, 0);
    }
}
