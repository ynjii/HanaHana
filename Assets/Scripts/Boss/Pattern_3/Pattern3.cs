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
    [SerializeField] private Animator _bossAnim;
    Player _player;
    bool isEnd = false;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        // StartCoroutine(InvertScene());
        StartCoroutine(PatternExecute());
    }

    private void Update()
    {
        // ���� ü��/�ð� ������ 0���ϰ� �Ǹ� ���� �ѱ�� ó���� ����
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
        
        // ī�޶� shaking
        _camera.transform.DOShakePosition(3, 1);

        // ���� �ִϸ��̼� ����
        _bossAnim.SetBool("isHideEye",true);

        // �� ��������Ʈ�� �ڵ� ���
        // ���� �� �ε� : ���� �ִϸ��̼� ������ �̵�
        yield return new WaitForSeconds(3);
        _bossAnim.SetBool("isHideEye", false);
        SceneManager.LoadScene("SnowBoss4");
    }

    IEnumerator InvertScene()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);
            _bossAnim.SetBool("isRaiseHand", true);
            CameraController cameraController = _camera.GetComponent<CameraController>();
            cameraController.isReverse = !cameraController.isReverse;
            _bossAnim.SetBool("isRaiseHand", true);
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
        StartCoroutine(PlayerInvincibility());
    }

    IEnumerator PlayerInvincibility()
    {
        _player.Invincibility = true;
        yield return new WaitForSeconds(1f);
        _player.Invincibility = false;
    }
}
