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
    private AudioSource _audio;

    [SerializeField] GameObject _patternChangeGO;
    [SerializeField] private Animator _bossAnim;
    [SerializeField] private GameObject _clearUI;
    Player _player;
    bool isEnd = false;

    void Start()
    {
        _audio = gameObject.GetComponent<AudioSource>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        // StartCoroutine(InvertScene());
        StartCoroutine(PatternExecute());
    }

    private void Update()
    {
        if (_bossHP.currentHP <= 0 && !GameManager.instance.isGameover)
        {
            StartCoroutine(PatternChange());
        }
    }

    IEnumerator PatternChange()
    {
        foreach (var pattern in _patterns)
        {
            pattern.SetActive(false);
        }
        
        _player.Invincibility = true;
        _patternChangeGO.SetActive(true);

        _audio.Play();
        
        // ī�޶� shaking
        _camera.transform.DOShakePosition(3f, new Vector3(0.1f, 0.1f, 0));

        // ���� �ִϸ��̼� ����
        _bossAnim.SetBool("isHideEye", true);

        yield return new WaitForSeconds(4f);
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
            _bossAnim.SetBool("isRaiseHand", false);
        }
    }

    IEnumerator PatternExecute()
    {
        HashSet<int> executedPatterns = new HashSet<int>();

        for (int i = 0; i < _patterns.Count; i++)
        {
            int rand = Random.Range(0, _patterns.Count);
            if (executedPatterns.Contains(rand))
            {
                i--;
                continue;
            }
            executedPatterns.Add(rand);
            PatternActivate(rand);
            yield return new WaitForSeconds(14.5f);
        }
    }

    void PatternActivate(int patternNum)
    {
        for (int i = 0; i < _patterns.Count; i++)
        {
            if (i == patternNum)
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
