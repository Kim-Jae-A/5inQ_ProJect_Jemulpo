using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Docent_AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator; //사용 할 애니메이터 
    [SerializeField] public AudioSource narration;//나레이션

    public delegate void DocentCreated(Docent_AnimationController docent);
    public  static event DocentCreated OnDocentCreated;

    Docent_PlayerController playerController;

    void Start()
    {
        StartCoroutine(DocentAnimation()); //프리팹이 생성되면 코루틴 시작
        OnDocentCreated?.Invoke(this);

    }
    private void OnDisable()
    {
        playerController.animationController = null;//프리팹이 비활성화 되면 Docent_PlayerController의 애니메이터 null값 할당
    }

    IEnumerator DocentAnimation() // 애니메이션을 나레이션에 맞게 제어.
    {
        narration.Play();
        animator.Play("totalClip");
        yield return null;
    }


    public void RePlay_Docent()//다시보기 기능.
    {
        StopCoroutine(DocentAnimation());
        animator.Play("totalClip", -1, 0f);
        StartCoroutine(DocentAnimation());
    }
}