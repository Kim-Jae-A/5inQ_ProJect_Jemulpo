using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class Directions5Manager : MonoBehaviour
{
    [SerializeField] string url = "https://naveropenapi.apigw.ntruss.com/map-direction/v1/driving";
    [SerializeField] string clientID = "";
    [SerializeField] string clientPW = "";
    [SerializeField] string myPoint = "";
    [SerializeField] string destination = "";
    [Serializable] enum OptionCode
    {
        trafast,
        tracomfort,
        traoptimal,
        traavoidtoll,
        traavoidcaronly
    }
    [SerializeField] OptionCode optionCode = OptionCode.trafast;
    IEnumerator Start()
    {
        string apiRequestURL = url + $"?start={myPoint}&goal={destination}&option={OptionCode.trafast}";
        UnityWebRequest request = UnityWebRequest.Get(apiRequestURL);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", clientID);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", clientPW);
        yield return request.SendWebRequest();

        switch (request.result)
        {
            case UnityWebRequest.Result.Success:
                break;
            case UnityWebRequest.Result.ConnectionError:
                yield break;
                break;
            case UnityWebRequest.Result.ProtocolError:
                yield break;
                break;
            case UnityWebRequest.Result.DataProcessingError:
                yield break;
                break;
        }

        if (request.isDone)
        {
            string json = request.downloadHandler.text;
        }




    }


}
