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

    [Header("API ����")]
    //string url = "https://api.vworld.kr/req/image?service=image&request=getmap&key=";
    public string url = "https://naveropenapi.apigw.ntruss.com/map-static/v2/raster"; // API ��û URL
    public float width; // ��û ���� �̹����� ��
    public float height; // ��û ���� �̹����� ����
    public int zoomLevel; // ��û ���� �̹����� Ȯ�� ����
    public string key_ID; // API ���̵�
    public string key;    // API Ű
    public RawImage map;  // �޾ƿ� �ؽ��ĸ� ������ ����
    string apiURL;

    [Header("����ġ")]
    [SerializeField] private Image myPoint;
    Vector2 myVector;

    private double center_lat;
    private double center_log;

    double _latitude;
    double _longitude;

    bool check;

    private void Awake()
    {
        // �ν��Ͻ��� null�� ��쿡�� ���� �ν��Ͻ��� �Ҵ�
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
        // �ȵ���̵忡���� ��ġ ���� ������ ��û
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
#endif

        // ���� ��û ���
        while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            yield return null;
        }

        // ��ġ ���� �ʱ�ȭ �� ��ġ ���� ��������
        InitializeLocationService();
    }

    void InitializeLocationService()
    {
        // ��ġ ���� �ʱ�ȭ
        Input.location.Start();

        // ��ġ ���� �ʱ�ȭ�� ��ٸ�
        StartCoroutine(UpdateLocation());
        //StartCoroutine(StaticMapDrawing());
    }

    IEnumerator UpdateLocation()
    {
        while (true)
        {
            // ��ġ ������ ������ ������ ���
            while (Input.location.status == LocationServiceStatus.Initializing)
            {
                yield return new WaitForSeconds(1);
            }

            // ��ġ ���� �������� ���� ��
            if (Input.location.status == LocationServiceStatus.Running)
            {
                // ���� ��ġ ������ ����ü�� ����
                _latitude = Input.location.lastData.latitude;
                _longitude = Input.location.lastData.longitude;
                if (!check)
                {
                    StartCoroutine(StaticMapDrawing()); // API ��û �ڷ�ƾ
                }
                check = true;

                myVector = ConvertGeoToUnityCoordinate(_latitude, _longitude);

                myPoint.transform.localPosition = new Vector3(myVector.x, myVector.y, 0);
            }      
            // ��� ��� �� �ٽ� ��ġ ������Ʈ
            yield return new WaitForSeconds(1);
        }
    }

    private Vector2 ConvertGeoToUnityCoordinate(double latitude, double longitude)
    {
        // ���� ����, �浵
        double originLatitude = center_lat;
        double originLongitude = center_log;

#if UNITY_EDITOR
        originLatitude = 37.713675f;
        originLongitude = 126.743572f;
#endif
        // ���� x, y
        double originX = 0;
        double originY = 0;

        // ����, �浵�� ���� x, y�� ��ȭ ����
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
        // ��ũ��Ʈ�� �ı��� �� ��ġ ���� ����
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
        apiURL = url + $"?w={width}&h={height}&center={_longitude},{_latitude}&level={zoomLevel}&scale=2"; // ���� ��ġ ��ǥ

#if UNITY_EDITOR
        //apiURL = url + $"?w={width}&h={height}&center=126.657566,37.466480&level={zoomLevel}&scale=2"; //��������
        apiURL = url + $"?w={width}&h={height}&center=126.743572,37.713675&level={zoomLevel}&scale=2"; // ����η�
#endif

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(apiURL);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", key_ID);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", key);

        yield return request.SendWebRequest();

        // ����ó�� ����ġ��
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
