using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// 로딩화면
/// </summary>
public class FadeInAndLoadScene : MonoBehaviour
{
    public Image LogoImage;
    public Text LogoText;
    public string SceneName;

    //로고,텍스트 페이드인 효과 시간
    public float fadeInDuration = 1.0f;

    //로딩창 딜레이 시간
    public float delayBeforeLoad = 1.0f;

    private float currentAlpha = 0.0f;
    private float startTime;

    void Start()
    {
        startTime = Time.time;

        LogoImage.color = new Color(LogoImage.color.r, LogoImage.color.g, LogoImage.color.b, 0);
        LogoText.color = new Color(LogoText.color.r, LogoText.color.g, LogoText.color.b, 0);
    }

    void Update()
    {
        
        float elapsedTime = Time.time - startTime;
        currentAlpha = Mathf.Lerp(0, 1, elapsedTime / fadeInDuration);
 
        LogoImage.color = new Color(LogoImage.color.r, LogoImage.color.g, LogoImage.color.b, currentAlpha);
        LogoText.color = new Color(LogoText.color.r, LogoText.color.g, LogoText.color.b, currentAlpha);

        if (elapsedTime >= delayBeforeLoad)
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
