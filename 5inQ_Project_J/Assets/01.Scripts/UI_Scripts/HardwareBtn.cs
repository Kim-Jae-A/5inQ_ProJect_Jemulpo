using UnityEngine;
using UnityEngine.SceneManagement;

public class HardwareBtn : MonoBehaviour
{
    [SerializeField] string previousScene;
    void Update()
    {
        if(Application.platform == RuntimePlatform.Android) //디바이스가 안드로이드 일때
        {
            if(SceneManager.GetActiveScene().name == "Home")
            {
                if (Input.GetKeyDown(KeyCode.Escape)) //뒤로가기 버튼을 누르면
                {
                    Application.Quit();//앱 종료
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape)) //뒤로가기 버튼을 누르면
                {
                    SceneManager.LoadScene(previousScene);
                }
            }

        }
        
    }
}
