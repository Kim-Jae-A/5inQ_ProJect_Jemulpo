using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class TakeAShot : MonoBehaviour
{
    public string path = "Assets/Resources";

    public void OnShotBtn()
    {
        StartCoroutine(ScreenShot());
    }

    IEnumerator ScreenShot()
    {
        yield return new WaitForEndOfFrame();

        // 캡처된 화면을 Texture2D로 생성합니다.
        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();

        // 캡처된 화면을 PNG 형식의 byte 배열로 변환합니다.
        byte[] bytes = tex.EncodeToPNG();
        Destroy(tex);

        // byte 배열을 PNG 파일로 저장합니다.
        string fileName = "ImageName";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllBytes(filePath, bytes);

        // "SavePhoto" 씬으로 이동합니다.
        SceneManager.LoadScene("SavePhoto");
    }

}






