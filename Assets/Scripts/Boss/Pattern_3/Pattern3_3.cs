using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern3_3 : MonoBehaviour
{
    [SerializeField] float _moveInterval = 4f;
    [SerializeField] float _rotateInterval = 3f;
    [SerializeField] float _rotateOnceValue = 180;
    [SerializeField] float _rotateDirection = 1;
    [SerializeField] string _color;
    [SerializeField] float _moveDelta;

    void Start()
    {
        MoveMirror();
    }

    void MoveMirror()
    {
        switch (_color)
        {
            case "Red":
                StartCoroutine(moveRed());
                break;
            case "Blue":
                moveBlue();
                break;
        }
    }


    IEnumerator moveRed()
    {
        while (true)
        {
            transform.DORotate(new Vector3(0, 0, _rotateOnceValue), _rotateInterval);
            yield return new WaitForSeconds(_rotateInterval);
            transform.DOLocalMoveX(_moveDelta, _moveInterval);
            yield return new WaitForSeconds(_moveInterval);

            transform.DORotate(new Vector3(0, 0, 0), _rotateInterval);
            yield return new WaitForSeconds(_rotateInterval);
            transform.DOLocalMoveX(-_moveDelta, _moveInterval);
            yield return new WaitForSeconds(_moveInterval);
        }
    }

    void moveBlue()
    {
        transform.DORotate(new Vector3(0, 0, 360 * _rotateDirection), 10, RotateMode.FastBeyond360)
                     .SetEase(Ease.Linear)
                     .SetLoops(-1);
    }
}
