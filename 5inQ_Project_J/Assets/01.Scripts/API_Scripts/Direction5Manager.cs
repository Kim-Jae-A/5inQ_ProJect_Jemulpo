using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Naver API Direction 사용을 위한 Class
/// </summary>
public class Direction5Manager : MonoBehaviour
{
    public static Direction5Manager instance;

    Map_DrawingLine drawingLine;

    [Header("API 설정")]
    public string url = "https://naveropenapi.apigw.ntruss.com/map-direction/v1/driving"; // API 요청 URL
    public string key_ID; // API 아이디
    public string key; // API 키
    string apiURL;
    
    public static string _endlongitude; // 경도
    public static string _endlatitude; // 위도
    public static string jsonData; // API 에서 받아온 JSON 데이터가 저장되는 변수

    [Header("UI Panel InFo")]
    public Image nevipanel;  // 현재 위치와 도착위치 경로 미리보기 팝업창
    public Image neviStartPanel; // AR 화면으로 넘어갈때 확인 팝업창
    public Image infopanel;  // POT 데이터 마커 클릭시 나오는 상세 설명 팝업창
    public Image markerpanel;  // POI 데이터 마커 패널
    public GameObject[] marker;  // POI 데이터를 받아와 생성할때 쓰는 마커 프리펩
    

    void Awake()
    {
        drawingLine = GetComponent<Map_DrawingLine>();

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

    /// <summary>
    /// 유니티 통신 라이브러리를 통해 API 통신 및 결과값을 받는 코루틴
    /// </summary>
    /// <returns></returns>
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
            string test = "{\"code\":1,\"message\":\"출발지와 도착지가 동일합니다. 확인 후 다시 지정해주세요.\"}";
            if (jsonData == test)
            {
                Debug.LogError("출발지와 목적지가 같습니다");
            }
            else
            {
                JsonManager.instance.LoadData();
                drawingLine.OnButtonEnter();
            }                       
        }
    }

    /// <summary>
    /// 길안내 버튼을 누르면 패널 및 마커 가리기
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
