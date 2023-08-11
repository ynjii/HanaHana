using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class MirrorReflect : MonoBehaviour
{
    private LineRenderer predictionLine;
    private RaycastHit2D predictionHit;
    private LayerMask predictionLayerMask;
    private GameObject player;
    private Player player_script;
    private SpriteRenderer player_spriterenderer;

    [SerializeField] private bool isReflectable;
    [SerializeField] private bool is_first_entered;
    [SerializeField] private Vector3 _direction;

    private void Start()
    {
        predictionLine = GetComponent<LineRenderer>();
        player = GameObject.FindWithTag("Player");
        player_script = player.GetComponent<Player>();
        player_spriterenderer = player.GetComponent<SpriteRenderer>();
        predictionLayerMask = ~(1 << 8);
    }

    private void LateUpdate()
    {
        //DrawPredictionLine(_direction);
    }

    private void Update()
    {
        // 색깔 별 기능
        if (predictionLine.materials[0].name == "RedLaserMat (Instance)" && player_script.player_state != PlayerState.Jump)
        {
            ShowLaser();
            DrawPredictionLine(_direction);
        }
        else if (predictionLine.materials[0].name == "RedLaserMat (Instance)" && player_script.player_state == PlayerState.Jump)
        {
            HideLaser();
        }
    }
      private void DrawPredictionLine(Vector3 _direction)
      {
            // Draw Prediction Line
            predictionLine.SetPosition(0, this.transform.position);
            Debug.DrawRay(transform.position, _direction * 10, Color.red);
            predictionHit = Physics2D.Raycast(transform.position, _direction, Mathf.Infinity, predictionLayerMask);

            if (predictionHit.collider.IsUnityNull())
            {
                predictionLine.SetPosition(1, new Vector3(_direction.x + transform.position.x, _direction.y + transform.position.y, 0));
                return;
            }
            
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
            
            if(!isReflectable) return;
            
            // draw first collision point
            predictionLine.SetPosition(1, new Vector3(predictionHit.point.x + transform.position.x, predictionHit.point.y + transform.position.y, 0));
            
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
