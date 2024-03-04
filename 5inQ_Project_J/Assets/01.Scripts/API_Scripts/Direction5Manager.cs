using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Naver API Direction ����� ���� Class
/// </summary>
public class Direction5Manager : MonoBehaviour
{
    public static Direction5Manager instance;

    Map_DrawingLine drawingLine;

    [Header("API ����")]
    public string url = "https://naveropenapi.apigw.ntruss.com/map-direction/v1/driving"; // API ��û URL
    public string key_ID; // API ���̵�
    public string key; // API Ű
    string apiURL;
    
    public static string _endlongitude; // �浵
    public static string _endlatitude; // ����
    public static string jsonData; // API ���� �޾ƿ� JSON �����Ͱ� ����Ǵ� ����

    [Header("UI Panel InFo")]
    public Image nevipanel;  // ���� ��ġ�� ������ġ ��� �̸����� �˾�â
    public Image neviStartPanel; // AR ȭ������ �Ѿ�� Ȯ�� �˾�â
    public Image infopanel;  // POT ������ ��Ŀ Ŭ���� ������ �� ���� �˾�â
    public Image markerpanel;  // POI ������ ��Ŀ �г�
    public GameObject[] marker;  // POI �����͸� �޾ƿ� �����Ҷ� ���� ��Ŀ ������
    

    void Awake()
    {
        drawingLine = GetComponent<Map_DrawingLine>();

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

    public void OnNaviStartButtonEnter() 
    {
        StartCoroutine(DirectionStart()); 
    }

    /// <summary>
    /// ����Ƽ ��� ���̺귯���� ���� API ��� �� ������� �޴� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
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
            print(jsonData);
            string test = "{\"code\":1,\"message\":\"������� �������� �����մϴ�. Ȯ�� �� �ٽ� �������ּ���.\"}";
            if (jsonData == test)
            {
                Debug.LogError("������� �������� �����ϴ�");
            }
            else
            {
                JsonManager.instance.LoadData();
                drawingLine.OnButtonEnter();
            }                       
        }
    }

    /// <summary>
    /// ��ȳ� ��ư�� ������ �г� �� ��Ŀ ������
    /// </summary>
    public void SomeFunction()
    {
        nevipanel.gameObject.SetActive(true);
        infopanel.gameObject.SetActive(false);
        markerpanel.gameObject.SetActive(false);
        foreach (GameObject obj in marker)
        {
            obj.SetActive(false);
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene("AR_LIneRenderer");
    }
    public void NeviStartButtonEnter()
    {
        neviStartPanel.gameObject.SetActive(true);
    }

    public void ExitPanel()
    {
        neviStartPanel.gameObject.SetActive(false);
    }
}
