using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Android;

public class StaticMapManager : MonoBehaviour
{
    public static StaticMapManager instance;

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
    bool check;

    [Header("��Ŀ �����")]
    [SerializeField]private MapUI_Enum[] mapui;
    [Header("����ġ")]
    [SerializeField] private Image myPoint;
    Vector2 myVector;

    private double center_lat;
    private double center_log;

    public static float latitude;  // ����
    public static float longitude; // �浵

    void Awake()
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

    void Start()
    {
        // ��ġ ���� ���� ��û
        StartCoroutine(RequestLocationPermission());
#if UNITY_EDITOR
        StartCoroutine(StaticMapDrawing());
#endif
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
                latitude = Input.location.lastData.latitude;
                longitude = Input.location.lastData.longitude;

                if (!check)
                {
                    for (int i = 0; i< mapui.Length; i++)
                    {
                        mapui[i].LoadingMarker();
                    }                
                    StartCoroutine(StaticMapDrawing()); // API ��û �ڷ�ƾ
                }
                check = true;

                myVector = ConvertGeoToUnityCoordinate(latitude, longitude);
                
                myPoint.transform.localPosition = new Vector3(myVector.x, myVector.y, 0);
            }
            // ��� ��� �� �ٽ� ��ġ ������Ʈ
            yield return new WaitForSeconds(1);
        }
    }

    void OnDestroy()
    {
        // ��ũ��Ʈ�� �ı��� �� ��ġ ���� ����
        Input.location.Stop();
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

    IEnumerator StaticMapDrawing()
    {
        center_lat = latitude;
        center_log = longitude;
        apiURL = url + $"?w={width}&h={height}&center={longitude},{latitude}&level={zoomLevel}&scale=2"; // ���� ��ġ ��ǥ
#if UNITY_EDITOR
        //apiURL = url + $"?w={width}&h={height}&center=126.657566,37.466480&level={zoomLevel}&scale=2"; //��������
        apiURL = url + $"?w={width}&h={height}&center=126.743572,37.713675&level={zoomLevel}&scale=2"; // ����η�
#endif
        /*        apiURL = url + $"{key}&format=png&basemap=GRAPHIC&center={longitude},{latitude}&crs=epsg:4326&zoom={zoomLevel}&size={width},{height}";// ���� ��ġ ��ǥ
        #if UNITY_EDITOR
                apiURL = url + $"{key}&format=png&basemap=GRAPHIC&center=126.657566,37.466480&crs=epsg:4326&zoom=16&size={width},{height}";
        #endif*/
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
