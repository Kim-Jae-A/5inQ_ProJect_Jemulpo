using Google.XR.ARCoreExtensions.GeospatialCreator.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ARGeospatialCreator : MonoBehaviour
{
    public GameObject AnchorPrefab;
    public List<double> Longitude = new List<double>();
    public List<double> Latitude = new List<double>();
    public List<double> Altitude = new List<double>();
    [System.Serializable]
    public class ElevationResponse
    {
        public ElevationResult[] results;
    }

    [System.Serializable]
    public class ElevationResult
    {
        public float elevation;
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
            // AR Geospatial Creator 게임 오브젝트 생성
            GameObject clone = Instantiate(AnchorPrefab, Vector3.zero, Quaternion.identity);
            // AR Geospatial Creator 게임 오브젝트의 자식으로 Anchor 스크립트를 추가
            ARGeospatialCreatorAnchor anchorComponent = clone.AddComponent<ARGeospatialCreatorAnchor>();

            // Latitude와 Longitude 설정
            anchorComponent.Latitude = Latitude[i];
            anchorComponent.Longitude = Longitude[i];
            StartCoroutine(FetchElevation(Latitude[i], Longitude[i]));
            clone.transform.SetParent(transform);
        }

    }
    IEnumerator FetchElevation(double lat, double lon)
    {
        string url = "https://maps.googleapis.com/maps/api/elevation/json?locations=" + lat + "," + lon + "&key=AIzaSyBXRRJbBhmvVduaSaeOw0_LVy76wg5bNTk";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;
                ElevationResponse response = JsonUtility.FromJson<ElevationResponse>(jsonResponse);
                if (response != null && response.results.Length > 0)
                {
                    float elevation = response.results[0].elevation;
                    Debug.Log("Elevation at (" + lat + ", " + lon + "): " + elevation + " meters");
                }
            }
            else
            {
                Debug.LogError("Failed to fetch elevation: " + webRequest.error);
            }
        }
    }
}
