using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using UnityEngine.XR.ARFoundation.Samples;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;
using UnityEngine.Rendering.Universal;


public class TakeAShot : MonoBehaviour
{
    [Header("버튼 이미지")]
    [SerializeField] Image shotImage;
    [SerializeField] Sprite videoStopShot;

    [Header("버튼")]
    [SerializeField] GameObject videoStartBtn;
    [SerializeField] GameObject videoStopBtn;
    [SerializeField] GameObject returnBtn;
    [SerializeField] string sceneName;


    [SerializeField]private ARSession arsesion;
    private string mp4Path;
    [Header("카메라 영역")]
    [SerializeField] GameObject shotUI;
    Camera cam;

    void Start()
    {
        videoStartBtn.SetActive(true);
        videoStopBtn.SetActive(false);
        
        arsesion = GetComponent<ARSession>();
        mp4Path = Path.Combine(Application.persistentDataPath, "arcore_session.mp4");
        
        returnBtn.SetActive(true);
        shotUI.SetActive(true);

    }

    public void OnShotBtn()
    {
        StartCoroutine(ScreenShot());
        // "SavePhoto" 씬으로 이동한다.
        SceneManager.LoadScene("SavePhoto");
    }

    IEnumerator ScreenShot()
    {
        returnBtn.SetActive(false);
        shotUI.SetActive(false );
        yield return new WaitForEndOfFrame();

        if (CameraMode.isPhoto)
        {
            // 캡처된 화면을 Texture2D로 생성한다
            Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            Rect captureRect = new Rect(0, 0, Screen.width, Screen.height);
            tex.ReadPixels(captureRect, 0, 0);
            tex.Apply();

            // 캡처된 화면을 PNG 형식의 byte 배열로 변환한다.
            byte[] bytes = tex.EncodeToPNG();
            Destroy(tex);

            // byte 배열을 PNG 파일로 저장한다.
            string fileName = "ImageName.png";
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            File.WriteAllBytes(filePath, bytes);

        }
    }

    //비디오 시작 버튼을 눌렀을 때
    public void OnVideoStartBtn()
    {
        //비디오 모드면
        if (CameraMode.isVideo)
        {
            videoStartBtn.SetActive(false);
            videoStopBtn.SetActive(true);
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

    //비디오 촬영을 끝내는 버튼을 눌렀을 때
    public void OnRecordDoneBtn()
    {
        //촬영 중이면
        if (CameraMode.isRecord)
        {
            // 녹화 종료
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

                // 녹화가 성공적으로 완료되면 저장 씬으로 이동합니다.
                if (File.Exists(mp4Path))
                {
                    SceneManager.LoadScene("SavePhoto");
                }
            }
            //SceneManager.LoadScene("SavePhoto");
        }
    }

    public void OnReturnBtn()
    {
        SceneManager.LoadScene(sceneName);
    }
}
