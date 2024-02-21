using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RouteData
{
    public int code;
    public string message;
    public string currentDateTime;
    public Route route;
}

[System.Serializable]
public class Route
{
    public List<TraFast> trafast;
}

[System.Serializable]
public class TraFast
{
    public Summary summary;
    public List<List<float>> path;
    public List<Section> section;
    public List<Guide> guide;
}
[System.Serializable]
public class Summary
{
    public Start start;
    public Goal goal;
    public int distance;
    public int duration;
    public int etaServiceType;
    public string departureTime;
    public List<List<float>> bbox;
    public int tollFare;
    public int taxiFare;
    public int fuelPrice;
}

[System.Serializable]
public class Start
{
    public List<float> location;
}

[System.Serializable]
public class Goal
{
    public List<float> location;
    public int dir;
}
[System.Serializable]
public class Section
{
    public int pointIndex;
    public int pointCount;
    public int distance;
    public string name;
    public int congestion;
    public int speed;
}

[System.Serializable]
public class Guide
{
    public int pointIndex;
    public int type;
    public string instructions;
    public int distance;
    public int duration;
}

public class JsonManager : MonoBehaviour
{    // 정적 변수로 JsonManager 클래스의 인스턴스를 저장
    public static JsonManager instance;

    // RouteData 객체
    public RouteData data;

    void Awake()
    {
        // 인스턴스가 null일 경우에만 현재 인스턴스를 할당
        if (instance == null)
        {
            instance = this;
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        LoadData();

        // 첫 번째 TraFast 정보 가져오기
        if (data.route.trafast.Count > 0)
        {
            TraFast firstTraFast = data.route.trafast[0];
        }
    }
    // 데이터 로드 함수
    void LoadData()
    {
        // Resources 폴더에서 JSON 파일 로드
        TextAsset json = Resources.Load<TextAsset>("Data");

        // JSON 파일을 RouteData 객체로 변환
        data = JsonConvert.DeserializeObject<RouteData>(json.text);
    }

}
