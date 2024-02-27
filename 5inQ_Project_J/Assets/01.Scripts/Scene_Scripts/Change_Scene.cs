using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    private void Update()
    {
        if (neviPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) //뒤로가기 버튼을 누르면
            {
                OffNeviPanel();
                return;
            }
        }
        if (infoPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) //뒤로가기 버튼을 누르면
            {
                OffInfoPanel();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape)) //뒤로가기 버튼을 누르면
            {
                SceneManager.LoadScene("Home");
            }
        }

    }

    public void HomeScene()
    {
        if (neviPanel.activeSelf)
        {
            OffNeviPanel();      
            return;
        }
        if (infoPanel.activeSelf)
        {
            OffInfoPanel();
        }
        else
        {
            SceneManager.LoadScene("Home");
        }
    }

    void OffNeviPanel()
    {
        neviPanel.SetActive(false);
        markerPanel.SetActive(true);
        foreach (GameObject obj in marker)
        {
            obj.SetActive(true);
        }
        endPoint.SetActive(false);
    }

    void OffInfoPanel()
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
    }
}