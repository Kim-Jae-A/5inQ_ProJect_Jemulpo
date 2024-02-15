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
/*        // 캡처할 스크린을 생성합니다.
        RenderTexture renderTexture = new RenderTexture(1080, 2000, 24);
        Texture2D screenshot = new Texture2D(1080, 2000, TextureFormat.RGB24, false);

        // 스크린을 렌더링합니다.
        Camera.main.targetTexture = renderTexture;
        Camera.main.Render();
        RenderTexture.active = renderTexture;

        // 스크린샷을 Texture2D로 변환합니다.
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        // Resources 폴더에 이미지 저장

        SaveImageToResources(screenshot, "ImageName");

        // 메모리 해제
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

    /*    private void SaveImageToResources(Texture2D image, string resourceName)
        {

            // 이미지를 파일로 저장
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






