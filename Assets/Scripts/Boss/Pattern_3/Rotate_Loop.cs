using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Loop : MonoBehaviour
{
    [SerializeField] float _rotateDuration = 10.0f;
    [SerializeField] int _direction = 1;
   
    void Start()
    {
        transform.DORotate(new Vector3(0, 0, _direction * 360), _rotateDuration, RotateMode.FastBeyond360)
                         .SetEase(Ease.Linear)
                         .SetLoops(-1);
    }
}
