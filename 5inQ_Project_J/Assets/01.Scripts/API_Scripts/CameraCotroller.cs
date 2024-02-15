using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class CameraCotroller : MonoBehaviour
{
    private Vector3 touchStart;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // 터치 시작 위치 저장
                    touchStart = Camera.main.ScreenToWorldPoint(touch.position);
                    break;
                case TouchPhase.Moved:
                    // 드래그 중 카메라 이동 계산
                    Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(touch.position);
                    Camera.main.transform.position += direction;
                    break;
            }
        }
    }
}
