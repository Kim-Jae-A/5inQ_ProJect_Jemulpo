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
        // ���� ����ҿ� ����� �̹��� ���� ��θ� �����ɴϴ�.
        string imagePath = Path.Combine(Application.persistentDataPath, "ImageName");

        // ������ �����ϴ��� Ȯ���մϴ�.
        if (File.Exists(imagePath))
        {
            // ������ ����Ʈ �迭�� �о�ɴϴ�.
            byte[] imageBytes = File.ReadAllBytes(imagePath);

            // ����Ʈ �迭�� Texture2D�� ��ȯ�մϴ�.
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);

            // Texture2D�� Sprite�� ��ȯ�մϴ�.
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            // Image ������Ʈ�� Sprite�� �����մϴ�.
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
