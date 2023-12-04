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
    //public GameObject endPanel;
    public Rigidbody2D other;

    void Start()
    {
        StartCoroutine(Pattern());
    }

    IEnumerator Pattern()
    {
        pattern1.SetActive(true);
        yield return new WaitForSeconds(12f);
        pattern1.SetActive(false);
        yield return new WaitForSeconds(2f);
        pattern4.SetActive(true);
        yield return new WaitForSeconds(12f);
        pattern4.SetActive(false);
        yield return new WaitForSeconds(2f);
        pattern2.SetActive(true);
        yield return new WaitForSeconds(12f);
        pattern2.SetActive(false);
        other.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(2f);
        pattern3.SetActive(true);
        yield return new WaitForSeconds(12f);
        pattern3.SetActive(false);
        other.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(2f);
        pattern5.SetActive(true);
        yield return new WaitForSeconds(12f);
        pattern5.SetActive(false);
        other.constraints = RigidbodyConstraints2D.FreezeRotation;

        //endPanel.SetActive(true);
        //Time.timeScale = 0f; //시간 정지
        SceneManager.LoadScene("SnowBoss2");
        //다음 씬()으로 로드
    }
}
