using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveListElementData : MonoBehaviour
{
    private AR_POI arJsonData;
    
    public void SetARJsonData(AR_POI data)
    {
        arJsonData = data;
        
    }
    public AR_POI GetARJsonData()
    {
        return arJsonData;
    }
}
