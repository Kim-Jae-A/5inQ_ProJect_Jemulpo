using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class HardwareBtn : MonoBehaviour
{
    [SerializeField] string previousScene;
    void Update()
    {
        if(Application.platform == RuntimePlatform.Android) //����̽��� �ȵ���̵� �϶�
        {
            if(SceneManager.GetActiveScene().name == "SavePhoto" || SceneManager.GetActiveScene().name == "SaveVideo")
            {
                if (Input.GetKeyDown(KeyCode.Escape)) //�ڷΰ��� ��ư�� ������
                {
                    LoaderUtility.Deinitialize();
                    LoaderUtility.Initialize();
                    SceneManager.LoadScene("TakeAShot");
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
