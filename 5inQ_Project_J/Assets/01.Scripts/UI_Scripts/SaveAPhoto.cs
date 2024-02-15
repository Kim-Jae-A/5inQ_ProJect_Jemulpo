using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SavePhoto : MonoBehaviour
{
    public GameObject save_UI;
    string[] files = null;


    private void Start()
    {
        files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");

        if (files.Length > 0)
        {
            GetPictureAndShowIt();
        }
    }

    private void GetPictureAndShowIt()
    {
        string pathToFile = files[files.Length - 1];
        Texture2D texture = GetScreenImage(pathToFile);
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, 1080, 2000), new Vector2(0.5f, 1));
        save_UI.GetComponent<Image>().sprite = sp;
    }

    private Texture2D GetScreenImage(string filePath)
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
    }
}
