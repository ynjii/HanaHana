using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrokenMirror : MonoBehaviour
{
    //거울 블록 두 가지 타입
    public enum BlockType
    {
        Once,//한 번
        Second //두 번
    }
    public SpriteRenderer before_img;
    public Sprite after_img;
    private int count=1;
    

    [SerializeField]
    private BlockType blType;

    private void OnCollisionEnter2D(Collision2D collision) {
        if(blType==BlockType.Once){
            this.gameObject.SetActive(false);
        }
        else if(count==1){//Second일 때, 아직 안 밟았을 때
            before_img.sprite=after_img;
            count--;
        }
        else{
            AudioSource audio=GameObject.FindWithTag("SoundController").GetComponent<AudioSource>();
            if (audio != null&&audio.clip.name== "glasscrack")
            {
                audio.Play();
            }
            this.gameObject.SetActive(false);
        }
    }
}
