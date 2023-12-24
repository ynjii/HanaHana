using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Pattern3 : MonoBehaviour
{

    [SerializeField] List<GameObject> _patterns;
    [SerializeField] Camera _camera;
    [SerializeField] BossHP _bossHP;

    [SerializeField] GameObject _patternChangeGO;
    [SerializeField] private Animator _bossAnim;
    [SerializeField] private GameObject _fadeOutUI;
    Player _player;
    bool isEnd = false;
    [SerializeField] GameObject startText;
    [SerializeField] GameObject naruto;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        // StartCoroutine(InvertScene());
        StartCoroutine(PatternExecute());
    }

    private void Update()
    {
        if (_bossHP.currentHP <= 0 && (!GameManager.instance.isGameover||_player.player_state!=Define.PlayerState.Damaged))
        {
            StartCoroutine(PatternChange());
        }
        //★
        _player.Invincibility = true;
    }

    IEnumerator PatternChange()
    {
        foreach (var pattern in _patterns)
        {
            pattern.SetActive(false);
        }
        
        _player.Invincibility = true;
        _patternChangeGO.SetActive(true);
        
        // ī�޶� shaking
        _camera.transform.DOShakePosition(3f, new Vector3(0.1f, 0.1f, 0));

        // ���� �ִϸ��̼� ����
        _bossAnim.SetBool("isHideEye", true);
        yield return new WaitForSeconds(1f);
        naruto.SetActive(true);
        yield return new WaitForSeconds(2f);
        _fadeOutUI.SetActive(true);
        
        SceneManager.LoadScene("SnowBoss4");
    }

    IEnumerator BossRaiseHandAnim()
    {
        _bossAnim.SetBool("isRaiseHand", true);
        yield return new WaitForSeconds(1.0f);
        _bossAnim.SetBool("isRaiseHand", false);
    }
    IEnumerator InvertScene()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);
            CameraController cameraController = _camera.GetComponent<CameraController>();
            cameraController.isReverse = !cameraController.isReverse;
            _bossAnim.SetBool("isRaiseHand", true);
            yield return new WaitForSeconds(2.0f);
            _bossAnim.SetBool("isRaiseHand", false);
        }
    }

    IEnumerator PatternExecute()
    {
        List<int> executedPatterns = new List<int>();
        
        for (int i = 0; i < _patterns.Count; i++)
        {
            int rand = Random.Range(0, _patterns.Count);
            if (executedPatterns.Contains(rand))
            {
                continue;
                i--;
            }
            //초반멘트
            if (i == 0)
            {
                StartCoroutine(StartText(rand));
            }
            executedPatterns.Add(rand);
            //짠탄제거
            DeleteCloneObjects();
            //2초쉬기
            yield return new WaitForSeconds(2f);
            //패턴활성화
            PatternActivate(rand);
            if (rand == 3)
            {
                //3초무적
                StartCoroutine(PlayerInvincibility(3f));
            }
            else
            {
                //1초무적
                StartCoroutine(PlayerInvincibility(1f));
            }
            Debug.Log(rand + " rand값");
            foreach(int value in executedPatterns)
            {
                Debug.Log("List 값 "+value);
            }
            yield return new WaitForSeconds(14.5f);
            
        }
    }

    IEnumerator StartText(int rand)
    {
        //초반멘트        
        startText.SetActive(true);
        if (rand == 3)
        {
            startText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Go to circle!";
        }
        yield return new WaitForSeconds(2f);
        startText.SetActive(false);
    }

    void PatternActivate(int patternNum)
    {
        for (int i = 0; i < _patterns.Count; i++)
        {
            if (i == patternNum)
            {
                DeleteCloneObjects();
                _patterns[i].SetActive(true);
                continue;
            }
            _patterns[i].SetActive(false);
        }

        StartCoroutine(BossRaiseHandAnim());
        //StartCoroutine(PlayerInvincibility());
    }

    IEnumerator PlayerInvincibility(float time)
    {
        SpriteRenderer playerSprite=_player.GetComponent<SpriteRenderer>();
        playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.5f);        
        _player.Invincibility = true;
        yield return new WaitForSeconds(time);
        playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
        _player.Invincibility = false;
    }

    //짠탄제거
    public void DeleteCloneObjects()
    {
        // 씬 내의 모든 게임 오브젝트 가져오기
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            //클론된 애면
            if (IsClone(obj))
            {
                Destroy(obj); // 클론 오브젝트 삭제
            }
        }
    }

    //클론된애인지 판별
    private bool IsClone(GameObject obj)
    {
        // 이름에 "(Clone)" 문자열이 포함되어 있는지 검사
        return obj.name.Contains("(Clone)");
    }
}
