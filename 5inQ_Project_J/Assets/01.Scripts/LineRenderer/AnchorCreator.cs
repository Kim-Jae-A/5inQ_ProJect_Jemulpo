using Google.XR.ARCoreExtensions.GeospatialCreator.Internal;
using Google.XR.ARCoreExtensions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AnchorCreator : MonoBehaviour
{
    public GameObject AnchorPrefab;
    public List<float> Longitude = new List<float>();
    public List<float> Latitude = new List<float>();

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
            clone.gameObject.AddComponent<ARAnchor>();
            clone.transform.SetParent(transform);
            // Latitude와 Longitude 설정
            anchorComponent.Latitude = Latitude[i];
            anchorComponent.Longitude = Longitude[i];
            anchorComponent.Altitude = 68f;



        }

    }
}
