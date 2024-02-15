using System.Collections.Generic;
using Newtonsoft.Json;
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
{
    void Start()
    {
        // Resources �������� JSON ���� �ε�
        TextAsset json = Resources.Load<TextAsset>("Test");

        // JSON ������ RouteData ��ü�� ��ȯ
        RouteData data = JsonConvert.DeserializeObject<RouteData>(json.text);

        // RouteData���� �ʿ��� ���� ���� �� ���
        Debug.Log("Code: " + data.code);
        Debug.Log("Message: " + data.message);
        Debug.Log("Current Date Time: " + data.currentDateTime);

        // ù ��° TraFast ���� ��������
        if (data.route.trafast.Count > 0)
        {
            TraFast firstTraFast = data.route.trafast[0];
            Debug.Log("Distance: " + firstTraFast.summary.distance);
            Debug.Log("Duration: " + firstTraFast.summary.duration);
            Debug.Log("Departure Time: " + firstTraFast.summary.departureTime);
            // path ������ Ȯ��
            foreach (var point in firstTraFast.path)
            {
                // ����Ʈ ���� �� ����Ʈ ���
                Debug.Log("Longitude: " + point[0] + ", Latitude: " + point[1]);
            }

            // Section ������ Ȯ��
            foreach (Section sectionInfo in firstTraFast.section)
            {
                Debug.Log("Point Index: " + sectionInfo.pointIndex);
                Debug.Log("Point Count: " + sectionInfo.pointCount);
                Debug.Log("Distance: " + sectionInfo.distance);
                Debug.Log("Name: " + sectionInfo.name);
                Debug.Log("Congestion: " + sectionInfo.congestion);
                Debug.Log("Speed: " + sectionInfo.speed);
            }

            // Guide ������ Ȯ��
            foreach (Guide guideInfo in firstTraFast.guide)
            {
                Debug.Log("Point Index: " + guideInfo.pointIndex);
                Debug.Log("Type: " + guideInfo.type);
                Debug.Log("Instructions: " + guideInfo.instructions);
                Debug.Log("Distance: " + guideInfo.distance);
                Debug.Log("Duration: " + guideInfo.duration);
            }
        }
    }
}
