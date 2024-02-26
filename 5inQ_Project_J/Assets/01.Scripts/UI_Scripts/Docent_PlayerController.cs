using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Docent_PlayerController : MonoBehaviour
{
    [SerializeField] private Slider Docent_slider;//�ִϸ��̼� �����ǥ�� �����̴�
    [SerializeField] private Button BackToDescription;//���� �� �̵�
    [SerializeField] private Button Replay; //�ִϸ��̼� �ٽú��� ��ư
    public Docent_AnimationController animationController;//�������� �����Ǹ� ����Ǿ�� �ϱ� ������ public ���


    // ������ ���� �̺�Ʈ�� ���� ��������Ʈ�� �̺�Ʈ
    public delegate void DocentCreated(); 
    public static event DocentCreated OnARDocentCreated;

    void Start()
    {

        Replay.gameObject.SetActive(false);
        Docent_slider.gameObject.SetActive(false);
        OnARDocentCreated += () => //�������� �����Ǹ� OnARDocentCreated �̺�Ʈ�� ���� replay��ư�� �����̴��� Ȱ��ȭ.
        {
            Replay.gameObject.SetActive(true);
            Docent_slider.gameObject.SetActive(true);
            Docent_slider.maxValue = animationController.narration.clip.length;//gaebalone.length;  
            Replay.onClick.AddListener(animationController.RePlay_Docent);
        };
        //animationController = GetComponent<Docent_AnimationController>();   
        BackToDescription.onClick.AddListener(Return_Docent_Description);//�ڷΰ����ư �̺�Ʈ �߰�.
    }
    void Update()
    {
        //�������� �������� ������ 
        if (animationController != null && !Replay.gameObject.activeSelf)
        {
            OnARDocentCreated?.Invoke();
        }

        //�������� �����Ǹ� �ִϸ��̼��� ������ҽ��� �ð��� �����̴����� value�� ��������. ������� ��Ÿ����.
        float narrationProgress = animationController.narration.time;
        if (animationController.narration.isPlaying)
        {
            Docent_slider.value = narrationProgress;
        }
    }

    //���� ������ ���ư���(����Ʈ �󼼼���â���� ����Ʈ �ִϸ��̼� ���� ������ �Ѿ���� �� ������ ����Ʈ �󼼼���â ������ ���ư��ϴ�.)
    private void Return_Docent_Description()
    {
        string previousScene = PlayerPrefs.GetString("PreviousScene");
        if(!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
    }
}