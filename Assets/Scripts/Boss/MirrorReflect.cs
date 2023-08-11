using UnityEngine;
using static Define;

public class MirrorReflect : MonoBehaviour
{
    private LineRenderer predictionLine;
    private RaycastHit2D predictionHit;
    private GameObject player;
    private Player player_script;
    private SpriteRenderer player_spriterenderer;
    private bool is_first_entered;

    [SerializeField] private Vector3 _direction;
    [SerializeField] private LayerMask predictionLayerMask;

    private void Start()
    {
        predictionLine = GetComponent<LineRenderer>();
        _direction = _direction.normalized;
        player = GameObject.FindWithTag("Player");
        player_script = player.GetComponent<Player>();
        player_spriterenderer = player.GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        DrawPredictionLine(_direction);
    }

    private void Update()
    {
        // 색깔 별 기능
        /*if (m_lineRenderer.materials[0].name == "RedLaserMat (Instance)" && player_script.player_state != PlayerState.Jump)
        {
            ShowLaser();
            ShootLaser();
        }
        else if (m_lineRenderer.materials[0].name == "RedLaserMat (Instance)" && player_script.player_state == PlayerState.Jump)
        {
            HideLaser();
        }*/
    }

    private void DrawPredictionLine(Vector3 direction)
        {
            // Draw Prediction Line
            predictionLine.SetPosition(0, this.transform.position);
            Debug.DrawRay(transform.position, direction * 10, Color.red);
            predictionHit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, predictionLayerMask);
    
            if(predictionHit.collider == null)
            {
                // no collision => don't draw prediction line
                predictionLine.enabled = false;
                return;
            }

            // draw first collision point
            predictionLine.SetPosition(1, predictionHit.point);
    
            // calculate second ray by Vector2.Reflect
            var inDirection = (predictionHit.point - (Vector2)transform.position).normalized;
            var reflectionDir = Vector2.Reflect(inDirection, predictionHit.normal);
    
            // By multiply 0.001, can have detail calculation
            predictionHit = Physics2D.Raycast(predictionHit.point + (reflectionDir * 0.001f), reflectionDir, Mathf.Infinity, predictionLayerMask);
            // 플레이어면 죽음
            if (predictionHit.collider.CompareTag("Player"))
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

        predictionLine.SetPosition(2, predictionHit.point);
    
            // finally render linerenderer
            predictionLine.enabled = true;
        }

    void HideLaser()
    {
        predictionLine.enabled = false;
    }

    
    void ShowLaser()
    {
        predictionLine.enabled = true;
    }
}
