using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // 이동할 씬의 이름
    public string sceneName;

    // 버튼 클릭 시 호출할 메소드
    public void LoadScene()
    {
        // sceneName에 지정된 이름의 씬을 로드합니다.
        SceneManager.LoadScene(sceneName);
    }
}
