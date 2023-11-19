using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
///이어하기, 새로하기, 등 ui button과 scenechange를 적절히 연결
///gamemanager가 timescale 0, scenechange가 scene load 하고 timescale 1로 세팅 
/// </summary>
public class SceneChange : MonoBehaviour
{
    
    public GameObject SaveLoad;
    private bool isNew=false;
    //이어하기
    public void Change()
    {
            SceneManager.LoadScene("SnowWhite");
            Time.timeScale = 1f; //시간 다시 흐르게
    }

    //새로하기
    public void new_Change()
    {
        /*if(EditorUtility.DisplayDialog("게임 세이브 정보 삭제", "정말 삭제 하시겠습니까?", "네", "아니오"))*/
        PlayerPrefs.DeleteAll();
        isNew=true;
        SaveLoad.GetComponent<SaveLoad>().SaveBool("New", isNew);
        SceneManager.LoadScene("SnowWhite");
        Time.timeScale = 1f; //시간 다시 흐르게
    }

    public void back_Home()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Closet_Change()
    {
        SceneManager.LoadScene("ClosetScene");
    }

    public void Stage_Change()
    {
        isNew=SaveLoad.GetComponent<SaveLoad>().LoadBool("New");
        if(isNew){
        SceneManager.LoadScene("StageScene");
        }
    }
}