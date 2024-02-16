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

    public static float latitude;  // ����
    public static float longitude; // �浵

    bool checkGPS;
    void Start()
    {
        // ��ġ ���� ���� ��û
        StartCoroutine(RequestLocationPermission());
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
                if (!checkGPS)
                {
                    StartCoroutine(StaticMapDrawing());
                }
                checkGPS = true;
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
        string apiURL = url + $"?w={1000}&h={1000}&center={longitude},{latitude}&level=16&scale=2";
        //string apiURL = url + $"?w=1000&h=1000&markers=type:n|size:mid|pos:126.74257123848277%2037.713703705925|label:1&scale=2";

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
