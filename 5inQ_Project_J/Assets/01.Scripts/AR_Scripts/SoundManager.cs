using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // �Ҹ��� ����� AudioSource ����
    private AudioSource soundSource; 
    
    AudioClip soundClip;

    private void Awake()
    {
        soundSource = GetComponent<AudioSource>();
    }

    public void TrainMoveSfx()
    {
        // AudioClip�� ���¿��� �ҷ��ͼ� �Ҵ�
        soundClip = Resources.Load<AudioClip>("AR_Sounds/TrainMove"); // �����̸��� ���� ���� ���� ��θ� �������� ��

        // AudioClip�� AudioSource�� �Ҵ�
        soundSource.clip = soundClip;

        soundSource.Play();
    }

    public void TrainIdleSfx()
    {
        // AudioClip�� ���¿��� �ҷ��ͼ� �Ҵ�
        soundClip = Resources.Load<AudioClip>("AR_Sounds/TrainIdle"); // �����̸��� ���� ���� ���� ��θ� �������� ��

        // AudioClip�� AudioSource�� �Ҵ�
        soundSource.clip = soundClip;

        soundSource.Play();
    }

    public void CatMeowSfx()
    {
        // AudioClip�� ���¿��� �ҷ��ͼ� �Ҵ�
        soundClip = Resources.Load<AudioClip>("AR_Sounds/CatMeow"); // �����̸��� ���� ���� ���� ��θ� �������� ��

        // AudioClip�� AudioSource�� �Ҵ�
        soundSource.clip = soundClip;

        soundSource.Play();
    }

    public void CatJumpSfx()
    {
        // AudioClip�� ���¿��� �ҷ��ͼ� �Ҵ�
        soundClip = Resources.Load<AudioClip>("AR_Sounds/CatJump"); // �����̸��� ���� ���� ���� ��θ� �������� ��

        // AudioClip�� AudioSource�� �Ҵ�
        soundSource.clip = soundClip;

        soundSource.Play();
    }
}
