using UnityEngine;

public class GPSCoordinatesConverter : MonoBehaviour
{
    // ������ ������ (����: ų�ι���)
    private const float EarthRadius = 6371f;

    // ������ ������ �浵
    private const float OriginLatitude = 0f;
    private const float OriginLongitude = 0f;

    // ������ �浵�� ����Ƽ ��ǥ�� ��ȯ�ϴ� �Լ�
    public Vector3 ConvertGPSToUnityCoordinates(float latitude, float longitude)
    {
        // ������ �浵�� ���� ������ ��ȯ
        float latitudeRad = latitude * Mathf.Deg2Rad;
        float longitudeRad = longitude * Mathf.Deg2Rad;

        // ������ ��� ���� ���� �Ÿ� ���
        float distanceX = (longitudeRad - OriginLongitude) * EarthRadius * Mathf.Cos(latitudeRad);
        float distanceZ = (latitudeRad - OriginLatitude) * EarthRadius;

        // ����Ƽ ��ǥ�� ��ȯ�Ͽ� ��ȯ
        return new Vector3(distanceX, 0f, distanceZ);
    }
}
