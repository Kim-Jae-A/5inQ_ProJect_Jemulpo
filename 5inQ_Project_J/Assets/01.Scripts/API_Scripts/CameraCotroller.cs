using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class CameraCotroller : MonoBehaviour
{
    float speed = 10.0f;
    private Vector2 nowPos, prePos;
    private Vector3 movePos;

    float m_fSpeed = 0.1f;       // 변경 속도를 설정합니다 
    float m_fFieldOfView = 60f;     // 카메라의 FieldOfView의 기본값을 60으로 정합니다.

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)  // 화면 터치 1개
        {
            Touch touch = Input.GetTouch(0); // 첫번째 터치 지점

            if (touch.phase == TouchPhase.Began)  // 움직임 시작
            {
                prePos = touch.position - touch.deltaPosition;  // 처음 지점에서 움직인 지점까지의 차이
            }
            else if (touch.phase == TouchPhase.Moved) // 움직임 끝날때
            {
                nowPos = touch.position - touch.deltaPosition;
                movePos = Time.deltaTime * speed * (Vector3)(prePos - nowPos);
                transform.Translate(movePos);  // 이동한 만큼 화면 이동
                prePos = touch.position - touch.deltaPosition;
            }
        }
        else if(Input.touchCount == 2) // 화면 터치 2개
        {
            Vector2 vecPreTouchPos0 = Input.touches[0].position - Input.touches[0].deltaPosition;
            Vector2 vecPreTouchPos1 = Input.touches[1].position - Input.touches[1].deltaPosition;

            // 이전 두 터치의 차이 
            float fPreDis = (vecPreTouchPos0 - vecPreTouchPos1).magnitude;
            // 현재 두 터치의 차
            float fToucDis = (Input.touches[0].position - Input.touches[1].position).magnitude;


            // 이전 두 터치의 거리와 지금 두 터치의 거리의 차이
            float fDis = fPreDis - fToucDis;

            // 이전 두 터치의 거리와 지금 두 터치의 거리의 차이를 FleldOfView를 차감합니다.
            m_fFieldOfView += (fDis * m_fSpeed);

            // 최대는 100, 최소는 20으로 더이상 증가 혹은 감소가 되지 않도록 합니다.
            m_fFieldOfView = Mathf.Clamp(m_fFieldOfView, 20.0f, 100.0f);

            Camera.main.fieldOfView = m_fFieldOfView;
        }
    }
}
