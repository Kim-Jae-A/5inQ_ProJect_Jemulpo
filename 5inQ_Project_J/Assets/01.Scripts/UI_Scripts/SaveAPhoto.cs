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
