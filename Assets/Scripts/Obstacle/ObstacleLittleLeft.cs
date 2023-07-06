using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
