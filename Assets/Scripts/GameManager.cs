using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //싱글턴을 할당할 전역변수
    public AudioSource[] audioSources;

    [SerializeField] private Vector3 respawnPoint = new Vector3(-9.16f, -0.48f, 0f); // 플레이어가 리스폰할 체크포인트 위치
    private int death_count = 0;//죽은 횟수
    public bool isGameover = false; //게임오버 상태
    public TextMeshProUGUI death_text;//죽은 횟수를 출력할 UI 텍스트
    public GameObject gameoverUI; //게임오버 시 활성화할 UI 게임오브젝트
    public GameObject finishUI; //게임이 끝났을 시 활성화할 UI 게임오브젝트
    public GameObject player;//플레이어
    public GameObject SaveLoad;
    public GameObject exitPanel;
    public GameObject Sky;
    public Sprite[] backgrounds;
    //배경 변경 위한 변수
    private bool is_Castle;
    private bool is_Mirror;

    /// <summary>
    /// 게임 시작과 동시에 싱글턴을 구성
    /// </summary>



    void Awake()
    {
        //싱글턴 변수 instance가 비어 있는가?
        if (instance == null)
        {
            //instance가 비어 있다면(null) 그곳에 자기 자신을 할당
            instance = this;
        }
        else
        {
            Debug.LogWarning("씬에 두 개 이상의 개임 매니저가 존재합니다.");
            Destroy(gameObject);
        }

    }

    void Start()
    {
                                                     //리스폰 위치로 플레이어 위치를 reset함.
        /*
        if (SaveLoad.GetComponent<SaveLoad>().LoadRespawn("respawn") != Vector3.zero)
        {
            player.transform.position = SaveLoad.GetComponent<SaveLoad>().LoadRespawn("respawn");
        }
        Debug.Log("Mrespawnpoint" + player.transform.position);*/
        player.GetComponent<Player>().ChangeSprites();
    }

    void Update()
    {
        if (isGameover && Input.GetMouseButtonDown(0))
        {
            //게임오버 상태에서 마우스 왼쪽 버튼을 클릭하면 현재 씬 재시작
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

         //만약 뒤로가기키 눌렀을때
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f; //시간 정지
            exitPanel.SetActive(true); //exit팝업착 띄우기
        }

        //배경바꾸기
        if (SceneManager.GetActiveScene().name == "SnowWhite")
        {
            if (player.transform.position.x < 382)
            {

                Image sky_image = Sky.GetComponent<Image>();
                sky_image.sprite = backgrounds[0];
                audioSources[0].enabled=true;
                audioSources[1].enabled=false;
                audioSources[2].enabled=false;
                audioSources[3].enabled = false;
            }

            if (player.transform.position.x >= 382 && player.transform.position.x < 650)
            {
                Image sky_image = Sky.GetComponent<Image>();
                sky_image.sprite = backgrounds[1];
                audioSources[1].enabled=true;
                audioSources[2].enabled = false;
                audioSources[0].enabled = false ;
                audioSources[3].enabled = false;
            }

            if (player.transform.position.x >= 650)
            {
                Image sky_image = Sky.GetComponent<Image>();
                sky_image.sprite = backgrounds[2];
                audioSources[2].enabled=true;
                audioSources[0].enabled=false;
                audioSources[1].enabled=false;
                audioSources[3].enabled = true;
            }
        }
    }

    /// <summary>
    /// 플레이어 캐릭터 사망 시 게임오버를 실행하는 메서드
    /// </summary>
    public void OnPlayerDead()
    {
        if (!isGameover)
        {
            //현재 상태를 게임오버 상태로 변경
            isGameover = true;
            //죽은 횟수를 증가

            death_count = SaveLoad.GetComponent<SaveLoad>().LoadDeathCount("death") + 1;

            SaveLoad.GetComponent<SaveLoad>().SaveDeathCount("death", death_count);
            death_text.text = "Death : " + death_count++;
            //게임오버 UI를 활성화
            gameoverUI.SetActive(true);
        }
    }

    public void OnPlayerFinish()
    {
        if (!isGameover)
        {
            finishUI.SetActive(true);
            // 딜레이를 위한 Invoke 실행
            float delaySeconds = 2f; // 2초
            Invoke("HideFinishUI", delaySeconds); // 지정된 시간(delaySeconds) 후에 HideFinishUI 메서드를 실행합니다.

        }
    }
    //Finish처리 함수
    private void HideFinishUI()
    {
        finishUI.SetActive(false);
    }

    public void ExitYes()
    {
        Application.Quit();
    }

    public void ExitNo()
    {
        Time.timeScale = 1f;
        exitPanel.SetActive(false);
    }
}