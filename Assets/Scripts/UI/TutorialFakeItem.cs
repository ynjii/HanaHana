using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFakeItem : MonoBehaviour
{
    public GameObject SaveLoad;
    
    private void OnCollisionEnter2D(Collision2D collision){
         if(SaveLoad.GetComponent<SaveLoad>().LoadDeathCount("tutorial")==1){
            SaveLoad.GetComponent<SaveLoad>().SaveDeathCount("tutorial", 2);
         }
        PlayerPrefs.SetString("RealItem", "TutorialFakeItem");
    }
}