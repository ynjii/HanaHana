using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Around : MonoBehaviour
{
    [SerializeField] Vector3 _targetVector3 = Vector3.zero;
    [SerializeField] float _moveSpeed = 3.0f;
    [SerializeField] float _moveDirection = 1;

    void FixedUpdate()
    {
        transform.RotateAround(_targetVector3, Vector3.back, _moveSpeed * Time.fixedDeltaTime * _moveDirection);
    }
}
