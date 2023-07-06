using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 조금만 왼쪽으로 움직인다는 소리
/// </summary>
public class ObstacleLittleLeft : ObstacleController
{
    [SerializeField]
    private float dis = 5f;
    [SerializeField]
    private float spd = 5f;

    public new void Awake()
    {
        base.Awake();
        currentDirection = Direction.Left;
        distance = dis;
        speed = spd;
    }
}
