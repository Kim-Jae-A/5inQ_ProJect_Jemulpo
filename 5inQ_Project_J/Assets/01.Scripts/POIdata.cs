using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct POIdata
{
    private string name;
    private string description;
    private float latitude;
    private float longitude;


    public POIdata(string name, string description, float latitude, float longitude)
    {
        this.name = name;
        this.description = description;
        this.latitude = latitude;
        this.longitude = longitude;
    }
}
