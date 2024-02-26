using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Docent_AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator; //��� �� �ִϸ����� 
    [SerializeField] public AudioSource narration;//�����̼�

    Docent_PlayerController playerController;

    void Start()
    {
        StartCoroutine(DocentAnimation()); //�������� �����Ǹ� �ڷ�ƾ ����
        narration = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        //����� ������ �ڵ�����, �������� �ִϸ��̼� Ŭ���� ����Ͽ� �ִϸ��̼��� ������� �������� �����ͷ�
        //�޾ƿ;� �ϴ� �����̹Ƿ� �������� �����Ǹ� DocentUI_Controller ������Ʈ�� ã�� ���������� �ִϸ��̼���
        //�ٽú���� ������¸� ǥ���ϴ� ����� ����� �� �ֽ��ϴ�.
        playerController = GameObject.Find("DocentUI_Controller").GetComponent<Docent_PlayerController>();
        
        playerController.animationController = this;// ���� �ִϸ����͸� ����ϹǷ� this ó��
        
    }

    private void OnDisable()
    {
        playerController.animationController = null;//�������� ��Ȱ��ȭ �Ǹ� Docent_PlayerController�� �ִϸ����� null�� �Ҵ�
    }
    private void Update()
    {
        Vector3 position = transform.position;
        position.y = 0; // ���ϴ� y ��ġ�� ����
        transform.position = position;
    }

    IEnumerator DocentAnimation() // �ִϸ��̼��� �����̼ǿ� �°� ����.
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


    public void RePlay_Docent()//�ٽú��� ���.
    {
        StopCoroutine(DocentAnimation());
        animator.SetTrigger("Replay");
        StartCoroutine(DocentAnimation());
    }
}