using System;
[System.Serializable]
public struct sectionData
{
    public int pointIndex;
    public int pointCount;
    public int distance;
    public string name;
    public int congestion;
    public int speed;


    public sectionData(int pointIndex, int pointCount, int distance, string name, int congestion, int speed)
    {
        this.pointIndex = pointIndex;
        this.pointCount = pointCount;
        this.distance = distance;
        this.name = name;
        this.congestion = congestion;
        this.speed = speed;
    }
}
