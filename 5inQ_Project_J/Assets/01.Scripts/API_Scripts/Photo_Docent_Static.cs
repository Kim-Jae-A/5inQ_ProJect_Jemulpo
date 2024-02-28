using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Networking;
using UnityEngine.UI;
using static UnityEngine.XR.ARSubsystems.XRCpuImage;

public class Photo_Docent_Static : MonoBehaviour
{
    public static Photo_Docent_Static instance;

    [Header("API 설정")]
    //string url = "https://api.vworld.kr/req/image?service=image&request=getmap&key=";
    public string url = "https://naveropenapi.apigw.ntruss.com/map-static/v2/raster"; // API 요청 URL
    public float width; // 요청 받을 이미지의 폭
    public float height; // 요청 받을 이미지의 높이
    public int zoomLevel; // 요청 받을 이미지의 확대 정도
    public string key_ID; // API 아이디
    public string key;    // API 키
    public RawImage map;  // 받아온 텍스쳐를 적용할 공간
    string apiURL;

    [Header("내위치")]
    [SerializeField] private Image myPoint;
    Vector2 myVector;

    private double center_lat;
    private double center_log;

    double _latitude;
    double _longitude;

    bool check;

    private void Awake()
    {
        // 인스턴스가 null일 경우에만 현재 인스턴스를 할당
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
                _latitude = Input.location.lastData.latitude;
                _longitude = Input.location.lastData.longitude;
                if (!check)
                {
                    StartCoroutine(StaticMapDrawing()); // API 요청 코루틴
                }
                check = true;

                myVector = ConvertGeoToUnityCoordinate(_latitude, _longitude);

                myPoint.transform.localPosition = new Vector3(myVector.x, myVector.y, 0);
            }      
            // 잠시 대기 후 다시 위치 업데이트
            yield return new WaitForSeconds(1);
        }
    }

    private Vector2 ConvertGeoToUnityCoordinate(double latitude, double longitude)
    {
        // 기준 위도, 경도
        double originLatitude = center_lat;
        double originLongitude = center_log;

#if UNITY_EDITOR
        originLatitude = 37.713675f;
        originLongitude = 126.743572f;
#endif
        // 기준 x, y
        double originX = 0;
        double originY = 0;

        // 위도, 경도에 대한 x, y의 변화 비율
        double xRatio = 172238.37f;
        double yRatio = 265780.73f;

        double x;
        double y;

        if (longitude - originLongitude == 0)
        {
            x = 0; y = 0;
        }
        else
        {
            x = originX + (longitude - originLongitude) * xRatio;
            y = originY + (latitude - originLatitude) * yRatio;
        }

        return new Vector2((float)x, (float)y);
    }

    void OnDestroy()
    {
        // 스크립트가 파괴될 때 위치 서비스 종료
        Input.location.Stop();
    }

    public void DrawingStart()
    {
        StartCoroutine(RequestLocationPermission());
#if UNITY_EDITOR
        StartCoroutine(StaticMapDrawing());
#endif
    }

    IEnumerator StaticMapDrawing()
    {
        apiURL = url + $"?w={width}&h={height}&center={_longitude},{_latitude}&level={zoomLevel}&scale=2"; // 현재 위치 좌표

#if UNITY_EDITOR
        //apiURL = url + $"?w={width}&h={height}&center=126.657566,37.466480&level={zoomLevel}&scale=2"; //제물포역
        apiURL = url + $"?w={width}&h={height}&center=126.743572,37.713675&level={zoomLevel}&scale=2"; // 경기인력
#endif

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(apiURL);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", key_ID);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", key);

        yield return request.SendWebRequest();

        // 예외처리 스위치문
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
            Debug.Log(request.result.ToString());
            map.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
}
