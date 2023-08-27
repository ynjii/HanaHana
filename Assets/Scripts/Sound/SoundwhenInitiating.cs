using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundwhenInitiating : MonoBehaviour
{

    public AudioSource soundSource; // AudioSource 컴포넌트를 인스펙터에서 할당

    private void Start()
    {
        PlayInitiationSound();
    }

    public void PlayInitiationSound()
    {
        if (soundSource != null)
        {
            soundSource.Play(); // AudioSource를 재생
        }
    }
}



