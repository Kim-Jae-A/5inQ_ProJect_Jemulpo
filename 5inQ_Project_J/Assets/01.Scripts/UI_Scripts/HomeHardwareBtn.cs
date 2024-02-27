using UnityEngine;

public class HomeHardwareBtn : MonoBehaviour
{
    [SerializeField] GameObject appQuitMessage; //종료 메세지
    [SerializeField] GameObject greyPanel; //종료 메시지 띄우고 뒤에 회색화면
    void Start()
    {
        appQuitMessage.SetActive(false);
        greyPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) //뒤로가기 버튼을 누르면
        {
            appQuitMessage.SetActive(true);
            greyPanel.SetActive(true);
            Time.timeScale = 0; //뒤에 버튼 눌리지않게 timeScale을 0으로 설정
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
