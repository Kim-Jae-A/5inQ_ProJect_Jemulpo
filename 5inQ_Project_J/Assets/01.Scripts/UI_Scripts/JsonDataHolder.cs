using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonDataHolder : MonoBehaviour
{
    //POI json데이터를 받아오기위해 선언해줍니다.
    private AR_POI selectedARData;

    //싱글톤 패턴을 구현합니다. static을 선언하여 jsondataholder의 instance에 접근합니다.
    public static JsonDataHolder Instance {  get; private set; }

    //인스턴스의 생성 오류를 방지하기 위한 코드입니다.
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
    //선택된 데이터를 받아서 저장합니다.
    public void SetSelectedARData(AR_POI data)
    {
        selectedARData = data;
        Debug.Log("SetSelectedARData: " + (selectedARData != null ? selectedARData.Name : "null"));
    }
    //받아온 데이터를 반환합니다.
    public AR_POI GetSelectedARData()
    {
        Debug.Log("GetSelectedARData: " + (selectedARData != null ? selectedARData.Name : "null"));
        return selectedARData;
    }
}

