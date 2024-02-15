using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonDataHolder : MonoBehaviour
{
    private AR_POI selectedARData;

    public static JsonDataHolder Instance {  get; private set; }

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
    public void SetSelectedARData(AR_POI data)
    {
        selectedARData = data;
        Debug.Log("SetSelectedARData: " + (selectedARData != null ? selectedARData.Landmark : "null"));
    }
    public AR_POI GetSelectedARData()
    {
        Debug.Log("GetSelectedARData: " + (selectedARData != null ? selectedARData.Landmark : "null"));
        return selectedARData;
    }
}

