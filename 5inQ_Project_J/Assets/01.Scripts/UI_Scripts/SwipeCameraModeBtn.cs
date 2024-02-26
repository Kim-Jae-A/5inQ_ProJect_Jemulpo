using UnityEngine;


public class SwipeCameraModeBtn : MonoBehaviour
{
    //Swipe
    [SerializeField] GameObject photoZone_UI;//PhotoZoneUI_Btn ��ũ��Ʈ�� ������ ���� ������Ʈ
    [SerializeField] RectTransform swipePanel; // ���������� �г�

    PhotoZoneUI_Btn photoZoneUI_Btn; 
    Animator animator;

    //���������� ���� ��ġ
    Vector2 fingerDownPosition; 
    Vector2 fingerUpPosition;
    bool detectSwipeOnlyAfterRelease = false; //�������� ������ ��ġ�� ������ ���� �����ϵ��� ����

    // ���������� �ּ� ����
    float minDistanceForSwipe = 20f;

    void Awake()
    {
        photoZoneUI_Btn = photoZone_UI.GetComponent<PhotoZoneUI_Btn>();
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        // ��ġ�� �߻��ߴ��� Ȯ��
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // ��ġ�� �г� ������ �߻��ߴ��� Ȯ��
            if (RectTransformUtility.RectangleContainsScreenPoint(swipePanel, touch.position))
            {
                // ��ġ�� ���� ���� ���
                if (touch.phase == TouchPhase.Began)
                {
                    fingerDownPosition = touch.position;
                    fingerUpPosition = touch.position;
                }

                // ��ġ�� ������ �� �������� ����
                if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
                {
                    fingerUpPosition = touch.position;
                    CheckSwipe();
                }

                // ��ġ�� ������ �� �������� ���� (��ġ ������ ��)
                if (touch.phase == TouchPhase.Ended)
                {
                    fingerUpPosition = touch.position;
                    CheckSwipe();
                }
            }
        }
    }

    void CheckSwipe()
    {
        // �������� ���� ���
        float deltaX = fingerUpPosition.x - fingerDownPosition.x;

        // ���� ��������
        if (Mathf.Abs(deltaX) > minDistanceForSwipe)
        {
            if (deltaX > 0)
            {
                if(CameraMode.isPhoto == false)//�ִϸ��̼� �ݺ����� �ʱ� ���� ����
                {
                    animator.SetTrigger("doRight"); //�ִϸ��̼� Ȱ��ȭ
                    photoZoneUI_Btn.OnPhotoBtn(); //PhotoMode�� �´� ��ư Ȱ��ȭ
                }                 
            }
            else if (deltaX < 0)
            {
                if(CameraMode.isVideo == false)
                {
                    animator.SetTrigger("doLeft"); //�ִϸ��̼� Ȱ��ȭ
                    photoZoneUI_Btn.OnVideoBtn(); //VideoMode�� �´� ��ư Ȱ��ȭ
                }               
            }
        }       
    }
}
