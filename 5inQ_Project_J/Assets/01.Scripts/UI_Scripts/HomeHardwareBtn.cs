using UnityEngine;

public class HomeHardwareBtn : MonoBehaviour
{
    [SerializeField] GameObject appQuitMessage; //���� �޼���
    [SerializeField] GameObject greyPanel; //���� �޽��� ���� �ڿ� ȸ��ȭ��
    void Start()
    {
        appQuitMessage.SetActive(false);
        greyPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) //�ڷΰ��� ��ư�� ������
        {
            appQuitMessage.SetActive(true);
            greyPanel.SetActive(true);
            Time.timeScale = 0; //�ڿ� ��ư �������ʰ� timeScale�� 0���� ����
        }
    }

    public void OnYesBtn()
    {
        Application.Quit();
    }

    public void OnNoBtn()
    {
        appQuitMessage.SetActive(false);
        greyPanel.SetActive(false);
        Time.timeScale = 1; 
    }
}
