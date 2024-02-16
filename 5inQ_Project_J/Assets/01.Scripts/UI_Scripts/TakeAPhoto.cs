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

    [Header("���� ��� ��ư")]
    [SerializeField] GameObject videoStartBtn;
    [SerializeField] GameObject videoStopBtn;

    void Start()
    {
        videoStartBtn.SetActive(true);
        videoStopBtn.SetActive(false);
    }

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
    }

    //���� ���� ��ư�� ������ ��
    public void OnVideoStartBtn()
    {
        videoStartBtn.SetActive(false);
        videoStopBtn.SetActive(true);
        //���� ����
        if (CameraMode.isVideo)
        {
            CameraMode.isVideo = false;
            CameraMode.isRecord = true;
        }
    }

    //���� �Կ��� ������ ��ư�� ������ ��
    public void OnRecordDoneBtn()
    {
        //�Կ� ���̸�
        if (CameraMode.isRecord)
        {
            SceneManager.LoadScene("SavePhoto");
        }
    }







}






