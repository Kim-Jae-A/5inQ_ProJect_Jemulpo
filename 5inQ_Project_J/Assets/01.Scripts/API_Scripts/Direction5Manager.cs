using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Direction5Manager : MonoBehaviour
{
    [Header("API 설정")]
    public string url = "https://naveropenapi.apigw.ntruss.com/map-direction/v1/driving"; // API 요청 URL
    public string key_ID; // API 아이디
    public string key; // API 키
    string apiURL;

    public static string _endlongitude; // 경도
    public static string _endlatitude; // 위도

    public Image panel;

    public void OnNaviStartButtonEnter()
    {
        StartCoroutine(DirectionStart());
    }

    IEnumerator DirectionStart()
    {
        apiURL = url + $"?start={StaticMapManager.longitude},{StaticMapManager.latitude}&goal={_endlongitude},{_endlatitude}&option=trafast"; // 현재 위치 좌표
#if UNITY_EDITOR
        apiURL = url + $"?start=126.747583,37.714058&&goal={_endlongitude},{_endlatitude}&option=trafast"; //제물포역
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
            panel.gameObject.SetActive(true);
            string json = request.downloadHandler.text;
            System.IO.File.WriteAllText(Application.dataPath + "\\Resources\\Data.json", json);   // 요청 결과값 데이터
            yield break;
        }
    }
    public void NextScene()
    {
        SceneManager.LoadScene("AR_LIneRenderer");
    }
    public void ExitPanel()
    {
        panel.gameObject.SetActive(false);
    }
}
