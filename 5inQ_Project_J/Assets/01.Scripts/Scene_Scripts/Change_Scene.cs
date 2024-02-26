using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_Scene : MonoBehaviour
{
    public GameObject neviPanel;
    public GameObject infoPanel;
    public GameObject markerPanel;
    public GameObject[] marker;
    public GameObject endPoint;
    public GameObject lineObj;

    public void NeviScene()
    {
        SceneManager.LoadScene("Map_Scene");
    }
    public void HomeScene()
    {
        if (neviPanel.activeSelf)
        {
            neviPanel.SetActive(false);
            markerPanel.SetActive(true);
            foreach (GameObject obj in marker)
            {
                obj.SetActive(true);
            }
            endPoint.SetActive(false);        
            return;
        }
        if (infoPanel.activeSelf)
        {
            neviPanel.SetActive(true);
            infoPanel.SetActive(false);
            if (lineObj.transform.childCount > 0)
            {
                for (int i = 0; i < lineObj.transform.childCount; i++)
                {
                    Destroy(lineObj.transform.GetChild(i).gameObject);
                }
                lineObj.GetComponent<LineRenderer>().positionCount = 0;
            }
            return;
        }
        else
        {
            SceneManager.LoadScene("Home");
        }
    }
}