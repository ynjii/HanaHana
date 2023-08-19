using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossWall : MonoBehaviour
{
    public GameObject other;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("뭐임?");
            Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
            otherRigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("졌다?");
            Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
            otherRigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
    }
}
