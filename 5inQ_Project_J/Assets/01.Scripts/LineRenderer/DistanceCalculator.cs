using System;
using UnityEngine;
public class DistanceCalculator : MonoBehaviour
{
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

    public string DC(double lat2, double lon2)
    {
        // 두 지점 사이의 거리 계산
        double distance = Distance(StaticMapManager.latitude, StaticMapManager.longitude, lat2, lon2);
        // 거리를 변환하여 텍스트로 표시
        string distanceString = "";
        if (distance < 1f)
        {
            // 거리가 1km 미만인 경우, 미터 단위로 표시
            distanceString = $"{distance * 1000:F0}m";
        }
        else
        {
            // 거리가 0.1km 이상인 경우, 킬로미터 단위로 표시
            distanceString = $"{distance:F0}km";
        }

        return distanceString;
    }
}
