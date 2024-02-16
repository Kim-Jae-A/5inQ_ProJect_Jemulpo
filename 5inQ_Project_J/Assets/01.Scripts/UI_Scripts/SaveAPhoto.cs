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
        // 내부 저장소에 저장된 이미지 파일 경로를 가져온다
        string imagePath = Path.Combine(Application.persistentDataPath, "ImageName");

        // 파일이 존재하는지 확인
        if (File.Exists(imagePath))
        {
            // 파일을 바이트 배열로 읽어온다
            byte[] imageBytes = File.ReadAllBytes(imagePath);

            // 바이트 배열을 Texture2D로 변환
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);

            // Texture2D를 Sprite로 변환
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            // Image 컴포넌트에 Sprite를 설정
            photoView.sprite = sprite;
        }
    }

    public void SaveToGalleryBtn()
    {
        //빈 텍스쳐에 사진이 출력된 이미지의 텍스처를 저장한다
        savephotoTexture = photoView.sprite.texture;

        //앨범이름, 파일이름 설정
        string albumName = "Station-J";
        string fileName = DateTime.Now.ToString("yyyyMMdd-HH:mm:ss");

        //NativeGallery를 사용해 갤러리에 저장
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(savephotoTexture, albumName, fileName + ".png",
        (success, path) => Invoke("DeleyUI", 3f)); //저장 메세지 띄운 후 3초 이후 비활성화

        //저장메세지
        saveMessage.SetActive(true);

    }

    public void DeleyUI()
    {
        //비활성화
        saveMessage.SetActive(false);       
    }

    public void ReturnBtn()
    {
        //카메라 촬영 씬으로 돌아간다
        SceneManager.LoadScene(sceneName);
    }

}
