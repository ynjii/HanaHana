using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfExitDie : MonoBehaviour
{
    public Player player;
    private bool playerInside = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("들어옴");
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    private void Update()
    {
        if (!playerInside)
        {
            player.GetComponent<Player>().Die(transform.position);
            return;
        }
    }
}
