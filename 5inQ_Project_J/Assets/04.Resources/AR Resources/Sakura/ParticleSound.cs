using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSound : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public AudioClip sparkleSound; // 반짝이는 효과음

    private AudioSource audioSource;

    void Start()
    {
        if (particleSystem == null)
        {
            particleSystem = GetComponent<ParticleSystem>();
        }

        // AudioSource 컴포넌트 추가 및 설정
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // 파티클이 생성되는 동안
        if (particleSystem.isPlaying)
        {
            // 파티클이 새로 생성될 때마다 효과음을 재생
            if (particleSystem.particleCount > 0 && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(sparkleSound);
            }
        }
    }
}
