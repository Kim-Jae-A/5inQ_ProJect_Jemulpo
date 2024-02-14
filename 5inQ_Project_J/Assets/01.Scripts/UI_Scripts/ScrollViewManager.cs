using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class AR_POI
{
    public int idx;
    public string Landmark;
    public string LandmarkInfo;
    public string shooting;
}
[System.Serializable]
public class AR_DataList
{
    public List<AR_POI> ARzone_List;
}
public class ScrollViewManager : MonoBehaviour
{
    //��ũ�Ѻ��� ��ҷ� ������ �����հ� �������� ������ content.
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject List_element;
    
    //�������� ����Ʈ POI
    private TextAsset jsonfile_Photozone;
    private TextAsset jsonfile_Docent;

    void Start()
    {
        //json������ �ε�
        jsonfile_Photozone = Resources.Load<TextAsset>("Json/POIData");
        jsonfile_Docent = Resources.Load<TextAsset>("Json/POIData_Docent");

        //�ʱ⿡�� �������� ���õǾ� �־�� �ϹǷ� ������ json������ �ε��ϰ� ��ũ�Ѻ��� ��ҷ� ����.
        LoadAndShowData(jsonfile_Photozone);
    }

    // 2���� �Լ��� ��ũ�Ѻ� �� �������� ����Ʈ ��ư�� Ŭ������ �� �����ϴ� �Լ��̴�.
    // �� �Լ��� json������ ����ǰ� �����͸� �ٲ㼭 �������� �����Ѵ�.
    public void OnPhotozoneButtonClicked()
    {
        LoadAndShowData(jsonfile_Photozone);
    }
    public void OnDocentButtonClicked()
    {
        LoadAndShowData(jsonfile_Docent);
    }

    //json������ �����ϸ� json������ ����ȭ�Ѵ�.
    private void LoadAndShowData(TextAsset jsonfile)
    {
        if(jsonfile != null)
        {
            AR_DataList ar_dataList = JsonUtility.FromJson<AR_DataList>(jsonfile.text);

            //content ������ ������Ʈ, �� ������ ��ҵ��� ��� �����Ͽ�, ��ư�� ������ �� ���ο� json������ 
            //�����ͷ� ������Ʈ �ϱ� ���� ���� ������ �����ִ� �κ�!
            foreach(Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }
            //������ ��ũ�Ѻ��� ��ҵ��� �����ָ� ���⼭�� json������ �����͸� �޾Ƽ� content�� ��Ҹ� �����Ѵ�.
            foreach(AR_POI ARZone_ in ar_dataList.ARzone_List)
            {
                CreateScrollviewContent(ARZone_);
            }
        }
    }
   
    //AR_POI�� ��ü�� ������, �� json�� �����͸� �̿��ؼ� element�� �����ϰ� �� element�� Text�� AR_POI�� �����͸�
    //�����Ͽ� ������ ����!
    private void CreateScrollviewContent(AR_POI ARZone_poi)
    {
        GameObject element = Instantiate(List_element, content.transform);

        Text title = element.transform.Find("Title and descripttion/Title").GetComponent<Text>();
        Text info = element.transform.Find("Title and descripttion/Description").GetComponent <Text>();
        if(title != null)
        {
            title.text = ARZone_poi.Landmark;
        }

        if (info != null)
        {
            info.text = ARZone_poi.LandmarkInfo;
        }
    }

}
