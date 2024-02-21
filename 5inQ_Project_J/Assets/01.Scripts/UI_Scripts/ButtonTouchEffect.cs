using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonTouchEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Button targetButton; // ��� ��ư
    public string sceneToLoad; // ��ġ ���� �� �ε��� �� �̸�
    public float scaleMultiplier = 1.0f; // Ȯ�� ����, �ν����Ϳ��� ���� ����
    public float outlineWidth = 2f; // �׵θ� �β�, �ν����Ϳ��� ���� ����

    private Vector3 originalScale; // ��� ��ư�� ���� ������
    private Outline outline; // ��ư�� Outline ������Ʈ

    void Start()
    {
        targetButton = GetComponent<Button>();
        originalScale = targetButton.transform.localScale; // ���� �� ��ư�� ���� ������ ����
        // ��ư�� Outline ������Ʈ�� ������ �߰�
        outline = targetButton.gameObject.GetComponent<Outline>() ?? targetButton.gameObject.AddComponent<Outline>();
        outline.effectColor = Color.black; // �׵θ� ���� ����
        outline.effectDistance = new Vector2(outlineWidth, -outlineWidth); // �׵θ� �β� ����
        outline.useGraphicAlpha = false; // �׵θ��� ���� ���� �׷����� ���� ���� ���������� ����
        outline.enabled = false; // ���� �� �׵θ� ��Ȱ��ȭ
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        targetButton.transform.localScale = originalScale * scaleMultiplier; // ��ư Ȯ��
        outline.enabled = true; // �׵θ� Ȱ��ȭ
        // ��ư�� Canvas ������ ���� ������ �����ɴϴ�.
        targetButton.transform.SetAsLastSibling();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        targetButton.transform.localScale = originalScale; // ��ư�� ���� �����Ϸ� ����
        outline.enabled = false; // �׵θ� ��Ȱ��ȭ

        SceneManager.LoadScene(sceneToLoad); // ������ ������ �̵�
    }
}
