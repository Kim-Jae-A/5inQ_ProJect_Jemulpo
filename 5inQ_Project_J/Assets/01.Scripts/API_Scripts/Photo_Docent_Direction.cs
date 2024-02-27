using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Photo_Docent_Direction : MonoBehaviour
{
    public static Photo_Docent_Direction instance;
    Map_DrawingLine drawingLine;
    JsonManager jsonManager;

    public static string jsonData;
    double _endlongitude;
    double _endlatitude;

    [Header("API ����")]
    public string url = "https://naveropenapi.apigw.ntruss.com/map-direction/v1/driving"; // API ��û URL
    public string key_ID; // API ���̵�
    public string key; // API Ű
    string apiURL;

    private void Awake()
    {
        drawingLine = JsonManager.instance.gameObject.GetComponent<Map_DrawingLine>();
        jsonManager = JsonManager.instance;

        // �ν��Ͻ��� null�� ��쿡�� ���� �ν��Ͻ��� �Ҵ�
        if (instance == null)
        {
            instance = this;
            //LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DirectionStart(double lo, double la)
    {
        _endlatitude = la;
        _endlongitude = lo;

        StartCoroutine(DirectionStart());
    }

    IEnumerator DirectionStart()
    {
        apiURL = url + $"?start={StaticMapManager.longitude},{StaticMapManager.latitude}&goal={_endlongitude},{_endlatitude}&option=trafast"; // ���� ��ġ ��ǥ
#if UNITY_EDITOR
        apiURL = url + $"?start=126.743572,37.713675&goal={_endlongitude},{_endlatitude}&option=trafast"; //��������
        //apiURL = url + $"?start=126.747583,37.714058&&goal={126.745253},{37.710519}&option=trafast"; //��������
#endif
        UnityWebRequest request = UnityWebRequest.Get(apiURL);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", key_ID);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", key);

        yield return request.SendWebRequest(); // ��û�����

        // ����ó��
        switch (request.result)
        {
            case UnityWebRequest.Result.Success:
                Debug.Log(request.result.ToString());
                break;
            case UnityWebRequest.Result.ConnectionError:
                Debug.LogWarning(request.result.ToString());
                yield break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogWarning(request.result.ToString());
                yield break;
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogWarning(request.result.ToString());
                yield break;
        }

        if (request.isDone)
        {
            jsonData = request.downloadHandler.text;
            jsonManager.LoadData(jsonData);
            drawingLine.OnButtonEnter();
        }
    }
}