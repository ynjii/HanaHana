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

    public bool isReverse = false;
    private bool preIsReverse; //이전 프레이므이 isReverse 저장

    private float zValue = -1f;


    void Awake()
    {
        Init();
    }

    void Init()
    {
        if (SceneManager.GetActiveScene().name == Define.Scene.SnowBoss4.ToString() || SceneManager.GetActiveScene().name == Define.Scene.SnowBoss123.ToString() || SceneManager.GetActiveScene().name == "SnowBoss3" )
        {
            return;
        }
        cameraState = Define.CameraState.Player;
        player = GameObject.FindWithTag("Player").transform;
        player_script = player.GetComponent<Player>();
        preIsReverse = isReverse;
        UpdateDelta();
    }

    private void UpdateDelta()
    {
        zValue = isReverse ? 1f : -1f;
        delta = new Vector3(3f, 1f, zValue);

        Quaternion newRotation = Quaternion.Euler(0f, isReverse ? 180f : 0f, 0f);
        transform.rotation = newRotation;
    }

    void LateUpdate()
    {
        if (player.IsUnityNull()) return;

        if (preIsReverse != isReverse) //isReverse에 변동이 있을때만
        {
            preIsReverse = isReverse;
            UpdateDelta(); // delta 값 업데이트

        }

        if (SceneManager.GetActiveScene().name == Define.Scene.SnowBoss123.ToString() || SceneManager.GetActiveScene().name == "SnowBoss3"|| SceneManager.GetActiveScene().name == Define.Scene.SnowBoss4.ToString())
        {
            return;
        }

        // 카메라의 position은 플레이어의 position에 델타값을 더한 값
        transform.position = player.transform.position + delta;

        // 플레이어가 피격당했다면 플레이어를 버림
        if (player_script.player_state == PlayerState.Damaged)
        {
            player = null;
        }

    }
}