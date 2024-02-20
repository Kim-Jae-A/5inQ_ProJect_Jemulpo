using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Android;

public class StaticMapManager : MonoBehaviour
{
    [Header("API ����")]
    public string url = "https://naveropenapi.apigw.ntruss.com/map-static/v2/raster";
    public float width;
    public float height;
    public int zoomLevel;
    public string key_ID;
    public string key;
    public RawImage map;
    string apiURL;
    bool check;

    public static float latitude;  // ����
    public static float longitude; // �浵

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
                    StartCoroutine(StaticMapDrawing());
                }
                check = true;
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

    IEnumerator StaticMapDrawing()
    {
        apiURL = url + $"?w={width}&h={height}&center={longitude},{latitude}&level={zoomLevel}&scale=2"; // ���� ��ġ ��ǥ
#if UNITY_EDITOR
        apiURL = url + $"?w={width}&h={height}&center=126.657566,37.466480&level={zoomLevel}&scale=2"; //��������
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
