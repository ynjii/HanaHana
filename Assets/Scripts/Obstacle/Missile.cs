using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 5f;
    public Transform player;

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;
    }
}
