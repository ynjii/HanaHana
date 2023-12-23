/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomInitiate : MonoBehaviour
{
    [SerializeField] private List<GameObject> objPrefabs = new List<GameObject>();
    [SerializeField] private float delayTime = 6f;
    [SerializeField] private float objectLifeTime = 4f;

    private float timer = 2f;
    private Transform parentTransform;

    void Awake()
    {
        parentTransform = this.gameObject.transform;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= delayTime)
        {
            timer = 0f;
            GameObject newObject = InitiateRandomObject();
            Destroy(newObject, objectLifeTime); // 일정 시간이 지난 후에 해당 오브젝트를 제거
        }
    }

    private GameObject InitiateRandomObject()
    {
        if (objPrefabs.Count == 0)
        {
            Debug.LogWarning("objPrefabs 리스트에 프리팹이 없습니다.");
            return null;
        }

        int randomIndex = Random.Range(0, objPrefabs.Count);
        GameObject newObject = Instantiate(objPrefabs[randomIndex], transform.position, Quaternion.identity);
        newObject.transform.parent = parentTransform;
        return newObject;
    }
}*/