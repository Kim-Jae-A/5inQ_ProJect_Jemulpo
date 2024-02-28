using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonDataHolder : MonoBehaviour
{
    //POI json�����͸� �޾ƿ������� �������ݴϴ�.
    private AR_POI selectedARData;

    //�̱��� ������ �����մϴ�. static�� �����Ͽ� jsondataholder�� instance�� �����մϴ�.
    public static JsonDataHolder Instance {  get; private set; }

    //�ν��Ͻ��� ���� ������ �����ϱ� ���� �ڵ��Դϴ�.
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("JsonDataHolder object created and set to DontDestroyOnLoad");
        }
        else
        {
            Debug.Log("JsonDataHolder object is already created");
        }
    }
    //���õ� �����͸� �޾Ƽ� �����մϴ�.
    public void SetSelectedARData(AR_POI data)
    {
        selectedARData = data;
        Debug.Log("SetSelectedARData: " + (selectedARData != null ? selectedARData.Name : "null"));
    }
    //�޾ƿ� �����͸� ��ȯ�մϴ�.
    public AR_POI GetSelectedARData()
    {
        Debug.Log("GetSelectedARData: " + (selectedARData != null ? selectedARData.Name : "null"));
        return selectedARData;
    }
}

