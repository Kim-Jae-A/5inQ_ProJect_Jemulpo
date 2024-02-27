using UnityEngine;
using UnityEngine.SceneManagement;

public class HardwareBtn : MonoBehaviour
{
    [SerializeField] string previousScene;
    void Update()
    {
        if(Application.platform == RuntimePlatform.Android) //����̽��� �ȵ���̵� �϶�
        {
            if(SceneManager.GetActiveScene().name == "Home")
            {
                if (Input.GetKeyDown(KeyCode.Escape)) //�ڷΰ��� ��ư�� ������
                {
                    Application.Quit();//�� ����
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape)) //�ڷΰ��� ��ư�� ������
                {
                    SceneManager.LoadScene(previousScene);
                }
            }

        }
        
    }
}
