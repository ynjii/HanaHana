using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Launch_Fire launcher0_script;
    private Launch_Fire launcher1_script;
    public GameObject[] launchers;
    private float hit_time=0;
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

    //랜덤값따라 패턴호출
    //패턴 시작하면 time=0, time+=Time.deltaTime
    //time이 0일때만 랜덤값 다시 넣기. & switch 함수 호출

    //23.05, 0.27: 초기위치
    private void Awake()
    {
        launcher0_script = launchers[0].GetComponent<Launch_Fire>();
        launcher1_script = launchers[1].GetComponent<Launch_Fire>();
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
    }

    // Update is called once per frame
    void Update()
    {
        //맞으면 빨간색 처리
        hit_time += Time.deltaTime;
        if (hit_time>=0.15f)
            mirror_renderer.color= new Color(1,1,1,0.7f);

        Pattern1();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            mirror_renderer.color = new Color(1, 0.54f, 0.54f, 0.77f);
            boss_hp -= P_bullet_script.bullet_damage;
            hit_time = 0f;
        }

    }

    private void Pattern1()
    {
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

}
