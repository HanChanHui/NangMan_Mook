using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("점프 사운드 넣기")][SerializeField] private AudioClip audioJump;
    [Header("데미지 입을 때 사운드 넣기")][SerializeField] private AudioClip audioDamaged;
    [Header("게임 오버 사운드 넣기")][SerializeField] private AudioClip audioDie;
    [Header("스테이지 클리어 사운드 넣기")][SerializeField] private AudioClip audioFinish;

    AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //============ 소리 ============//
    public void PlaySound(string action)
    {
        switch (action)
        {
            case "JUMP":
                audioSource.clip = audioJump;
                audioSource.volume = 0.1f;
                audioSource.PlayOneShot(audioJump);
                break;
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                audioSource.PlayOneShot(audioDamaged);
                break;
            case "GAMEOVER":
                audioSource.clip = audioDie;
                audioSource.PlayOneShot(audioDie);
                break;
            case "FINISH":
                audioSource.clip = audioFinish;
                audioSource.PlayOneShot(audioFinish);
                break;
        }
    }
    //=============================//
}