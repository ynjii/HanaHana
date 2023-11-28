using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CircleScaler : MonoBehaviour
{
    private Transform player;
    public Vector3 initScale = new Vector3(5, 5, 0);
    
    public Vector3 maxScale = new Vector3(16f, 8f, 0);
    public Vector3 minScale = new Vector3(3f, 3f, 0);
    public float maxDuration = 5f;
    public float minDuration = 2f;
    private Vector3 maxPosition = new Vector3(7,1.5f,0);
    private Vector3 minPosition = new Vector3(-7, -3, 0);
    private bool isPatternEnd = true;

    private Vector3 currentPos, currentScale, previousPos, previousScale = new Vector3(0,0,0);

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        transform.position = player.position;
        transform.localScale = initScale;
    }

    private void Update()
    {
        if (!isPatternEnd) return;

        isPatternEnd = false;
        StartCoroutine(WaitAndScale());
    }

    IEnumerator WaitAndScale()
    {
        float duration = Random.Range(minDuration, maxDuration);
        MoveAndScale(duration);
        yield return new WaitForSeconds(duration);
        previousScale = currentScale;
        previousPos = currentPos;
        isPatternEnd = true;
    }

    private void MoveAndScale(float duration)
    {
        currentPos.x = Random.Range(minPosition.x, maxPosition.x);
        
        if ((currentPos.x > 2f && currentPos.x < 7f) || (currentPos.x < -7f && currentPos.x > -2f))
        {
            currentPos.y = Random.Range(minPosition.y, maxPosition.y + 2);
        }
        else
        {
            currentPos.y = Random.Range(minPosition.y, maxPosition.y);
        }
        transform.DOMove(currentPos, duration);
        
        currentScale.x = Random.Range(minScale.x, maxScale.x);
        currentScale.y = Random.Range(minScale.y, maxScale.y);
        transform.DOScale(currentScale, duration);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            player.GetComponent<Player>().Die(transform.position);
        }
    }
}
