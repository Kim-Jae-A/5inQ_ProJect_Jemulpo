using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Docent_AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator; //��� �� �ִϸ����� 
    [SerializeField] public AudioSource narration;//�����̼�

    public delegate void DocentCreated(Docent_AnimationController docent);
    public  static event DocentCreated OnDocentCreated;

    Docent_PlayerController playerController;

    void Start()
    {
        StartCoroutine(DocentAnimation()); //�������� �����Ǹ� �ڷ�ƾ ����
        OnDocentCreated?.Invoke(this);

    }

    //private void OnEnable()
    //{
    //    //����� ������ �ڵ�����, �������� �ִϸ��̼� Ŭ���� ����Ͽ� �ִϸ��̼��� ������� �������� �����ͷ�
    //    //�޾ƿ;� �ϴ� �����̹Ƿ� �������� �����Ǹ� DocentUI_Controller ������Ʈ�� ã�� ���������� �ִϸ��̼���
    //    //�ٽú���� ������¸� ǥ���ϴ� ����� ����� �� �ֽ��ϴ�.
    //    //playerController = GameObject.Find("DocentUI_Controller").GetComponent<Docent_PlayerController>();
    //    //playerController.animationController = this;// ���� �ִϸ����͸� ����ϹǷ� this ó��
        
    //}

    private void OnDisable()
    {
        playerController.animationController = null;//�������� ��Ȱ��ȭ �Ǹ� Docent_PlayerController�� �ִϸ����� null�� �Ҵ�
    }

    IEnumerator DocentAnimation() // �ִϸ��̼��� �����̼ǿ� �°� ����.
    {
        narration.Play();
        animator.Play("totalClip");
        yield return null;
    }


    public void RePlay_Docent()//�ٽú��� ���.
    {
        StopCoroutine(DocentAnimation());
        animator.Play("totalClip", -1, 0f);
        StartCoroutine(DocentAnimation());
    }
}