using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pattern3 : MonoBehaviour
{

    [SerializeField] List<GameObject> _patterns;
    [SerializeField] Camera _camera;
    [SerializeField] BossHP _bossHP;

    [SerializeField] GameObject _patternChangeGO;
    Player _player;
    bool isEnd = false;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartCoroutine(InvertScene());
        StartCoroutine(PatternExecute());
    }

    private void Update()
    {
        // 보스 체력/시간 변수가 0이하가 되면 패턴 넘기는 처리를 시작
        if (_bossHP.currentHP <= 0 && !isEnd)
        {
            isEnd= true;
            StartCoroutine(PatternChange());
        }
    }

    IEnumerator PatternChange()
    {
        StopAllCoroutines();
        _patternChangeGO.SetActive(true); 
        
        // 카메라 shaking
        _camera.transform.DOShakePosition(3, 1);

        // 보스 애니메이션 변경
        // TODO : 소연이가 애니메이션 추가해주면 작성하기


        // 불 스프라이트는 자동 재생
        // 다음 씬 로드 : 보스 애니메이션 끝나고 이동
        yield return new WaitForNextFrameUnit();
        SceneManager.LoadScene("SnowBoss4");

    }

    IEnumerator InvertScene()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);
            CameraController cameraController = _camera.GetComponent<CameraController>();
            cameraController.isReverse = !cameraController.isReverse;
        }
    }

    IEnumerator PatternExecute()
    {
        HashSet<int> executedPatterns = new HashSet<int>();

        for (int i = 0; i<_patterns.Count; i++)
        {
            int rand = Random.Range(0, 5);
            if (executedPatterns.Contains(rand))
            {
                i--;
                continue;
            }
            executedPatterns.Add(rand);
            PatternActivate(rand);
            yield return new WaitForSeconds(12);
        }
    }

    void PatternActivate(int patternNum)
    {
        for(int i = 0; i<_patterns.Count; i++)
        {
            if(i == patternNum)
            {
                _patterns[i].SetActive(true);
                continue;
            }
            _patterns[i].SetActive(false);
        }
    }
}
