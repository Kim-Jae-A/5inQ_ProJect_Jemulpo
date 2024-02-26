using UnityEngine;


public class SwipeCameraModeBtn : MonoBehaviour
{
    //Swipe
    [SerializeField] GameObject photoZone_UI;//PhotoZoneUI_Btn 스크립트를 가져올 게임 오브젝트
    [SerializeField] RectTransform swipePanel; // 스와이프할 패널

    PhotoZoneUI_Btn photoZoneUI_Btn; 
    Animator animator;

    //스와이프한 값의 위치
    Vector2 fingerDownPosition; 
    Vector2 fingerUpPosition;
    bool detectSwipeOnlyAfterRelease = false; //스와이프 동작을 터치가 끝났을 때만 감지하도록 설정

    // 스와이프의 최소 길이
    float minDistanceForSwipe = 20f;

    void Awake()
    {
        photoZoneUI_Btn = photoZone_UI.GetComponent<PhotoZoneUI_Btn>();
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        // 터치가 발생했는지 확인
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // 터치가 패널 내에서 발생했는지 확인
            if (RectTransformUtility.RectangleContainsScreenPoint(swipePanel, touch.position))
            {
                // 터치의 시작 지점 기록
                if (touch.phase == TouchPhase.Began)
                {
                    fingerDownPosition = touch.position;
                    fingerUpPosition = touch.position;
                }

                // 터치가 끝났을 때 스와이프 감지
                if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
                {
                    fingerUpPosition = touch.position;
                    CheckSwipe();
                }

                // 터치가 끝났을 때 스와이프 감지 (터치 릴리즈 후)
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
        // 스와이프 길이 계산
        float deltaX = fingerUpPosition.x - fingerDownPosition.x;

        // 가로 스와이프
        if (Mathf.Abs(deltaX) > minDistanceForSwipe)
        {
            if (deltaX > 0)
            {
                if(CameraMode.isPhoto == false)//애니메이션 반복되지 않기 위한 조건
                {
                    animator.SetTrigger("doRight"); //애니메이션 활성화
                    photoZoneUI_Btn.OnPhotoBtn(); //PhotoMode에 맞는 버튼 활성화
                }                 
            }
            else if (deltaX < 0)
            {
                if(CameraMode.isVideo == false)
                {
                    animator.SetTrigger("doLeft"); //애니메이션 활성화
                    photoZoneUI_Btn.OnVideoBtn(); //VideoMode에 맞는 버튼 활성화
                }               
            }
        }       
    }
}
