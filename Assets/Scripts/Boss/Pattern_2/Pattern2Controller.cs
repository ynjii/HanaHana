using DG.Tweening;
using System;
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
    //패턴을 넣습니다. 패턴별로 부모 스크립트에 넣고, 비활성화했다가 활성화 되면 움직이는 식으로 만들었어요 
    private List<string> bossAnimation = new List<string> { "isCollectingEnergy" };

    public List<GameObject> bossPatterns = new List<GameObject>();
    [SerializeField] Camera cam;

    [SerializeField] GameObject sceneChangeGO;

    public Slider slHP; //보스 피받아오기

    private float currentTime; //현재 시간
    private float currentHP;//현재 HP 

    private int previousIndex = -1;
    public Animator animator;

    private void Start()
    {
        currentHP = slHP.maxValue; //슬라이더 시작값 받아오기
        currentTime = slHP.maxValue;

        StartNextPattern();
    }

    private void Update()
    {
        if (slHP.value <= 0)
        {
            StartCoroutine(ChangetoNextScene()); //이거는 전체 보스맵 4개의 패턴에서의 패턴. scene change로 바꾸면 좋을 것 같음.
        }

        if (slHP.value <= currentHP - 17f && bossPatterns.Count > 0) //17초 지나면, 그리고 가능한 패턴이 남아있다면
        {
            currentHP -= 17f;
            StartNextPattern(); //여기서는 보스맵 2 안의 패턴
        }

    }

    //이건 다음 패턴으로 옮김
    private IEnumerator ChangetoNextScene()
    {
        sceneChangeGO.SetActive(true);

        // 카메라 shaking
        cam.transform.DOShakePosition(3, 1);

        // 보스 애니메이션 변경
        animator.SetBool("isHideEye", true);

        // 불 스프라이트는 자동 재생
        // 다음 씬 로드 : 보스 애니메이션 끝나고 이동
        yield return new WaitForSeconds(4);
        animator.SetBool("isHideEye", false);
        SceneManager.LoadScene("SnowBoss3");
    }

    //랜덤으로 패턴 시작하기. 과거 나온 패턴은 리스트에서 삭제한다. 
    private void StartNextPattern()
    {

        // 이전 패턴을 제외한 패턴들로 리스트 갱신
        if (previousIndex != -1)
        {
            bossPatterns[previousIndex].SetActive(false);
            bossPatterns.RemoveAt(previousIndex);
        }

        // 랜덤한 패턴 선택
        int randomIndex = random.Next(bossPatterns.Count);
        // 선택된 패턴 실행
        bossPatterns[randomIndex].SetActive(true);

        // 이전 패턴 업데이트
        previousIndex = randomIndex;
    }

}
