using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PathOverlay : MonoBehaviour
{
    // POI �����͸� ������ ����Ʈ

    class sectionDataList
    {
        public List<sectionData> section = new List<sectionData>();
    }
    sectionDataList data = new sectionDataList();
    // Start is called before the first frame update
    void Start()
    {
        // Resources �������� JSON ���� �ε�
        TextAsset json = Resources.Load<TextAsset>("Test");
        Debug.Log(json);
        // JSON ������ POIDataList ��ü�� ��ȯ
        data = JsonUtility.FromJson<sectionDataList>(json.text);
        Debug.Log(data.section.Count);
        // ��ȯ�� ������ Ȯ��
        foreach (sectionData section in data.section)
        {
            Debug.Log("pointIndex: " + section.pointIndex);
            Debug.Log("pointCount: " + section.pointCount);
            Debug.Log("distance: " + section.distance);
            Debug.Log("name: " + section.name);
            Debug.Log("congestion: " + section.congestion);
            Debug.Log("speed: " + section.speed);
        }
    }

    // Update is called once per frame

}