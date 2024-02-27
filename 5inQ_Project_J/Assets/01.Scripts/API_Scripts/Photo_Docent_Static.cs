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

    double latitude;
    double longitude;

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
                latitude = Input.location.lastData.latitude;
                longitude = Input.location.lastData.longitude;
                StartCoroutine(StaticMapDrawing()); // API ��û �ڷ�ƾ
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

    public void DrawingStart()
    {
#if UNITY_ANDROID
        StartCoroutine(RequestLocationPermission());
#endif
#if UNITY_EDITOR
        StartCoroutine(StaticMapDrawing());
#endif
    }

    IEnumerator StaticMapDrawing()
    {
        apiURL = url + $"?w={width}&h={height}&center={longitude},{latitude}&level={zoomLevel}&scale=2"; // ���� ��ġ ��ǥ

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
