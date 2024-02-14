using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/*
public class DBManager : MonoBehaviour
{
    // POI 데이터를 저장할 리스트
    public string url = "https://docs.google.com/spreadsheets/d/10GaozCAYllIZpwaNcOVs_IMyFdZO7P77DjIqR2mWiBY/edit#gid=0";
   


class POIDataList
    {
        public List<POIdata> poi = new List<POIdata>();
    }
    POIDataList data = new POIDataList();
    // Start is called before the first frame update
    void Start()
    {
        ReadJSON();
        StartCoroutine("RequestCoroutine");
    }

    IEnumerator RequestCoroutine()
    {
        UnityWebRequest data = UnityWebRequest.Get(url);
        yield return data.SendWebRequest();

        switch (data.result)
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
        if (data.isDone == true)
        {
            string json = data.downloadHandler.text;
            string[] rows = json.Split('\n');
            for (int i = 0; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split("\t");
                foreach (var column in columns)
                {
                    Debug.Log("Line:" + i + " " + column);
                }
            }
        }


        //DisplayText(json);
    }

    private void DisplayText(string json)
    {

    }

    // Update is called once per frame
    void ReadJSON()
    {
        // Resources 폴더에서 JSON 파일 로드
        TextAsset json = Resources.Load<TextAsset>("POIInfo");
        Debug.Log(json);
        // JSON 파일을 POIDataList 객체로 변환
        data = JsonUtility.FromJson<POIDataList>(json.text);
        Debug.Log(data.poi.Count);
        // 변환된 데이터 확인
        foreach (POIdata poi in data.poi)
        {
            Debug.Log("Name: " + poi.name);
            Debug.Log("Description: " + poi.description);
            Debug.Log("Latitude: " + poi.latitude);
            Debug.Log("Longtitude: " + poi.longtitude);
        }
    }
}
 */