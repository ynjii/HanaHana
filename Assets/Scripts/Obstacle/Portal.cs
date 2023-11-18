using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;
using UnityEngine.UI;
/// <summary>
/// 포탈스크립트.
/// 트리거로 움직입니다~
/// </summary>

public class Portal : MonoBehaviour
{
    //페이드아웃 판넬 UI 
    [SerializeField] private Image fadeOutPanel;
    //포탈 타면 넘어가는 씬 이름
    [SerializeField] private string _nextScene;
    //페이드아웃 bool 변수
    private bool fade_out = false;
    //타이머
    private float timer = 0;
    //트리거 처리되면
    private void OnTriggerEnter2D(Collider2D other)
    {
        //플레이어에만 반응하게
        if (!other.CompareTag("Player")) return;
        //페이드아웃 켜지고
        fade_out = true;
        //페이드아웃 판넬의 부모오브젝트(캔버스) 가져옴
        Transform parent_trans=fadeOutPanel.gameObject.transform.parent;
        //켜주기
        parent_trans.gameObject.SetActive(true);
    }

    private void Update()
    {
        //페이드아웃 조건변수 켜지면
        if (fade_out)
        {
            //타이머 키고
            timer += Time.deltaTime;
            //색 까매지게
            fadeOutPanel.color+= new Color(0,0,0,0.1f);
        }
        if (timer >= 1f)
        {
            //1초 되면 씬로드
            SceneManager.LoadScene(_nextScene);
        }
    }


}
