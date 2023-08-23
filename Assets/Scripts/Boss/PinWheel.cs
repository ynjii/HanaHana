using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinWheel : MonoBehaviour
{
    CirclePattern pattern;
    [SerializeField] public float rotateSpeed;
    [SerializeField] float launch_time;
    [SerializeField]
    private float launchForce;
    [SerializeField]
    private float spreadFactor;
    float launch_timer=0;
    float destroy_time=0;
    // Start is called before the first frame update
    void Start()
    {
        pattern=new CirclePattern();
    }

    // Update is called once per frame
    void Update()
    {
        launch_timer += Time.deltaTime;
        destroy_time+= Time.deltaTime;
        Rotate();
        if (launch_timer>=launch_time)
        {
            pattern.LaunchToOutside(this.gameObject.transform, launchForce, spreadFactor);
            launch_timer = 0;
        }
        if (destroy_time >= 15)
        {
            Destroy(gameObject);
        }

    }
    private void Rotate()
    {
        transform.Rotate(0, 0, -Time.deltaTime * rotateSpeed, Space.Self);
    }
}
