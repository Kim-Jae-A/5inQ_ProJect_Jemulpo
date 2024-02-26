using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using System.Collections;

public class GPSmanager : MonoBehaviour
{
    public float latitude;
    public float longitude;
    public static GPSmanager instance;
    public static Vector2 GetLocation()
    {
        return new Vector2(instance.latitude, instance.longitude);
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
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
        StartCoroutine(UpdateLocation());
    }

    IEnumerator UpdateLocation()
    {
        while (true)
        {
            // 위치 정보를 가져올 때까지 대기
            while (Input.location.status == LocationServiceStatus.Initializing)
            {
                yield return new WaitForSeconds(1);
            }

            // 위치 정보 가져오기 성공 시
            if (Input.location.status == LocationServiceStatus.Running)
            {
                // 현재 위치 정보를 구조체에 저장
                latitude = Input.location.lastData.latitude;
                longitude = Input.location.lastData.longitude;

                // 상태 텍스트 업데이트
                Debug.Log("현재 위치:\n" + "위도: " + latitude.ToString("F6") + "\n경도: " + longitude.ToString("F6"));
            }
            else
            {
                // 위치 정보 가져오기 실패 시 메시지 표시
                Debug.Log("위치 정보를 가져오는 데 실패했습니다.");
            }

            // 잠시 대기 후 다시 위치 업데이트
            yield return new WaitForSeconds(1);
        }
    }
    private void OnDestroy()
    {
        Input.location.Stop();
    }
}
