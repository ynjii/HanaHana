using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class SnowBossClear : MonoBehaviour
{
    bool giveItemOnce = false;
    Animator anim;
    //페이드아웃 판넬 UI 
    [SerializeField] private Image fadeOutPanel;
    //페이드아웃 bool 변수
    private bool fade_out = false;
    //타이머
    private float timer = 0;
    [SerializeField] AudioSource[] audio;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isCalm", true);
        //페이드아웃 켜지고
        fade_out = true;
        //페이드아웃 판넬의 부모오브젝트(캔버스) 가져옴
        Transform parent_trans = fadeOutPanel.gameObject.transform.parent;
        //켜주기
        parent_trans.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //아이템을 얻었는가?
            string realItem = PlayerPrefs.GetString("RealItem");
            switch (realItem)
            {
                case "SnowWhite":
                    if (!giveItemOnce)
                    {
                        audio[0].Play();
                        giveItemOnce = true;
                    }
                    anim.SetBool("isCalm", false);
                    anim.SetBool("isClear", true);
                    break;
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //페이드아웃 조건변수 켜지면
        if (fade_out)
        {
            //타이머 키고
            timer += Time.deltaTime;
            //색 밝아지게
            fadeOutPanel.color += new Color(0, 0, 0, -0.01f);
        }
        if (timer <= 0f)
        {
            fade_out = false;
        }
    }
}
