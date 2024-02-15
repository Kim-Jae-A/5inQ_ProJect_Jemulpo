using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTransfer : MonoBehaviour
{
    public static DataTransfer Instance;

    public string SelectedElementName;
    public string SelectedElementDescription;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSelectedElementData(string name, string description)
    {
        SelectedElementName = name;
        SelectedElementDescription = description;
    }
}
