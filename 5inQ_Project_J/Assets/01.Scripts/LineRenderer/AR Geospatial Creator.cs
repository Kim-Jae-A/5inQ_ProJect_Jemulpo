using Google.XR.ARCoreExtensions;
using Google.XR.ARCoreExtensions.GeospatialCreator.Internal;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class ARGeospatialCreator : MonoBehaviour
{
    public GameObject AnchorPrefab;
    public List<double> Longitude = new List<double>();
    public List<double> Latitude = new List<double>();
    public double Originlatitude = 37.71432699999999;
    public double Originlongitude = 126.74199899999999;
    public double Originaltitude = 77.42;
    void Start()
    {
        JsonCall();
        CreateAnchor();

    }
    void JsonCall()
    {
        if (JsonManager.instance != null && JsonManager.instance.data != null &&
JsonManager.instance.data.route.trafast.Count > 0)
        {
            TraFast firstTraFast = JsonManager.instance.data.route.trafast[0];

            // Path 데이터 확인
            foreach (var point in firstTraFast.path)
            {
                Longitude.Add(point[0]);
                Latitude.Add(point[1]);
            }
        }
    }

    void CreateAnchor()
    {
        for (int i = 0; i < Longitude.Count; i++)
        {

            // AR Geospatial Creator 게임 오브젝트 생성
            GameObject clone = Instantiate(AnchorPrefab, Vector3.zero, Quaternion.identity);
            // AR Geospatial Creator 게임 오브젝트의 자식으로 Anchor 스크립트를 추가
            ARGeospatialCreatorAnchor anchorComponent = clone.AddComponent<ARGeospatialCreatorAnchor>();
            clone.transform.SetParent(transform);
            // Latitude와 Longitude 설정
            //anchorComponent.SetLatitude(Latitude[i]);
            //anchorComponent.SetLongitude(Longitude[i]);
            //anchorComponent.SetAltitude(68);
            Vector3 unityPosition = ConvertGeoToUnityCoordinates(Latitude[i], Longitude[i], 68);
            clone.transform.position = unityPosition;
        }

    }
    // 위도, 경도, 고도를 Unity 좌표로 변환하여 반환하는 메서드
    public Vector3 ConvertGeoToUnityCoordinates(double latitude, double longitude, double altitude)
    {
        // 위도와 경도를 월드 좌표로 변환
        float worldX = (float)((longitude - Originlongitude) * 111319.9); // 경도 변환
        float worldZ = (float)((latitude - Originlatitude) * 111319.9); // 위도 변환
        float worldY = (float)(altitude - Originaltitude); // 고도 변환

        return new Vector3(worldX, worldY, worldZ);
    }
}
