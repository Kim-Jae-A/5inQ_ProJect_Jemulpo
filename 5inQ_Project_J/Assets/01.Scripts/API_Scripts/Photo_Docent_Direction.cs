using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


/// <summary>
/// Direction5Manager와 비슷하게 API 통신을 통해 받아오나 해당씬에서 안쓰는 기능들을 배제한 스크립트
/// </summary>
public class Photo_Docent_Direction : MonoBehaviour
{

    public static Photo_Docent_Direction instance;
    Map_DrawingLine drawingLine;

    public static string jsonData;
    double _endlongitude;
    double _endlatitude;

    [Header("API 설정")]
    public string url = "https://naveropenapi.apigw.ntruss.com/map-direction/v1/driving"; // API 요청 URL
    public string key_ID; // API 아이디
    public string key; // API 키
    string apiURL;

    private void Awake()
    {
        drawingLine = GetComponent<Map_DrawingLine>();

        // 인스턴스가 null일 경우에만 현재 인스턴스를 할당
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

    /// <summary>
    /// 받아온 위도 경도 초기화 후 안내 시작
    /// </summary>
    /// <param name="lo">경도</param>
    /// <param name="la">위도</param>
    public void Direction_Start(double lo, double la)
    {
        _endlatitude = la;
        _endlongitude = lo;

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
            JsonManager.instance.LoadData(jsonData);
            drawingLine.OnButtonEnter();
        }
    }
}
