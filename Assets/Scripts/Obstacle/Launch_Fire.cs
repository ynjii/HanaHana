using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Launch_Fire : MonoBehaviour
{
    [SerializeField]
    private float cool_time=1f;

    private float time=0;

    private Player player_script;
    public GameObject fire;
    public Transform pos;
    public int fires_index;
    [SerializeField]
    private GameObject[] fires = new GameObject[0];
    private void Awake()
    {
        player_script = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= cool_time)
        {
            if (fires.Length >= 1)//배열에 뭔가 넣었을 때
            {
                //안죽었을때
                if (player_script.player_state != Define.PlayerState.Damaged)
                {
                    //Launch block 1개 더 있을 때 && 그게 분열되는 애일 때  
                    if (this.gameObject.name == "bullet2_launcher" && fires[fires_index].name=="Bullet13")
                    {
                        GameObject clone_obj = Instantiate(fires[fires_index], pos.position, transform.rotation) as GameObject;
                        StartCoroutine(SplitCoroutine(clone_obj)); // 클론된 객체에 대해 코루틴 시작                    
                    }
                    else
                    {
                        //패턴탄알발사
                        Instantiate(fires[fires_index], pos.position, transform.rotation);
                    }
                }
            }
            else
            {
                Instantiate(fire, pos.position, transform.rotation);
            }
            time = 0f;
        }

    }

    IEnumerator SplitCoroutine(GameObject clone_obj)
    {
        while (true)
        {
            // n 초 대기
            yield return new WaitForSeconds(0.2f);

            // 대기 후 split
            GameObject cclon_obj1=Instantiate(clone_obj, clone_obj.transform.position + new Vector3(0, 1.5f, 0), transform.rotation);
            GameObject cclon_obj2 = Instantiate(clone_obj, clone_obj.transform.position + new Vector3(0, -1.5f, 0), transform.rotation);

            float move_time=0;            
            while (move_time<=0.2)
            {
                move_time += Time.deltaTime;
                // 체크 후에 접근
                if (cclon_obj1 != null && cclon_obj2 != null)
                {
                    cclon_obj1.transform.position += new Vector3(0, 0.2f, 0);
                    cclon_obj2.transform.position += new Vector3(0, -0.2f, 0);
                }
                yield return new WaitForSeconds(0.1f);
            }

            //화면밖으로 나갈때까지 대기
            yield return new WaitForSeconds(5f);

        }
    }

}
