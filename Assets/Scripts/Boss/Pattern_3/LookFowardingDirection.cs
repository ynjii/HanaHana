using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookFowardingDirection : MonoBehaviour
{
    // 오른쪽으로 이동할 경우가 디폴트, 왼쪽으로 이동한다면 X flip
    // deltaPosition 0~양수 -> 오른쪽 이동 / 음수 -> 왼쪽 이동
    private float deltaPositionX;
    private float previousPosX;
    private SpriteRenderer _sprite;
    
    void Start()
    {
        previousPosX = transform.position.x;
        _sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        deltaPositionX = GetDeltaPos();
        if (deltaPositionX < 0)
        {
            _sprite.flipX = true;
        }
        else
        {
            _sprite.flipX = false;
        }
    }

    float GetDeltaPos()
    {
        float currentPosX = transform.position.x;
        deltaPositionX = currentPosX - previousPosX;
        previousPosX = currentPosX;
        return deltaPositionX;
    }
}
