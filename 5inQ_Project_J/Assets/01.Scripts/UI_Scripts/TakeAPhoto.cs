using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TakeAShot : MonoBehaviour
{
    [Header("��ư �̹���")]
    [SerializeField] Image shotImage;
    [SerializeField] Sprite videoStopShot;

    public void OnShotBtn()
    {
        StartCoroutine(ScreenShot());
    }

    IEnumerator ScreenShot()
    {
        yield return new WaitForEndOfFrame();

        if (CameraMode.isPhoto)
        {
            // ĸó�� ȭ���� Texture2D�� �����Ѵ�
            Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            tex.Apply();

            // ĸó�� ȭ���� PNG ������ byte �迭�� ��ȯ�Ѵ�.
            byte[] bytes = tex.EncodeToPNG();
            Destroy(tex);

            // byte �迭�� PNG ���Ϸ� �����Ѵ�.
            string fileName = "ImageName";
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            File.WriteAllBytes(filePath, bytes);

            // "SavePhoto" ������ �̵��Ѵ�.
            SceneManager.LoadScene("SavePhoto");
        }

        //���� ��ư�� �������� ��
        if(CameraMode.isVideo)
        {
            shotImage.sprite = videoStopShot;
            CameraMode.isVideo = false;
            CameraMode.isRecord = true;
        }

        yield return null;

        //���� �Կ� ��ư�� ������ ��
        if(CameraMode.isRecord)
        {         
            CameraMode.isRecord = false;
            CameraMode.isRecordDone = true;
        }

        yield return null;

        //���� �Կ��� ������ ��ư�� ������ ��
        if (CameraMode.isRecordDone)
        {
            SceneManager.LoadScene("SavePhoto");
        }
 
    }

}






