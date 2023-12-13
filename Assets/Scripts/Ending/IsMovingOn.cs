using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsMovingOn : MonoBehaviour
{
    ObstacleController obstacleController;
    private void OnBecameVisible()
    {
        obstacleController.IsMoving = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        obstacleController = GetComponent<ObstacleController>();
    }

}
