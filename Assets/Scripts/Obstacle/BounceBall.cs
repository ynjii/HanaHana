using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BounceBall : MonoBehaviour
{
  const float C_Radian = 180f;
  public Rigidbody2D rb;
  public float speed;
  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }
  // Update is called once per frame
  void FixedUpdate()
  {
    Vector3 pos = rb.position;
    Vector3 movePos = pos + transform.up * speed * Time.deltaTime;
    rb.MovePosition(movePos);
  }
  private void OnCollisionEnter2D(Collision2D collision)
  {
    if(collision.collider.CompareTag("Border"))
    {
      Vector3 tmp = transform.eulerAngles;
      tmp.z = C_Radian - tmp.z;
      transform.eulerAngles = tmp;
    }
    else if(collision.collider.CompareTag("Platform"))
    {
      Vector3 tmp = transform.eulerAngles;
      tmp.z = (C_Radian * 2) - tmp.z;
      transform.eulerAngles = tmp;
    }
  }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall : MonoBehaviour
{
    //볼의 가속 속도
    public float BallInitialVelocity = 300f;

    //리지드 바디
    private Rigidbody2D ballRigidBody = null;

    //볼플레이 선택여부
    private bool isBallInPlay = false;

    private void Awake()
    {
        ballRigidBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
        if(isBallInPlay)
        {   transform.parent = null;
            ballRigidBody.isKinematic = false;
            ballRigidBody.AddForce(new Vector3(BallInitialVelocity, BallInitialVelocity, 0f));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isBallInPlay=true;
    }


}*/