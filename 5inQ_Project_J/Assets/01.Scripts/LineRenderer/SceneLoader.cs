using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // �̵��� ���� �̸�
    public string sceneName;

    // ��ư Ŭ�� �� ȣ���� �޼ҵ�
    public void LoadScene()
    {
        // sceneName�� ������ �̸��� ���� �ε��մϴ�.
        SceneManager.LoadScene(sceneName);
    }
}
