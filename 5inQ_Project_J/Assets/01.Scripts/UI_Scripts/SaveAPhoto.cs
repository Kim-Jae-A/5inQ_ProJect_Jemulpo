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
   
/*    IEnumerator GetPictureAndShowIt()
    {
        files = Directory.GetFiles(Application.persistentDataPath, "*.png");

        if (files.Length > 0)
        {
            string pathToFile = files[files.Length - 1];
            Texture2D texture = GetScreenImage(pathToFile);
            yield return new WaitForEndOfFrame();

            if (texture != null)
            {
                Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 1));
                save_UI.GetComponent<Image>().sprite = sp;
            }
        }

    }*/

/*    private Texture2D GetScreenImage(string filePath)
    {
        Texture2D texture = null;
        byte[] fileBytes;
        if (File.Exists(filePath))
        {
            fileBytes = File.ReadAllBytes(filePath);
            texture = new Texture2D(1080, 2000, TextureFormat.ARGB32, false);
            texture.LoadImage(fileBytes);
        }
        return texture;
    }*/
}
