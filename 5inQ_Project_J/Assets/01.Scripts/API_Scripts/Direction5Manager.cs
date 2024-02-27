using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Direction5Manager : MonoBehaviour
{
    public static Direction5Manager instance;
    public static string jsonData;

    Map_DrawingLine drawingLine;
    JsonManager jsonManager;

    [Header("API ����")]
    public string url = "https://naveropenapi.apigw.ntruss.com/map-direction/v1/driving"; // API ��û URL
    public string key_ID; // API ���̵�
    public string key; // API Ű
    string apiURL;
    
    public static string _endlongitude; // �浵
    public static string _endlatitude; // ����
    public static string jsonData;

    [Header("UI Panel InFo")]
    public Image nevipanel;
    public Image infopanel;
    public Image markerpanel;
    public GameObject[] marker;
    public Text endText;

    void Awake()
    {
        drawingLine = JsonManager.instance.gameObject.GetComponent<Map_DrawingLine>();
        jsonManager = JsonManager.instance;

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
                //text.text = request.result.ToString();
                Debug.Log(request.result.ToString());
                break;
            case UnityWebRequest.Result.ConnectionError:
                Debug.LogWarning(request.result.ToString());
                //text.text = request.result.ToString();
                yield break;           
            case UnityWebRequest.Result.ProtocolError:
                //text.text = request.result.ToString();
                Debug.LogWarning(request.result.ToString());
                yield break;
            case UnityWebRequest.Result.DataProcessingError:
                //text.text = request.result.ToString();
                Debug.LogWarning(request.result.ToString());
                yield break;
        }

        if (request.isDone)
        {
            jsonData = request.downloadHandler.text;
            print(jsonData);
            jsonManager.LoadData();
            drawingLine.OnButtonEnter();
            //File.WriteAllText(Application.dataPath + "\\Resources\\Data.json", json);   // ��û ����� ������
            //File.WriteAllText(Path.Combine(Application.persistentDataPath, "data.json"), json);
            //StartCoroutine(LoadDataCoroutine());                  
        }
    }

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
    public void ExitPanel()
    {
        nevipanel.gameObject.SetActive(false);
    }
}
