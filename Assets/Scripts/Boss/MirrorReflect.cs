using UnityEngine;

public class MirrorReflect : MonoBehaviour
{
    private LineRenderer predictionLine;
    private RaycastHit2D predictionHit;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private LayerMask predictionLayerMask;

    private void Start()
    {
        predictionLine = GetComponent<LineRenderer>();
        _direction = _direction.normalized;
    }

    private void LateUpdate()
    {
        DrawPredictionLine(_direction);
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
    
            predictionLine.SetPosition(2, predictionHit.point);
    
            // finally render linerenderer
            predictionLine.enabled = true;
        }
}
