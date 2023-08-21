using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Red_Blue_Laser : MonoBehaviour
{
    [SerializeField] GameObject _redMirror;
    [SerializeField] GameObject _blueMirror;
    Player _playerScript;

    private void Start()
    {
        _playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (_playerScript.player_state != PlayerState.Jump)
        {
            _redMirror.SetActive(true);
            _blueMirror.SetActive(false);
        }
        else
        {
            _redMirror.SetActive(false);
            _blueMirror.SetActive(true);
        }
    }
}