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
    //애플 패턴(4_2)
    private const int MAX_APPLE_NUM= 12;
    private int a_pattern_count=0;
    private int a_pattern_direction = 1; // 1 or -1
    private List<List<int>> initial_index= new List<List<int>>
    {
        new List<int> { 4,5,10,11 },
        new List<int> { 3,4,5,9,10,11},
        new List<int> { 2,3,4,5,8,9,10,11},
        new List<int> { 1,2,3,4,5,7,8,9,10,11},
        new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }
    };

    private Player player_script;
    public GameObject fire;
    public Transform pos;
    public int fires_index;
    [SerializeField]
    private GameObject[] fires = new GameObject[0];
    private List<Transform> start_points=new List<Transform>();

    private void Awake()
    {
        player_script = GameObject.FindWithTag("Player").GetComponent<Player>();
        //애플패턴(4_2)
        for(int i=0;i<MAX_APPLE_NUM;i++)
        {
            Transform trans = new GameObject("start_point").transform;
            if (i < MAX_APPLE_NUM/2)//위 
            {
                trans.position = new Vector3(22f, 2f + 0.5f * i, 0);
            }
            else//아래
            {
                trans.position = new Vector3(22f, -2f - (0.5f * (i%(MAX_APPLE_NUM/2))), 0);
            }
            start_points.Add(trans);
        }
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
                    //더블패턴일때 
                    if (this.gameObject.name == "bullet2_launcher" )
                    {
                        //그게 분열되는 애일 때(4_1패턴)
                        if (fires[fires_index].name == "Bullet13")
                        {
                            cool_time = 1f;
                            GameObject clone_obj = Instantiate(fires[fires_index], pos.position, transform.rotation) as GameObject;
                            StartCoroutine(SplitCoroutine(clone_obj)); // 클론된 객체에 대해 코루틴 시작                    
                        }
                        //애플 패턴일 때(4_2패턴)
                        if (fires[fires_index].name == "Apple")
                        {
                            
                            cool_time = 0.1f;
                            
                            int i = 0;
                            int j = 0;
                            foreach (Transform trans in start_points)
                            {
                                //맞는 인덱스일때만 발사
                                if (i == initial_index[a_pattern_count][j])
                                {
                                    //패턴탄알발사
                                    Instantiate(fires[fires_index], trans.position, Quaternion.identity);
                                    j++;
                                }
                                i++;
                            }
                            if (a_pattern_direction == 1)
                            {
                                a_pattern_count++;
                            }
                            else
                            {
                                a_pattern_count--;
                            }
                            if (a_pattern_count == 4 || a_pattern_count == 0) 
                            {
                                a_pattern_direction *= -1; // 방향을 반대로 변경
                            }
                        }
                    }
                    else
                    {
                        //패턴탄알발사
                        cool_time = 1f;
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
