using Google.XR.ARCoreExtensions.GeospatialCreator.Internal;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Networking;

public class AnchorCreator : MonoBehaviour
{
    public Transform XROrigin;
    private GPSCoordinatesConverter converter;
    public GameObject AnchorPrefab;
    public List<float> Longitude = new List<float>();
    public List<float> Latitude = new List<float>();
    private void Awake()
    {
        gameObject.AddComponent<GPSCoordinatesConverter>();
        converter = GetComponent<GPSCoordinatesConverter>();
    }
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
        for(int i = 0;  i < Longitude.Count; i++)
        {
            // 위도와 경도를 유니티 좌표로 변환
            Vector3 unityCoordinates = converter.ConvertGPSToUnityCoordinates(Latitude[i], Longitude[i]);
            // 유니티 좌표를 화면 좌표로 변환
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(XROrigin.TransformPoint(unityCoordinates));

            // AR Geospatial Creator 게임 오브젝트 생성
            GameObject clone = Instantiate(AnchorPrefab, Vector3.zero, Quaternion.identity);
            // AR Geospatial Creator 게임 오브젝트의 자식으로 Anchor 스크립트를 추가
            //ARGeospatialCreatorAnchor anchorComponent = clone.AddComponent<ARGeospatialCreatorAnchor>();
            clone.transform.SetParent(transform);
            // Latitude와 Longitude 설정
            clone.transform.localPosition = screenPoint;


        }

    }
}
