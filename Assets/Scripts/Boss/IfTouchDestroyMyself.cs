using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfTouchDestroyMyself : MonoBehaviour
{
    [SerializeField]
    private string tagName = "Untagged";
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagName))
        {
            Destroy(this.gameObject);
        }
    }
}
