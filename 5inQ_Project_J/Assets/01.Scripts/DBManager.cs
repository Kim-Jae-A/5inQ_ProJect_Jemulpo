using System.Collections.Generic;
using UnityEngine;


public class DBManager : MonoBehaviour
{
    // POI �����͸� ������ ����Ʈ

    class POIDataList
    {
        public List<POIdata> poi = new List<POIdata>();
    }
    POIDataList data = new POIDataList();
    // Start is called before the first frame update
    void Start()
    {
        // Resources �������� JSON ���� �ε�
        TextAsset json = Resources.Load<TextAsset>("POIInfo");
        Debug.Log(json);
        // JSON ������ POIDataList ��ü�� ��ȯ
        data = JsonUtility.FromJson<POIDataList>(json.text);
        Debug.Log(data.poi.Count);
        // ��ȯ�� ������ Ȯ��
        foreach (POIdata poi in data.poi)
        {
            Debug.Log("Name: " + poi.name);
            Debug.Log("Description: " + poi.description);
            Debug.Log("Latitude: " + poi.latitude);
            Debug.Log("Longtitude: " + poi.longtitude);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}