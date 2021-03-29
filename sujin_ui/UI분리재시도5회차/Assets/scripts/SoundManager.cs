using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicsource;
    public AudioSource btnsource;


    public void SetMusicVolume(float volume)
    {
        musicsource.volume = volume; // 배경음 오디오 소스의 볼륨을 조절해주는 역할
    }

    public void SetButtonVolume(float volume)
    {
        btnsource.volume = volume;  // 효과음 오디오 소스 볼륨 조절 
    }

    public void EffectSound()
    {
        btnsource.Play();
    }
}
