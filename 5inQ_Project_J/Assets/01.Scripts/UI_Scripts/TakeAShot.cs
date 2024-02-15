using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class TakeAShot : MonoBehaviour
{
    public void OnShotBtn()
    {
        StartCoroutine(TakePhoto());
    }

    IEnumerator TakePhoto()
    {
        string timeStamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string fileName = timeStamp + ".png";
        string pathToSave = fileName;
        ScreenCapture.CaptureScreenshot(pathToSave);
        SceneManager.LoadScene("SavePhoto");
        yield return new WaitForEndOfFrame();
        
    }
}






