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
    private SnowBoss4 boss_script;
    public enum HPType
    {
        Time, //시간따라
        HP
    }
    void Start()
    {

        if (hpType == HPType.HP)
        {
            boss_script = GameObject.FindWithTag("Boss").GetComponent<SnowBoss4>();
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