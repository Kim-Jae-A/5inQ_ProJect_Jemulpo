using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Android;

public class StaticMapManager : MonoBehaviour
{
    string url = "https://naveropenapi.apigw.ntruss.com/map-static/v2/raster";
    string key_ID = "yxpbvkcpme";
    string key = "0yutJDRB0oHCq9pOLk9u060XEpQlEMZL7sRShb9t";
    public RawImage map;
    string apiURL;
    bool check;

    public static float latitude;  // 위도
    public static float longitude; // 경도

    void Start()
    {
        // 위치 정보 권한 요청
        StartCoroutine(RequestLocationPermission());
#if UNITY_EDITOR
        StartCoroutine(StaticMapDrawing());
#endif
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
        //StartCoroutine(StaticMapDrawing());
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

                if (!check)
                {
                    StartCoroutine(StaticMapDrawing());
                }
                check = true;
            }
            // 잠시 대기 후 다시 위치 업데이트
            yield return new WaitForSeconds(1);
        }
    }

    void OnDestroy()
    {
        // 스크립트가 파괴될 때 위치 서비스 종료
        Input.location.Stop();
    }

    IEnumerator StaticMapDrawing()
    {
        apiURL = url + $"?w=1000&h=1000&center={longitude},{latitude}&level=16&scale=2"; // 현재 위치 좌표
#if UNITY_EDITOR
        apiURL = url + $"?w=1000&h=1000&center=126.657566,37.466480&level=17&scale=2"; //제물포역
#endif
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(apiURL);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", key_ID);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", key);

        yield return request.SendWebRequest();
        switch (request.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                Debug.LogWarning(request.result.ToString());
                yield break;
            case UnityWebRequest.Result.Success:
                Debug.LogWarning(request.result.ToString());
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogWarning(request.result.ToString());
                yield break;
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogWarning(request.result.ToString());
                yield break;
        }
        if (request.isDone)
        {
            Debug.LogWarning(request.result.ToString());
            map.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
}
