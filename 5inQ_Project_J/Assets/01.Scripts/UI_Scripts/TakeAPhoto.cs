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
/*        // ĸó�� ��ũ���� �����մϴ�.
        RenderTexture renderTexture = new RenderTexture(1080, 2000, 24);
        Texture2D screenshot = new Texture2D(1080, 2000, TextureFormat.RGB24, false);

        // ��ũ���� �������մϴ�.
        Camera.main.targetTexture = renderTexture;
        Camera.main.Render();
        RenderTexture.active = renderTexture;

        // ��ũ������ Texture2D�� ��ȯ�մϴ�.
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        // Resources ������ �̹��� ����

        SaveImageToResources(screenshot, "ImageName");

        // �޸� ����
        RenderTexture.active = null;
        Camera.main.targetTexture = null;
        Destroy(renderTexture);
        Destroy(screenshot);

        SceneManager.LoadScene("SavePhoto");*/

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

    /*    private void SaveImageToResources(Texture2D image, string resourceName)
        {

            // �̹����� ���Ϸ� ����
            byte[] bytes = image.EncodeToPNG();
            File.WriteAllBytes(path, bytes);

        }*/

    /*IEnumerator ScreenShot()
    {
        yield return new WaitForEndOfFrame();
        byte[] imageByte;

        Texture2D tex = new Texture2D(1080, 2000, TextureFormat.RGB24, true);

        tex.ReadPixels(new Rect(0, 0, 1080, 2000), 0, 0, true);
        tex.Apply();

        imageByte = tex.EncodeToPNG();
        DestroyImmediate(tex);

        File.WriteAllBytes(path, imageByte);

        SceneManager.LoadScene("SavePhoto");
    }*/


}






