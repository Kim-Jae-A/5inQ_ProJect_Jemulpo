using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class HardwareBtn : MonoBehaviour
{
    [SerializeField] string previousScene;
    void Update()
    {
        if(Application.platform == RuntimePlatform.Android) //디바이스가 안드로이드 일때
        {
            if(SceneManager.GetActiveScene().name == "SavePhoto" || SceneManager.GetActiveScene().name == "SaveVideo")
            {
                if (Input.GetKeyDown(KeyCode.Escape)) //뒤로가기 버튼을 누르면
                {
                    LoaderUtility.Deinitialize();
                    LoaderUtility.Initialize();
                    SceneManager.LoadScene("TakeAShot");
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
