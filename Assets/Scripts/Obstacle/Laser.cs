using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        ShootLaser();
    }
    private void FixedUpdate()
    {
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
