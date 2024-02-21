using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonTouchEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Button targetButton; // 대상 버튼
    public string sceneToLoad; // 터치 종료 시 로드할 씬 이름
    public float scaleMultiplier = 1.0f; // 확대 비율, 인스펙터에서 조정 가능
    public float outlineWidth = 2f; // 테두리 두께, 인스펙터에서 조정 가능

    private Vector3 originalScale; // 대상 버튼의 원래 스케일
    private Outline outline; // 버튼의 Outline 컴포넌트

    void Start()
    {
        targetButton = GetComponent<Button>();
        originalScale = targetButton.transform.localScale; // 시작 시 버튼의 원래 스케일 저장
        // 버튼에 Outline 컴포넌트가 없으면 추가
        outline = targetButton.gameObject.GetComponent<Outline>() ?? targetButton.gameObject.AddComponent<Outline>();
        outline.effectColor = Color.black; // 테두리 색상 설정
        outline.effectDistance = new Vector2(outlineWidth, -outlineWidth); // 테두리 두께 설정
        outline.useGraphicAlpha = false; // 테두리의 알파 값을 그래픽의 알파 값과 독립적으로 설정
        outline.enabled = false; // 시작 시 테두리 비활성화
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        targetButton.transform.localScale = originalScale * scaleMultiplier; // 버튼 확대
        outline.enabled = true; // 테두리 활성화
        // 버튼을 Canvas 내에서 가장 앞으로 가져옵니다.
        targetButton.transform.SetAsLastSibling();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        targetButton.transform.localScale = originalScale; // 버튼을 원래 스케일로 복구
        outline.enabled = false; // 테두리 비활성화

        SceneManager.LoadScene(sceneToLoad); // 지정된 씬으로 이동
    }
}
