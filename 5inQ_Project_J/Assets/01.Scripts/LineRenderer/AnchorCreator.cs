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

            // Path ������ Ȯ��
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
            // ������ �浵�� ����Ƽ ��ǥ�� ��ȯ
            Vector3 unityCoordinates = converter.ConvertGPSToUnityCoordinates(Latitude[i], Longitude[i]);
            // ����Ƽ ��ǥ�� ȭ�� ��ǥ�� ��ȯ
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(XROrigin.TransformPoint(unityCoordinates));

            // AR Geospatial Creator ���� ������Ʈ ����
            GameObject clone = Instantiate(AnchorPrefab, Vector3.zero, Quaternion.identity);
            // AR Geospatial Creator ���� ������Ʈ�� �ڽ����� Anchor ��ũ��Ʈ�� �߰�
            //ARGeospatialCreatorAnchor anchorComponent = clone.AddComponent<ARGeospatialCreatorAnchor>();
            clone.transform.SetParent(transform);
            // Latitude�� Longitude ����
            clone.transform.localPosition = screenPoint;


        }

    }
}
