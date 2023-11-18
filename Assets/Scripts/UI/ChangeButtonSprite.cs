/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonSprite : MonoBehaviour
{
    Player player_script;
    public Sprite[] images;
    Sprite my_sprite;
    // Start is called before the first frame update
    void Start()
    {
        player_script=GameObject.FindWithTag("Player").GetComponent<Player>();
        my_sprite = GetComponent<Image>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name == "ButtonImage_left")
        {
            if (player_script.IsLeftButton)
            {
                GetComponent<Image>().sprite = images[0];
            }
            else
            {
                GetComponent<Image>().sprite = my_sprite;
            }
        }
        if(this.gameObject.name == "ButtonImage_right")
        {
            if (player_script.IsRightButton)
            {
                GetComponent<Image>().sprite = images[1];
            }
            else
            {
                GetComponent<Image>().sprite = my_sprite;
            }
        }
        if (this.gameObject.name == "ButtonImage_jump")
        {
            if (player_script.IsJumpButton)
            {
                GetComponent<Image>().sprite = images[2];
            }
            else
            {
                GetComponent<Image>().sprite = my_sprite;
            }
        }
    }
}
*/