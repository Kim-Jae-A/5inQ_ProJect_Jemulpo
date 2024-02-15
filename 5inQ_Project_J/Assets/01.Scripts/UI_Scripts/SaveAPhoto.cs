using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SavePhoto : MonoBehaviour
{
    [SerializeField] Image photoView;
    [SerializeField] GameObject saveMessage;
    [SerializeField] string sceneName;

    Texture2D savephotoTexture;
    private void Start()
    {
        GetImageAndShow();
        saveMessage.SetActive(false);
    }

    public void GetImageAndShow()
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
    }

    public void SaveToGalleryBtn()
    {
        savephotoTexture = photoView.sprite.texture;

        string albumName = "Station-J";
        string fileName = DateTime.Now.ToString("yyyyMMdd-HH:mm:ss");

        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(savephotoTexture, albumName, fileName + ".png",
        (success, path) => Invoke("DeleyUI", 3f));

        saveMessage.SetActive(true);

    }

    public void DeleyUI()
    {
        saveMessage.SetActive(false);       
    }

    public void ReturnBtn()
    {
        SceneManager.LoadScene(sceneName);
    }

}
