using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Direction5Manager : MonoBehaviour
{
    [Header("API ����")]
    public string url = "https://naveropenapi.apigw.ntruss.com/map-direction/v1/driving"; // API ��û URL
    public string key_ID; // API ���̵�
    public string key; // API Ű
    string apiURL;

    public static string _endlongitude; // �浵
    public static string _endlatitude; // ����

    public Image panel;

    public void OnNaviStartButtonEnter()
    {
        StartCoroutine(DirectionStart());
    }

    IEnumerator DirectionStart()
    {
        apiURL = url + $"?start={StaticMapManager.longitude},{StaticMapManager.latitude}&goal={_endlongitude},{_endlatitude}&option=trafast"; // ���� ��ġ ��ǥ
#if UNITY_EDITOR
        apiURL = url + $"?start=126.747583,37.714058&&goal={_endlongitude},{_endlatitude}&option=trafast"; //��������
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
            panel.gameObject.SetActive(true);
            string json = request.downloadHandler.text;
            System.IO.File.WriteAllText(Application.dataPath + "\\Resources\\Data.json", json);   // ��û ����� ������
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
