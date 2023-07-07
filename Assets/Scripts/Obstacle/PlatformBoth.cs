using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 여기서 Obstacle 상태 정의.
/// </summary>
public class PlatformBoth : ObstacleController
{
    [SerializeField]
    private float dis = 10f;
    [SerializeField]
    private float spd = 5f; 

    public new void Awake()
    {
        base.Awake();
        currentDirection = Direction.Down;
        distance = dis;
        speed = spd;

    }
}