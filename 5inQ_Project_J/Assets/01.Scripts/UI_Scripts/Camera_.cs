using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Data;
using UnityEngine.UI;

public class Camera_ : MonoBehaviour
{
    public RawImage cameraView;
    public void TakePicture()
    {
        StartCoroutine(OnShotBtn());
    }
    IEnumerator OnShotBtn()
    {
        yield return new WaitForEndOfFrame();
        Camera camera = Camera.main;
        int width = cameraView.texture.width;
        int height = cameraView.texture.height;
        RenderTexture renderTexture = new RenderTexture(width, height, 24);
        camera.targetTexture = renderTexture;

        var currRenderTexture = RenderTexture.active;
        RenderTexture.active = renderTexture;

        camera.Render();

        Texture2D image = new Texture2D(width, height);
        image.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        image.Apply();

        camera.targetTexture = null;

        RenderTexture.active = currRenderTexture;

        byte[] bytes = image.EncodeToPNG();
        string fileNamge = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string filePath = Path.Combine(Application.persistentDataPath, fileNamge);
        File.WriteAllBytes(filePath, bytes );

    }
}
