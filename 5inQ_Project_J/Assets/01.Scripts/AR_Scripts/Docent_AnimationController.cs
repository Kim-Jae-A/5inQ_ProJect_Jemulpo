using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Docent_AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator; //사용 할 애니메이터 
    [SerializeField] public AudioSource narration;//나레이션

    Docent_PlayerController playerController;

    void Start()
    {
        StartCoroutine(DocentAnimation()); //프리팹이 생성되면 코루틴 시작
        narration = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        //사용을 꺼리는 코드지만, 여러개의 애니메이션 클립을 사용하여 애니메이션의 진행률을 프리팹의 데이터로
        //받아와야 하는 상태이므로 프리팹이 생성되면 DocentUI_Controller 오브젝트를 찾아 연결시켜줘야 애니메이션의
        //다시보기와 진행상태를 표시하는 기능을 사용할 수 있습니다.
        playerController = GameObject.Find("DocentUI_Controller").GetComponent<Docent_PlayerController>();
        
        playerController.animationController = this;// 같은 애니메이터를 사용하므로 this 처리
        
    }

    private void OnDisable()
    {
        playerController.animationController = null;//프리팹이 비활성화 되면 Docent_PlayerController의 애니메이터 null값 할당
    }
    private void Update()
    {
        Vector3 position = transform.position;
        position.y = 0; // 원하는 y 위치로 설정
        transform.position = position;
    }

    IEnumerator DocentAnimation() // 애니메이션을 나레이션에 맞게 제어.
    {
        narration.Play();
        yield return new WaitForSeconds(1.2f);
        animator.SetTrigger("Landing");
        yield return new WaitForSeconds(3F);
        animator.SetTrigger("Hello");
        yield return new WaitForSeconds(4.5f);
        animator.SetTrigger("Gesture");
        yield return new WaitForSeconds(4.5f);
        animator.SetTrigger("Study");
        yield return new WaitForSeconds(8);
        animator.SetTrigger("Large");
        yield return new WaitForSeconds(5.6f);
        animator.SetTrigger("Explain");
        yield return new WaitForSeconds(7.3f);
        animator.SetTrigger("Good");
        yield return new WaitForSeconds(4);
        animator.SetTrigger("Gesture2");
        yield return new WaitForSeconds(6.5f);
        animator.SetTrigger("Bye");
    }


    public void RePlay_Docent()//다시보기 기능.
    {
        StopCoroutine(DocentAnimation());
        animator.SetTrigger("Replay");
        StartCoroutine(DocentAnimation());
    }
}