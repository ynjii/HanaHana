using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossWall : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        collision.gameObject.CompareTag("Player")
    }
}
