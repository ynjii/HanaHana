using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public bool enableSpawn = false;
    public GameObject Enemy; //Prefab을 받을 public 변수 입니다.
    void EnemySpawn()
    {
        float randomX = Random.Range(9.5f, 23f); //적이 나타날 X좌표를 랜덤으로 생성해 줍니다.
        float randomY= Random.Range(5f, 3f);
        if (enableSpawn)
        {
            GameObject enemy =(GameObject)Instantiate(Enemy, new Vector3(randomX, randomY, 0f), Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
        }
    }
    void Start () {
        InvokeRepeating("EnemySpawn", 4, 1.5f); //3초후 부터, SpawnEnemy함수를 1초마다 반복해서 실행 시킵니다.
    }
}