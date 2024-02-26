using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GPSmanager))]
public class DistanceCalculator : MonoBehaviour
{
    public Text distanceText;
    public GPSmanager gpsExample;
    public InputField latInputField;
    public InputField lonInputField;

    // 위도와 경도를 사용하여 두 지점 사이의 거리 계산 (단위: m)
    static double Distance(double lat1, double lon1, double lat2, double lon2)
    {
        double deg2radMultiplier = Math.PI / 180;
        lat1 = lat1 * deg2radMultiplier;
        lon1 = lon1 * deg2radMultiplier;
        lat2 = lat2 * deg2radMultiplier;
        lon2 = lon2 * deg2radMultiplier;

        double radius = 6378.137; // earth mean radius defined by WGS84
        double dlon = lon2 - lon1;
        double distance = Math.Acos(Math.Sin(lat1) * Math.Sin(lat2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(dlon)) * radius;

        return distance;
    }

    private void Awake()
    {
        gpsExample = GetComponent<GPSmanager>();
        latInputField.onValueChanged.AddListener(delegate { UpdateDistance(); });
        lonInputField.onValueChanged.AddListener(delegate { UpdateDistance(); });
    }

    void Start()
    {
        // 시작 시 거리 업데이트
        UpdateDistance();
    }

    void UpdateDistance()
    {
        // 현재 위치의 위도와 경도
        float lat1 = gpsExample.latitude;
        float lon1 = gpsExample.longitude;

        // 입력 필드에 입력된 위도와 경도
        float lat2 = 0f;
        float lon2 = 0f;
        float.TryParse(latInputField.text, out lat2);
        float.TryParse(lonInputField.text, out lon2);

        // 거리 계산
        var distance = Distance(lat1, lon1, lat2, lon2);

        // 거리를 변환하여 텍스트로 표시
        string distanceString = "";
        if (distance < 0.1f)
        {
            distanceString = $"{distance * 1000:F0}m";
        }
        else
        {
            distanceString = $"{distance:F0}km";
        }

        distanceText.text = distanceString;
    }
}
