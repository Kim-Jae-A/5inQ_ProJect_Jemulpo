using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_Scene : MonoBehaviour
{
    public GameObject neviPanel;
    public void NeviScene()
    {
        SceneManager.LoadScene("Map_Scene");
    }
    public void HomeScene()
    {
        if (neviPanel.activeSelf)
        {
            neviPanel.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene("Ys_Home");
        }
    }
}