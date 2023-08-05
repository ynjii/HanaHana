using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Laser : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 100;
    public Transform laserFirePoint;
    public LineRenderer m_lineRenderer;
    Transform m_transform;
    private  GameObject player;
    private Player player_script;
    private SpriteRenderer player_spriterenderer;
    public bool is_first_entered;
    // Start is called before the first frame update
    void Awake()
    {
        m_transform = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player") ;
        player_script = player.GetComponent<Player>();
        player_spriterenderer= player.GetComponent<SpriteRenderer>();
        is_first_entered = true;
    }

    // Update is called once per frame
    void Update()
    {
        //빨간 레이저면 : 애가 땅에 붙어있을 때만 켜짐 
        if (m_lineRenderer.materials[0].name == "RedLaserMat (Instance)" && player_script.player_state != PlayerState.Jump)
        {
            ShowLaser();
            ShootLaser();
        }
        else if(m_lineRenderer.materials[0].name == "RedLaserMat (Instance)" && player_script.player_state == PlayerState.Jump)
        {
            HideLaser();
        }
        
}

    // 레이저를 숨기는 함수
    void HideLaser()
    {
        m_lineRenderer.enabled = false;
    }

    // 레이저를 다시 보이게 하는 함수
    void ShowLaser()
    {
        m_lineRenderer.enabled = true;
    }

    void ShootLaser()
    {
        if (Physics2D.Raycast(m_transform.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(laserFirePoint.position, transform.right);            
            Draw2DRay(laserFirePoint.position, _hit.point);
            if (_hit.collider.CompareTag("Player"))
            {
                Vector2 target_pos = new Vector2(0, 0);
                if (player_spriterenderer.flipX)
                {
                    target_pos = new Vector2(player.transform.position.x - 1, 0);
                }
                else
                {
                    target_pos = new Vector2(player.transform.position.x + 1, 0);
                }
                if (is_first_entered)
                {
                    is_first_entered = false;
                    player_script.onDamaged(target_pos);
                    GameManager.instance.OnPlayerDead();
                }
            }
        }
        else
        {
            Draw2DRay(laserFirePoint.position, laserFirePoint.transform.right * defDistanceRay);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        m_lineRenderer.SetPosition(0,startPos);
        m_lineRenderer.SetPosition(1, endPos);
    }
}
