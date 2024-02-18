using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation.Samples;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;


public class TakeAShot : MonoBehaviour
{
    [Header("��ư �̹���")]
    [SerializeField] Image shotImage;
    [SerializeField] Sprite videoStopShot;

    [Header("���� ��� ��ư")]
    [SerializeField] GameObject videoStartBtn;
    [SerializeField] GameObject videoStopBtn;

    [SerializeField]private ARSession arsesion;
    private string mp4Path;


    void Start()
    {
        videoStartBtn.SetActive(true);
        videoStopBtn.SetActive(false);
        arsesion = GetComponent<ARSession>();
        mp4Path = Path.Combine(Application.persistentDataPath, "arcore_session.mp4");
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
           if(arsesion.subsystem is ARCoreSessionSubsystem subsystem)
            {
                using (var config = new ArRecordingConfig(subsystem.session))
                {
                    config.SetMp4DatasetFilePath(subsystem.session, mp4Path);
                    var status = subsystem.StartRecording(config);
                    Debug.Log($"StartRecording to {config.GetMp4DatasetFilePath(subsystem.session)} => {status}");
                }
            }
           
            
        }
    }

    //���� �Կ��� ������ ��ư�� ������ ��
    public void OnRecordDoneBtn()
    {
        //�Կ� ���̸�
        if (CameraMode.isRecord)
        {
            // ��ȭ ����
            if (arsesion.subsystem is ARCoreSessionSubsystem subsystem)
            {
                var status = subsystem.StopRecording();
                Debug.Log($"StopRecording() => {status}");

                if (status == ArStatus.Success)
                {
                    Debug.Log(File.Exists(mp4Path)
                        ? $"ARCore session saved to {mp4Path}"
                        : "Recording completed, but no file was produced.");
                }

                // ��ȭ�� ���������� �Ϸ�Ǹ� ���� ������ �̵��մϴ�.
                if (File.Exists(mp4Path))
                {
                    SceneManager.LoadScene("SavePhoto");
                }
            }
            //SceneManager.LoadScene("SavePhoto");
        }
    }

}