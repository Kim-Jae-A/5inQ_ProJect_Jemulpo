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
    int width = 3000;
    int height = 3000;

    IEnumerator Start()
    {
        map.rectTransform.sizeDelta = new Vector2(width, height);

        string apiURL = url + $"?w={1024}&h={1024}&center=127.1054221,37.3591614&level=16&scale=2";

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(apiURL);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", key_ID);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", key);

        yield return request.SendWebRequest();
        switch (request.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                yield break;
            case UnityWebRequest.Result.Success:
                break;
            case UnityWebRequest.Result.ProtocolError:
                yield break;
            case UnityWebRequest.Result.DataProcessingError:
                yield break;
        }
        if (request.isDone)
        {
            map.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }

    private void Update()
    {
        
    }
}
