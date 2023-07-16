using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraController : MonoBehaviour
{
    public Define.CameraState cameraState;
    [SerializeField] private Transform player;
    private Player player_script;
    [SerializeField] private Vector3 delta; 
    void Awake()
    {
        Init();
    }

    void Init()
    {
        cameraState = Define.CameraState.Player;
        player = GameObject.FindWithTag("Player").transform;
        player_script = player.GetComponent<Player>();
        delta = new Vector3(0f, 1f, -1f);
    }

    void LateUpdate()
    {
        // 플레이어가 피격당했다면 더 이상 따라가지 않음
        if (player_script.player_state == Define.PlayerState.Damaged)
        {
            player = null;
        }

        if (!player.IsUnityNull())
        {
            // 카메라의 position은 플레이어의 position에 델타값을 더한 값
            transform.position = player.transform.position + delta;
            
        }
    }
}
