using UnityEngine;
using UnityEngine.UI;
using static Define;


public class BossHP : MonoBehaviour
{
    public Slider slHP; // Inspector에서 Slider 컴포넌트를 할당해야 합니다.
    public GameObject fillArea;

    public GameObject player;
    public float decreaseRate = 1.0f; // 감소율 조정을 위한 변수
    public float currentHP; // 현재 HP 값
    [SerializeField] private HPType hpType;
    private Boss boss_script;
    public enum HPType
    {
        Time, //시간따라
        HP
    }
    void Start()
    {

        if (hpType == HPType.HP) //이거는 유진님 코드
        {
            boss_script = GameObject.FindWithTag("Boss").GetComponent<Boss>();
            slHP.maxValue = boss_script.boss_hp;
            currentHP = boss_script.boss_hp;// 보스 피: 초기 피
        }
        else
        {
            currentHP = slHP.maxValue; // 초기 HP 값을 최대값으로 설정
        }
    }


    void Update()
    {
        if (player == null || player.layer != 7)
        {
            DecreaseHP(hpType);
        }

        //이거 근데 update마다 돌아가서 
        //1. 굳이 else를 만들어줘야하나? 0되면 어차피 다음으로 넘어가니까 굳이 신경쓸 필요는 없어보임.
        //2. 이거 없앤 이유가 다 사라져도 피가 남은 것처럼 보임. 
        if (currentHP <= 0)
            fillArea.SetActive(false);
        else
            fillArea.SetActive(true);
    }

    private void DecreaseHP(HPType hpType)
    {
        switch (hpType)
        {
            case HPType.Time:
                // 시간 경과에 따라 HP 값을 감소시킴
                currentHP -= decreaseRate * Time.deltaTime;
                currentHP = Mathf.Max(currentHP, 0); // 최소값은 0으로 제한

                slHP.value = currentHP; // 슬라이더의 값에 현재 HP 값을 할당
                break;

            case HPType.HP:
                currentHP = boss_script.boss_hp;
                currentHP = Mathf.Max(currentHP, 0); // 최소값은 0으로 제한

                slHP.value = currentHP; // 슬라이더의 값에 현재 HP 값을 할당

                break;

        }
    }
}