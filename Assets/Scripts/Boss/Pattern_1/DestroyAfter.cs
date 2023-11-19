using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몇 초 뒤에 destroy 해라.
/// </summary>
public class DestroyAfter : MonoBehaviour
{
    [SerializeField] private float waitingTime = 3f;
    void Start()
    {
        StartCoroutine(WaitDestroy());
    }

    IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(waitingTime);
        Destroy(this.gameObject);
    }
}
