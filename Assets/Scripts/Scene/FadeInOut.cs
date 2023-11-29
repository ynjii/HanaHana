using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    private bool once = false;
    //페이드아웃 판넬 UI 
    private Image fadeOutPanel;

    public enum FadeType
    {
        FadeIn,
        FadeOut
    }
    [SerializeField] FadeType fadeType;
    [SerializeField] float timeSet = 3f;
    //타이머
    private float timer = 0;
    //시작
    [SerializeField] bool startFade;
    //외부 코드에서 시작시킬 경우 대비해 캡슐화
    public bool StartFade
    {
        get
        {
            return startFade;
        }
        set
        {
            startFade = value;
        }
    }
    public FadeType _FadeType
    {
        get
        {
            return fadeType;
        }
        set
        {
            fadeType = value;
        }
    }
    //증감량
    [SerializeField] float plusMinusAmount = 0.01f;
    private Transform panel;
    //페이드아웃 판넬의 부모오브젝트(캔버스)
    private Transform panelCanvus;
    void Awake()
    {
        panelCanvus = transform.GetChild(0);
        panel = panelCanvus.transform.GetChild(0);
        fadeOutPanel = panel.gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startFade)
        {
            if (!once)
            {
                //켜주기
                panelCanvus.gameObject.SetActive(true);
                //색 초기화
                if (fadeType == FadeType.FadeIn)
                {
                    fadeOutPanel.color = new Color(fadeOutPanel.color.r, fadeOutPanel.color.g, fadeOutPanel.color.b, 1f);
                }
                else
                {
                    fadeOutPanel.color = new Color(fadeOutPanel.color.r, fadeOutPanel.color.g, fadeOutPanel.color.b, 0f);
                }
                once = true;
            }
            switch (fadeType)
            {
                // 페이드아웃 조건변수 켜지면
                case FadeType.FadeOut:
                    //타이머 키고
                    timer += Time.deltaTime;
                    //색 어두워지게
                    fadeOutPanel.color += new Color(0, 0, 0, plusMinusAmount);
                    if (timer >= timeSet)
                    {
                        startFade = false;
                        fadeOutPanel.color = new Color(fadeOutPanel.color.r, fadeOutPanel.color.g, fadeOutPanel.color.b, 1f);
                    }

                    break;
                case FadeType.FadeIn:

                    //타이머 키고
                    timer += Time.deltaTime;
                    //색 밝아지게
                    fadeOutPanel.color += new Color(0, 0, 0, -plusMinusAmount);
                    if (timer >= timeSet)
                    {
                        startFade = false;
                        fadeOutPanel.color = new Color(fadeOutPanel.color.r, fadeOutPanel.color.g, fadeOutPanel.color.b, 0f);
                    }

                    break;
            }

        }
    }
}
