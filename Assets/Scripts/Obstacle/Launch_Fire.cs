using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch_Fire : MonoBehaviour
{
    [SerializeField]
    private float cool_time=1f;

    private float time=0;

    public GameObject fire;
    public Transform pos;
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= cool_time)
        {
            Instantiate(fire,pos.position,transform.rotation);
            time = 0f;
        }
    }
}
