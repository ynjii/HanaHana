using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// pattern1 controller
/// 초단위로 계산해서 아래 phase object 활성/비활성화함
/// 1. phase object를 함수로 만들어서 하나의 스크립트/여러개 스크립트에 넣기
/// 2. 이것도 다시 개선
/// </summary>
public class Pattern1Controller : MonoBehaviour
{
    //public GameObject phases=new List<GameObject>();
    public GameObject pattern1;
    public GameObject pattern2;
    public GameObject pattern3;
    public GameObject pattern4;
    public GameObject pattern5;
    public Rigidbody2D other;
    public GameObject SaveLoad;

    void Start()
    {
        //change respawn point to lava start point
        Vector3 start_point = new Vector3(10.18f, -1.31f, 0.0f);
        SaveLoad.GetComponent<SaveLoad>().SaveRespawn("respawn", start_point);

        StartCoroutine(Pattern());
    }

    IEnumerator Pattern()
    {
        pattern1.SetActive(true);
        yield return new WaitForSeconds(12f);
        pattern1.SetActive(false);
        DeleteCloneObjects();
        yield return new WaitForSeconds(2f);

        pattern4.SetActive(true);
        yield return new WaitForSeconds(12f);
        other.constraints = RigidbodyConstraints2D.FreezeRotation;
        pattern4.SetActive(false);
        DeleteCloneObjects();
        yield return new WaitForSeconds(2f);

        pattern2.SetActive(true);
        yield return new WaitForSeconds(12f);
        other.constraints = RigidbodyConstraints2D.FreezeRotation;
        pattern2.SetActive(false);
        DeleteCloneObjects();
        yield return new WaitForSeconds(2f);

        pattern3.SetActive(true);
        yield return new WaitForSeconds(12f);
        other.constraints = RigidbodyConstraints2D.FreezeRotation;
        pattern3.SetActive(false);
        DeleteCloneObjects();
        yield return new WaitForSeconds(2f);

        pattern5.SetActive(true);
        yield return new WaitForSeconds(12f);
        DeleteCloneObjects();
        pattern5.SetActive(false);

        //다음 씬()으로 로드
        SceneManager.LoadScene("SnowBoss2");
    }

    //짠탄제거
    public void DeleteCloneObjects()
    {
        // 씬 내의 모든 게임 오브젝트 가져오기
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            //클론된 애면
            if (IsClone(obj))
            {
                Destroy(obj); // 클론 오브젝트 삭제
            }
        }
    }

    //클론된애인지 판별
    private bool IsClone(GameObject obj)
    {
        // 이름에 "(Clone)" 문자열이 포함되어 있는지 검사
        return obj.name.Contains("(Clone)");
    }
}
