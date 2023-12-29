using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertPositioning : MonoBehaviour
{
    [SerializeField] private Transform _leftAlert;
    [SerializeField] private Transform _rightAlert;
    [SerializeField] private Camera _mainCamera;

    private void Update()
    {
        float xScreenHalfSize = _mainCamera.orthographicSize * _mainCamera.aspect;
        _leftAlert.position = new Vector3(-xScreenHalfSize - 0.5f, _leftAlert.position.y, _leftAlert.position.z);
        _rightAlert.position = new Vector3(xScreenHalfSize + 0.5f, _rightAlert.position.y, _rightAlert.position.z);
    }
}
