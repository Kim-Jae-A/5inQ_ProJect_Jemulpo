using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonLoader : MonoBehaviour
{
    private JsonManager jsonManager;
    public MinMapLineRender drawingLine;

    private void Awake()
    {
        jsonManager = GetComponent<JsonManager>();      
    }
    // Start is called before the first frame update
    void Start()
    {
        jsonManager.LoadData();
        StaticMapManager.instance.StartDrawing();
        drawingLine.OnButtonEnter();
    }
}
