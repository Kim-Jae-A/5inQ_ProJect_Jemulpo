using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

// JsonUtility를 통해 JSON을 Object로 변환
public class DBManager : MonoBehaviour
{
    public string url = "https://docs.google.com/spreadsheets/d/10GaozCAYllIZpwaNcOVs_IMyFdZO7P77DjIqR2mWiBY/edit#gid=0";
    class POIDataList
    {
        public List<POIData> poi = new List<POIData>();
    }

    POIDataList data = new POIDataList();

    // Start is called before the first frame update
    void Start()
    {
        ReadJSON();

        StartCoroutine(RequestCoroutine());
    }

    IEnumerator RequestCoroutine()
    {
        UnityWebRequest data = UnityWebRequest.Get(url);

        yield return data.SendWebRequest(); // 서버에 데이터 요청 보내기

        switch(data.result)
        {
            case UnityWebRequest.Result.Success:
                break;
            case UnityWebRequest.Result.ConnectionError:
                yield break;
            case UnityWebRequest.Result.ProtocolError:
                yield break;
            case UnityWebRequest.Result.DataProcessingError:
                yield break;
        }

        if (data.isDone)
        {
            string json = data.downloadHandler.text;

            string[] rows = json.Split('\n');
            for (int i = 0; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split('\t');

                foreach (var column in columns)
                {
                    // Debug.Log("line: " + i + ": " + column); // 구글 스프레드 시트 사용 2 ~ 10번째
                }
            }
        }
    }

    private void ReadJSON()
    {
        // Resources 폴더를 사용하지 않을 시
        //string path = Application.dataPath + "/04. Resources/ROIInfo.json";
        //string json = File.ReadAllText(path);
        //Debug.Log(json);

        // Resources 폴더를 사용 시
        TextAsset textAsset = Resources.Load<TextAsset>("POIInfo");
        string json = textAsset.text;

        data = JsonUtility.FromJson<POIDataList>(json);
        foreach (POIData po in data.poi)
        {
            Debug.Log(po.name);
        }
    }
}
