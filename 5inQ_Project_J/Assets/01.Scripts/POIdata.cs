using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct POIdata
{
    private string name;
    private string description;
    private float latitude;
    private float longitude;
    private float altitude;
    public POIdata(string name, string description, float latitude, float longitude, float altitude)
    {
        this.name = name;
        this.description = description;
        this.latitude = latitude;
        this.longitude = longitude;
        this.altitude = altitude;
    }
}
