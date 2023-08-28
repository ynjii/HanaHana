using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfTouchDestroyMyself : MonoBehaviour
{
    [SerializeField]
    private string tagName1 = "Untagged";
    [SerializeField]
    private string tagName2="Untagged";
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagName1) || collision.gameObject.CompareTag(tagName2))
        {
            Destroy(this.gameObject);
        }
    }

}
