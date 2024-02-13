using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using System.Collections;
using System.Collections.Generic;

public class GPSExample : MonoBehaviour
{
    public Text statusText;
    
    void Start()
    {
        
        // 위치 정보 권한 요청
        StartCoroutine(RequestLocationPermission());
    }

    IEnumerator RequestLocationPermission()
    {
#if UNITY_ANDROID
        // 안드로이드에서는 위치 정보 권한을 요청
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
#endif

        // 권한 요청 대기
        while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            yield return null;
        }

        // 위치 서비스 초기화 및 위치 정보 가져오기
        InitializeLocationService();
    }

    void InitializeLocationService()
    {
        // 위치 서비스 초기화
        Input.location.Start();

        // 위치 서비스 초기화를 기다림
        StartCoroutine(GetGPSLocation());
    }

    IEnumerator GetGPSLocation()
    {
        // 위치 정보를 가져올 때까지 대기
        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return new WaitForSeconds(1);
        }

        // 위치 정보 가져오기 성공 시
        if (Input.location.status == LocationServiceStatus.Running)
        {
            // 위도와 경도 출력
            float latitude = Input.location.lastData.latitude;
            float longitude = Input.location.lastData.longitude;

            statusText.text = "현재 위치:\n" + "위도: " + latitude + "\n경도: " + longitude;
        }
        else
        {
            // 위치 정보 가져오기 실패 시 메시지 표시
            statusText.text = "위치 정보를 가져오는 데 실패했습니다.";
        }

        // 위치 서비스 종료
        Input.location.Stop();
    }

    void OnDestroy()
    {
        // 스크립트가 파괴될 때 위치 서비스 종료
        Input.location.Stop();
    }
}
