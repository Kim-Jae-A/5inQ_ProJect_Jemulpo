using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PathOverlay : MonoBehaviour
{
    // POI 데이터를 저장할 리스트

    class sectionDataList
    {
        public List<sectionData> section = new List<sectionData>();
    }
    sectionDataList data = new sectionDataList();
    // Start is called before the first frame update
    void Start()
    {
        // Resources 폴더에서 JSON 파일 로드
        TextAsset json = Resources.Load<TextAsset>("Test");
        Debug.Log(json);
        // JSON 파일을 POIDataList 객체로 변환
        data = JsonUtility.FromJson<sectionDataList>(json.text);
        Debug.Log(data.section.Count);
        // 변환된 데이터 확인
        foreach (sectionData section in data.section)
        {
            Debug.Log("pointIndex: " + section.pointIndex);
            Debug.Log("pointCount: " + section.pointCount);
            Debug.Log("distance: " + section.distance);
            Debug.Log("name: " + section.name);
            Debug.Log("congestion: " + section.congestion);
            Debug.Log("speed: " + section.speed);
        }
    }

    // Update is called once per frame

}