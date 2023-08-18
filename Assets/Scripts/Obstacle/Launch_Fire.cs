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
            if (fires.Length >= 1)
            {
                //안죽었을때
                if (player_script.player_state != Define.PlayerState.Damaged)
                {
                    //패턴탄알발사
                    Instantiate(fires[fires_index], pos.position, transform.rotation);
                }
            }
            else
            {
                Instantiate(fire, pos.position, transform.rotation);
            }
            time = 0f;
        }
       
    }
}
