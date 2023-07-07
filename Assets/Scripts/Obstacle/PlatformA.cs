using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 여기서 Obstacle 상태 정의.
/// </summary>
public class PlatformA : ObstacleController
{
    [SerializeField]
    private float dis = 5f;
    [SerializeField]
    private float spd = 5f; 
    [SerializeField]
    private new string tag = "Platform";

    public new void Awake()
    {
        base.Awake();
        currentDirection = Direction.Down;
        distance = dis;
        speed = spd;
        tagName = tag;
    }
}