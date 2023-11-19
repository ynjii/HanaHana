using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*미러맵에서 깨지는 유리 타일. 2번, 1번 중 설정 가능, 알맞은 타이밍에 부서지는지 확인*/
public class WeakPlatform : MonoBehaviour
{
    //거울 블록 두 가지 타입
    public enum BlockType
    {
        Once,//한 번
        Second //두 번
    }
    public SpriteRenderer before_img;
    public Sprite after_img;
    

    [SerializeField]
    private BlockType blType;

    private void OnCollisionEnter2D(Collision2D collision) {
        if(blType==BlockType.Once){//처음 밟아도 죽을 때
            AudioSource audio = GameObject.FindWithTag("SoundController").GetComponent<AudioSource>();
            if (audio != null && audio.clip.name == "glasscrack")
            {
                audio.Play();
            }
            Invoke("setActiveFalse", 0.5f);
        }
        else if(blType==BlockType.Second){//Second일 때, 아직 안 밟았을 때
            before_img.sprite=after_img;
            blType--;
        }
    }

    private void setActiveFalse()
    {
        this.gameObject.SetActive(false);   
    }
}
