using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{

    /// <summary>
    ///리스폰 위치를 변경 및 저장
    /// </summary>
    /// <param name="position">Flag(체크포인트)의 위치</param>
    public void SetRespawnPoint(Vector3 position)
    {
        GameManager.respawnPoint=position;
    }

    /// <summary>
    /// 플레이어를 리스폰할 위치로 이동
    /// </summary>
    public void Respawn()
    {
        transform.position = GameManager.respawnPoint.position;
    }
}
