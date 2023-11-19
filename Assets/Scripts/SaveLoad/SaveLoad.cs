using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// playerprefs를 이용한 save, load data. gamemanager의 static 변수-saveload의 playerpref 접근 함수로 연결된다. gm쪽에서 연결
/// </summary> 
public class SaveLoad : MonoBehaviour
{
    //데스카운트 저장, 튜토리얼 진행 함수도 이거 쓴다. 함수명 바꿔야하나?
    public void SaveDeathCount(string key, int count)
    {
        PlayerPrefs.SetInt(key, count);
    }

    //데스카운트 로드, playerprefs
    public int LoadDeathCount(string key)
    {
        int count = PlayerPrefs.GetInt(key, 0);
        return count;
    }

    //scene 이어하기, 새로하기 저장
    public void SaveBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    //scene 이어하기, 새로하기 로드
    public bool LoadBool(string key)
    {
        int intValue = PlayerPrefs.GetInt(key, 0);
        bool loadedValue = intValue != 0;
        return loadedValue;
    }

    //리스폰 위치 저장
    public void SaveRespawn(string key, Vector3 value)
    {
        PlayerPrefs.SetFloat(key + "X", value.x);
        PlayerPrefs.SetFloat(key + "Y", value.y);
        PlayerPrefs.SetFloat(key + "Z", value.z);

    }

    //리스폰 위치 로드
    public Vector3 LoadRespawn(string key)
    {
        Vector3 v3 = Vector3.zero;
        v3.x = PlayerPrefs.GetFloat(key + "X");
        v3.y = PlayerPrefs.GetFloat(key + "Y");
        v3.z = PlayerPrefs.GetFloat(key + "Z");
        return v3;
    }
}