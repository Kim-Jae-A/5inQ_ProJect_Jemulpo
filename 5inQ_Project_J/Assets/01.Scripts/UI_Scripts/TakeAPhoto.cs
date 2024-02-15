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

        // ĸó�� ȭ���� Texture2D�� �����մϴ�.
        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();

        // ĸó�� ȭ���� PNG ������ byte �迭�� ��ȯ�մϴ�.
        byte[] bytes = tex.EncodeToPNG();
        Destroy(tex);

        // byte �迭�� PNG ���Ϸ� �����մϴ�.
        string fileName = "ImageName";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllBytes(filePath, bytes);

        // "SavePhoto" ������ �̵��մϴ�.
        SceneManager.LoadScene("SavePhoto");
    }

}






