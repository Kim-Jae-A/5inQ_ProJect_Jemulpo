using UnityEngine;

public class GPSCoordinatesConverter : MonoBehaviour
{
    // 지구의 반지름 (단위: 킬로미터)
    private const float EarthRadius = 6371f;

    // 원점의 위도와 경도
    private const float OriginLatitude = 0f;
    private const float OriginLongitude = 0f;

    // 위도와 경도를 유니티 좌표로 변환하는 함수
    public Vector3 ConvertGPSToUnityCoordinates(float latitude, float longitude)
    {
        // 위도와 경도를 라디안 단위로 변환
        float latitudeRad = latitude * Mathf.Deg2Rad;
        float longitudeRad = longitude * Mathf.Deg2Rad;

        // 원점과 대상 지점 간의 거리 계산
        float distanceX = (longitudeRad - OriginLongitude) * EarthRadius * Mathf.Cos(latitudeRad);
        float distanceZ = (latitudeRad - OriginLatitude) * EarthRadius;

        // 유니티 좌표로 변환하여 반환
        return new Vector3(distanceX, 0f, distanceZ);
    }
}
