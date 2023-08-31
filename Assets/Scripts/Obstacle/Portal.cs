using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Portal : MonoBehaviour
{
    //[SerializeField] private Image fadeOutPanel;
    //[SerializeField] private string _nextScene;

    public GameObject clearUI;
    public AudioSource clap;
    private bool fade_out = false;
    private float timer = 0;
    private bool clear = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        /*        fade_out = true;
                Transform parent_trans=fadeOutPanel.gameObject.transform.parent;
                parent_trans.gameObject.SetActive(true);
        */
        clearUI.SetActive(true);
        clap.Play();
        clear = true;
        
    }

    private void Update()
    {
        /*if (fade_out)
        {
            timer += Time.deltaTime;
            fadeOutPanel.color+= new Color(0,0,0,0.1f);
        }
        if (timer >= 1f)
        {
            SceneManager.LoadScene(_nextScene);
        }*/
        if (clear)
        {
            timer += Time.deltaTime;
            
        }
        if (timer >= 3)
        {
            touchTorestart();
        }
        
    }

    private void touchTorestart()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
