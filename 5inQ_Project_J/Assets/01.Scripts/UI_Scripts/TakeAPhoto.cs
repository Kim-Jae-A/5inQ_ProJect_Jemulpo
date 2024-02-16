using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class TakeAShot : MonoBehaviour
{
    [Header("��ư �̹���")]
    [SerializeField] Image shotImage;
    [SerializeField] Sprite videoStopShot;

    [Header("��ư")]
    [SerializeField] GameObject videoStartBtn;
    [SerializeField] GameObject videoStopBtn;
    [SerializeField] GameObject returnBtn;
    [SerializeField] string sceneName;

    [Header("ī�޶� ����")]
    [SerializeField] GameObject shotUI;


    Camera cam;
    void Start()
    {
        videoStartBtn.SetActive(true);
        videoStopBtn.SetActive(false);
        returnBtn.SetActive(true);
        shotUI.SetActive(true);
    }

    public void OnShotBtn()
    {
        StartCoroutine(ScreenShot());
        // "SavePhoto" ������ �̵��Ѵ�.
        SceneManager.LoadScene("SavePhoto");
    }

    IEnumerator ScreenShot()
    {
        returnBtn.SetActive(false);
        shotUI.SetActive(false );
        yield return new WaitForEndOfFrame();

        if (CameraMode.isPhoto)
        {
            // ĸó�� ȭ���� Texture2D�� �����Ѵ�
            Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            Rect captureRect = new Rect(0, 0, Screen.width, Screen.height);
            tex.ReadPixels(captureRect, 0, 0);
            tex.Apply();

            // ĸó�� ȭ���� PNG ������ byte �迭�� ��ȯ�Ѵ�.
            byte[] bytes = tex.EncodeToPNG();
            Destroy(tex);

            // byte �迭�� PNG ���Ϸ� �����Ѵ�.
            string fileName = "ImageName.png";
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            File.WriteAllBytes(filePath, bytes);

        }
    }

    //���� ���� ��ư�� ������ ��
    public void OnVideoStartBtn()
    {
        //���� ����
        if (CameraMode.isVideo)
        {
            videoStartBtn.SetActive(false);
            videoStopBtn.SetActive(true);
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

    public void OnReturnBtn()
    {
        SceneManager.LoadScene(sceneName);
    }







}






