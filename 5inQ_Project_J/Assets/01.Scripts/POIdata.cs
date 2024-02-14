using System;
[System.Serializable]
public struct POIdata
{
    public string name;
    public string description;
    public float latitude;
    public float longtitude;

    public POIdata(string name, string description, float latitude, float longtitude)
    {
        this.name = name;
        this.description = description;
        this.latitude = latitude;
        this.longtitude = longtitude;
    }
}
