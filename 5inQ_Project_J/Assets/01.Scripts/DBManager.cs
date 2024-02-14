using System.Collections.Generic;
using UnityEngine;


public class DBManager : MonoBehaviour
{
    // POI 데이터를 저장할 리스트

    class POIDataList
    {
        public List<POIdata> poi = new List<POIdata>();
    }
    POIDataList data = new POIDataList();
    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {

    }
}