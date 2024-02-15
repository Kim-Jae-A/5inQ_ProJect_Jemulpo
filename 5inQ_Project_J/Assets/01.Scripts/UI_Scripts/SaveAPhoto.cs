using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SavePhoto : MonoBehaviour
{
    public Image photoView;
    public string albumName = "Statin-J";
    public string fileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".png";


    private void Start()
    {
        LoadResources();
        //StartCoroutine(GetPictureAndShowIt());
    }

    public void LoadResources()
    {
        // 내부 저장소에 저장된 이미지 파일 경로를 가져옵니다.
        string imagePath = Path.Combine(Application.persistentDataPath, "ImageName");

        // 파일이 존재하는지 확인합니다.
        if (File.Exists(imagePath))
        {
            // 파일을 바이트 배열로 읽어옵니다.
            byte[] imageBytes = File.ReadAllBytes(imagePath);

            // 바이트 배열을 Texture2D로 변환합니다.
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);

            // Texture2D를 Sprite로 변환합니다.
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            // Image 컴포넌트에 Sprite를 설정합니다.
            photoView.sprite = sprite;
        }
        else
        {
            Debug.LogWarning("Image file not found in persistent data path.");
        }

    }

    public void SaveToGallery()
    {
        Texture2D texture = photoView.sprite.texture;

        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(texture, albumName, fileName + ".png",
            (success, path) => Debug.Log("Image save result: " + success + " " + path));

        Debug.Log("Permission result: " + permission);
    }
   
}
