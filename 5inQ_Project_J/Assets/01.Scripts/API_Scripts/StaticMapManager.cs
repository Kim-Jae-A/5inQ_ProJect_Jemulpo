using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StaticMapManager : MonoBehaviour
{
    string url = "https://naveropenapi.apigw.ntruss.com/map-static/v2/raster";
    string key_ID = "yxpbvkcpme";
    string key = "0yutJDRB0oHCq9pOLk9u060XEpQlEMZL7sRShb9t";
    public RawImage map;
    int width = 10000;
    int height = 10000;
    

    IEnumerator Start()
    {
        //map.rectTransform.sizeDelta = new Vector2(width, height);

        //string apiURL = url + $"?w={1000}&h={1000}&center=126.74257123848277,37.71370370592501&level=16&scale=2";
        string apiURL = url + $"?w=1000&h=1000&markers=type:n|size:mid|pos:126.74257123848277%2037.713703705925|label:1&";

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

    private void Update()
    {
        
    }
}
