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
