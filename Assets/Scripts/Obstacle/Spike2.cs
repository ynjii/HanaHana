using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//튜토리얼가시2
public class Spike2 : ObstacleController
{
   [SerializeField]
    private float dis = 1f;
    [SerializeField]
    private float spd = 5f;

    public new void Awake()
    {
        base.Awake();
        currentDirection = Direction.Up;
        distance = dis;
        speed = spd;
    }
}
