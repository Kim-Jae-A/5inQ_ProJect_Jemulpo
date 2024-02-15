using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SavePhoto : MonoBehaviour
{
    public Image photoView;


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
   
}
