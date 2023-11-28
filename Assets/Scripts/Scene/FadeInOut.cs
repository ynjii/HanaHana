using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    //페이드아웃 판넬 UI 
    Image fadeOutPanel;

    public enum FadeType
    {
        FadeIn,
        FadeOut
    }
    [SerializeField] FadeType fadeType;
    [SerializeField] float timeSet=1f;
    //타이머
    private float timer = 0;
    //시작
    [SerializeField] bool startFade;

    //증감량
    [SerializeField] float plusMinusAmount=0.01f;
    
    void Awake()
    {
        fadeOutPanel = GetComponent<Image>();
        //페이드아웃 판넬의 부모오브젝트(캔버스) 가져옴
        Transform parent_trans = fadeOutPanel.gameObject.transform.parent;
        //켜주기
        parent_trans.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (fadeType)
        {
            // 페이드아웃 조건변수 켜지면
            case FadeType.FadeOut:
                if (startFade)
                {
                    //타이머 키고
                    timer += Time.deltaTime;
                    //색 어두워지게
                    fadeOutPanel.color += new Color(0, 0, 0, plusMinusAmount);
                    if (timer >= timeSet)
                    {
                        startFade = false;
                    }
                }
            break;
            case FadeType.FadeIn:
                if (startFade)
                {
                    //타이머 키고
                    timer += Time.deltaTime;
                    //색 밝아지게
                    fadeOutPanel.color += new Color(0, 0, 0, -plusMinusAmount);
                    if (timer >= timeSet)
                    {
                        startFade = false;
                    }
                }
                break;
        }
    }
}
