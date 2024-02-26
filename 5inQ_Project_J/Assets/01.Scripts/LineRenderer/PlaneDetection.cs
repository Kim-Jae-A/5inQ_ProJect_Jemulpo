using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

// ARRay를 발사하여 Plane에 닿으면 Indicator를 띄워준다.
[RequireComponent(typeof(ARRaycastManager))]
public class PlaneDetection : MonoBehaviour
{
    [SerializeField] Transform spawnedObject;
    [SerializeField] Transform indicator;
    ARRaycastManager raycastManager;
    [SerializeField] float zoomSpeed = 0.05f;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        LocateIndicatorAtScreenCenter();

        SpawnObjectByClick();
        ZoomInOutObjectByWheel();

        if (Input.touchCount == 0) return;

        SpawnObjectByTouch();
        ZoomInOutObjectByTouch();
    }

    /// <summary>
    /// 마우스 휠로 물체의 크기를 조절합니다.
    /// </summary>
    private void ZoomInOutObjectByWheel()
    {
        // 마우스 휠(또는 드래그)로 스케일 조정
        if (Input.mouseScrollDelta.y != 0)
        {
            float scroll = Input.mouseScrollDelta.y; // 마우스 스크롤 변화량(+, -)
            if (spawnedObject != null)
            {
                spawnedObject.localScale += Vector3.one * scroll * zoomSpeed; // 스크롤 변화량의 속도에 따라 스케일 변경
                spawnedObject.localScale = new Vector3(
                    Mathf.Clamp(spawnedObject.localScale.x, 0.1f, 5f),
                    Mathf.Clamp(spawnedObject.localScale.y, 0.1f, 5f),
                    Mathf.Clamp(spawnedObject.localScale.z, 0.1f, 5f));
            }
        }
    }

    /// <summary>
    /// 마우스 클릭으로 물체를 스폰합니다.
    /// </summary>
    private void SpawnObjectByClick()
    {
        // 마우스 클릭으로 오브젝트 배치
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;
            List<ARRaycastHit> hitInfo = new List<ARRaycastHit>();
            if (raycastManager.Raycast(mousePos, hitInfo, TrackableType.Planes))
            {
                Pose hitPose = hitInfo[0].pose;
                if (spawnedObject != null)
                {
                    spawnedObject.position = hitPose.position;
                    spawnedObject.rotation = hitPose.rotation;
                }
            }
        }
    }

    private void SpawnObjectByTouch()
    {
        if (Input.touchCount == 1)
        {
            // Input.GetMouseButtonDown(0)
            Touch touch = Input.GetTouch(0); // 스마트폰 스크린 터치용으로만 사용가능

            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPoint = touch.position;

                List<ARRaycastHit> hitInfo = new List<ARRaycastHit>();

                if (raycastManager.Raycast(touchPoint, hitInfo, TrackableType.Planes))
                {
                    Pose hitPose = hitInfo[0].pose;
                    if (spawnedObject != null)
                    {
                        spawnedObject.position = hitPose.position;
                        spawnedObject.rotation = hitPose.rotation;
                        spawnedObject.gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (spawnedObject != null)
                        spawnedObject.gameObject.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// 스크린 터치로 물체의 크기를 키우고 줄입니다.
    /// </summary>
    private void ZoomInOutObjectByTouch()
    {
        // 스크린에 두 개의 손가락 터치가 있는지 확인
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // 두 터치 간의 현재 위치와 이전 위치를 기반으로 거리를 계산
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // 거리 차이를 계산하여 핀치 줌 크기를 결정
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // 오브젝트의 스케일을 조절
            // 핀치를 하여 화면을 벌릴 때 오브젝트를 확대하고, 핀치를 모을 때 오브젝트를 축소
            spawnedObject.localScale -= Vector3.one * deltaMagnitudeDiff * zoomSpeed;

            // 스케일의 최소값과 최대값을 제한 (옵션)
            spawnedObject.localScale = new Vector3(
                Mathf.Clamp(spawnedObject.localScale.x, 0.1f, 5f),
                Mathf.Clamp(spawnedObject.localScale.y, 0.1f, 5f),
                Mathf.Clamp(spawnedObject.localScale.z, 0.1f, 5f));
        }
    }

    /// <summary>
    /// 스크린 터치로 물체를 스폰합니다.
    /// </summary>
    private void LocateIndicatorAtScreenCenter()
    {
        // Screen의 중심에서 ARRay를 전방으로 발사한다.
        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        // ARRay가 닿은 정보들
        List<ARRaycastHit> hitInfo = new List<ARRaycastHit>();

        // ARRay를 발사
        if (raycastManager.Raycast(screenCenter, hitInfo, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
        {
            indicator.position = hitInfo[0].pose.position;
            indicator.rotation = hitInfo[0].pose.rotation;
            indicator.gameObject.SetActive(true);
        }
        else
        {
            indicator.gameObject.SetActive(false);
        }
    }
}
