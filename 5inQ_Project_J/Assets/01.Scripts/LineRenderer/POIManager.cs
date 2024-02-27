using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

[System.Serializable]
public class ARZone
{
    public int idx;
    public string Name;
    public string Info;
    public string shooting;
    public string imagepath;
    public string Address;
    public float latitude;
    public float longitude;
}

[System.Serializable]
public class ARZoneList
{
    public List<ARZone> ARzone_List;
}

public class POIManager : MonoBehaviour
{
    public static POIManager instance;
    public ARZone data;
    public List<TextAsset> jsonFile;    
    public ARZoneList arZoneList;   
}
