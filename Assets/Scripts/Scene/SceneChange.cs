using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChange : MonoBehaviour
{
    public void Change()
    {
        SceneManager.LoadScene("SnowWhite");
    }

    public void new_Change()
    {
        /*if(EditorUtility.DisplayDialog("게임 세이브 정보 삭제", "정말 삭제 하시겠습니까?", "네", "아니오"))*/
        PlayerPrefs.DeleteAll();
        
        SceneManager.LoadScene("SnowWhite");
    }

    public void back_Home()
    {
        SceneManager.LoadScene("MainScene");
    }
}