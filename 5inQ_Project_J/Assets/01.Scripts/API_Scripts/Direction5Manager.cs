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

    [Header("API 설정")]
    public string url = "https://naveropenapi.apigw.ntruss.com/map-direction/v1/driving"; // API 요청 URL
    public string key_ID; // API 아이디
    public string key; // API 키
    string apiURL;
    
    public static string _endlongitude; // 경도
    public static string _endlatitude; // 위도
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

    public void OnNaviStartButtonEnter()
    {
        StartCoroutine(DirectionStart());
    }

    IEnumerator DirectionStart()
    {
        apiURL = url + $"?start={StaticMapManager.longitude},{StaticMapManager.latitude}&goal={_endlongitude},{_endlatitude}&option=trafast"; // 현재 위치 좌표
#if UNITY_EDITOR
        apiURL = url + $"?start=126.743572,37.713675&goal={_endlongitude},{_endlatitude}&option=trafast"; //제물포역
        //apiURL = url + $"?start=126.747583,37.714058&&goal={126.745253},{37.710519}&option=trafast"; //제물포역
#endif
        UnityWebRequest request = UnityWebRequest.Get(apiURL);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", key_ID);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", key);

        yield return request.SendWebRequest(); // 요청결과값

        // 예외처리
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
            //File.WriteAllText(Application.dataPath + "\\Resources\\Data.json", json);   // 요청 결과값 데이터
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
