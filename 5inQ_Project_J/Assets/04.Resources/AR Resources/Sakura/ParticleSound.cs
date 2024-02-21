using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSound : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public AudioClip sparkleSound; // ��¦�̴� ȿ����

    private AudioSource audioSource;

    void Start()
    {
        if (particleSystem == null)
        {
            particleSystem = GetComponent<ParticleSystem>();
        }

        // AudioSource ������Ʈ �߰� �� ����
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // ��ƼŬ�� �����Ǵ� ����
        if (particleSystem.isPlaying)
        {
            // ��ƼŬ�� ���� ������ ������ ȿ������ ���
            if (particleSystem.particleCount > 0 && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(sparkleSound);
            }
        }
    }
}
