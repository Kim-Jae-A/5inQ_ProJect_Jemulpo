using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_Scene : MonoBehaviour
{
    public void NeviScene()
    {
        SceneManager.LoadScene("Map_Scene");
    }
    public void HomeScene()
    {
        SceneManager.LoadScene("Ys_Home");

    }
}