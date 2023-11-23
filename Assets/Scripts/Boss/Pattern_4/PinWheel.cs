using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 바람개비(백설보스패턴4) 스크립트
/// 바람개비 프리팹 참고
/// Assets/Resources/Prefabs/SnowBoss4/Pinwheel.prefab
/// </summary>
public class PinWheel : MonoBehaviour
{
    //써클패턴 객체 생성(함수 갖다쓰게)
    CirclePattern pattern;
    //도는 스피트
    [SerializeField] public float rotateSpeed;
    //쏘는 타이밍
    [SerializeField] float launch_time;
    //쏘는 힘
    [SerializeField]
    private float launchForce;
    //퍼져나가는 정도
    [SerializeField]
    private float spreadFactor;
    //타이머
    float launch_timer = 0;
    //사라지는타임
    float destroy_time = 0;

    // Start is called before the first frame update
    void Start()
    {
        pattern = new CirclePattern();
    }

    // Update is called once per frame
    void Update()
    {

        launch_timer += Time.deltaTime;
        destroy_time += Time.deltaTime;
        //계속돌기
        Rotate();

        if (launch_timer >= launch_time)
        {
            //쏘는 소리
            GetComponent<AudioSource>().Play();
            //바깥으로퍼져나감
            pattern.LaunchToOutside(this.gameObject.transform, launchForce, spreadFactor);
            launch_timer = 0;
        }
        if (destroy_time >= 10)
        {
            //10초지나면 오브젝트삭제
            Destroy(gameObject);
        }

    }
    private void Rotate()
    {
        transform.Rotate(0, 0, -Time.deltaTime * rotateSpeed, Space.Self);
    }
}
