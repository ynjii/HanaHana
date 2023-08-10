using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static Define;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public CameraState cameraState;
    [SerializeField] private Transform player;
    private Player player_script;
    [SerializeField] private Vector3 delta; 
    void Awake()
    {
        Init();
    }

    void Init()
    {
        if (SceneManager.GetActiveScene().name == Define.Scene.SnowBoss123.ToString() || SceneManager.GetActiveScene().name == Define.Scene.SnowBoss4.ToString())
        {
            return;
        }
        cameraState = Define.CameraState.Player;
        player = GameObject.FindWithTag("Player").transform;
        player_script = player.GetComponent<Player>();
        delta = new Vector3(3f, 1f, -1f);
    }

    void LateUpdate()
    {
        if (player.IsUnityNull()) return;
        
        // 카메라의 position은 플레이어의 position에 델타값을 더한 값
        transform.position = player.transform.position + delta;
        
        // 플레이어가 피격당했다면 플레이어를 버림
        if (player_script.player_state == PlayerState.Damaged)
        {
            player = null;
        }
    }
}