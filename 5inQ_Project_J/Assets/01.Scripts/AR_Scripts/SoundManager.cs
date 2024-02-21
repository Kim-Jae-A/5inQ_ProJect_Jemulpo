using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 소리를 재생할 AudioSource 변수
    private AudioSource soundSource; 
    
    AudioClip soundClip;

    private void Awake()
    {
        soundSource = GetComponent<AudioSource>();
    }

    public void TrainMoveSfx()
    {
        // AudioClip을 에셋에서 불러와서 할당
        soundClip = Resources.Load<AudioClip>("AR_Sounds/TrainMove"); // 파일이름은 에셋 폴더 내의 경로를 기준으로 함

        // AudioClip을 AudioSource에 할당
        soundSource.clip = soundClip;

        soundSource.Play();
    }

    public void TrainIdleSfx()
    {
        // AudioClip을 에셋에서 불러와서 할당
        soundClip = Resources.Load<AudioClip>("AR_Sounds/TrainIdle"); // 파일이름은 에셋 폴더 내의 경로를 기준으로 함

        // AudioClip을 AudioSource에 할당
        soundSource.clip = soundClip;

        soundSource.Play();
    }

    public void CatMeowSfx()
    {
        // AudioClip을 에셋에서 불러와서 할당
        soundClip = Resources.Load<AudioClip>("AR_Sounds/CatMeow"); // 파일이름은 에셋 폴더 내의 경로를 기준으로 함

        // AudioClip을 AudioSource에 할당
        soundSource.clip = soundClip;

        soundSource.Play();
    }

    public void CatJumpSfx()
    {
        // AudioClip을 에셋에서 불러와서 할당
        soundClip = Resources.Load<AudioClip>("AR_Sounds/CatJump"); // 파일이름은 에셋 폴더 내의 경로를 기준으로 함

        // AudioClip을 AudioSource에 할당
        soundSource.clip = soundClip;

        soundSource.Play();
    }
}
