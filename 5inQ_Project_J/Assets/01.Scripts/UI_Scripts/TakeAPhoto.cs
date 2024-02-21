using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*using UnityEngine.XR.ARFoundation.Samples;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;
using UnityEngine.Rendering.Universal;
using UnityEditor.Recorder;
using UnityEngine.Recorder.Examples;
using UnityEditor.Recorder.Encoder;
using UnityEditor.Recorder.Input;
*/

public class TakeAShot : MonoBehaviour
{
    [Header("버튼 이미지")]
    [SerializeField] Image shotImage;
    [SerializeField] Sprite videoStopShot;

    [Header("버튼")]
    [SerializeField] GameObject videoStartBtn;
    [SerializeField] GameObject videoStopBtn;

    [Header("카메라 영역")]
    [SerializeField] GameObject shotUI;
    [SerializeField]RenderTexture shotTexture;


    private bool isRecording = false;
    private string VideoFilePath;
    [SerializeField] private GameObject Test1;
    [SerializeField] private GameObject Test2;

    void Start()
    {
        videoStartBtn.SetActive(true);
        videoStopBtn.SetActive(false);  
  
        Test1.SetActive(false);
        Test2.SetActive(false);
    }
    public void OnShotBtn()
    {
        StartCoroutine(ScreenShot());
        // "SavePhoto" 씬으로 이동한다.
        SceneManager.LoadScene("SavePhoto");
    }

   //비디오 시작 버튼을 눌렀을 때
    public void OnVideoStartBtn()
    {
        //비디오 모드면
        if (CameraMode.isVideo)
        {
            videoStartBtn.SetActive(false);
            videoStopBtn.SetActive(true);
            isRecording = true;
            if (isRecording)
            {
                //녹화시작
         
                Test1.SetActive(true);
                Debug.Log("녹화시작");
            }
 
        }
    }

    //비디오 촬영을 끝내는 버튼을 눌렀을 때
    public void OnRecordDoneBtn()
    {
        isRecording = false;
        videoStartBtn.SetActive(true);
        videoStopBtn.SetActive(false);
        //촬영 중이면
        if (CameraMode.isRecord)
        {
            if (!isRecording)
            {
                // 녹화 종료
               
                Test2.SetActive(true);
                Debug.Log("녹화종료");
                VideoFilePath = Application.persistentDataPath + "/temp_video.mp4";
                string destinationPath = Path.Combine(Application.persistentDataPath, "RecordedVideo.mp4");
                CopyRecordedVideo(VideoFilePath, destinationPath);
                SceneManager.LoadScene("SavePhoto");
            }
        }
    }
    IEnumerator ScreenShot()
    {
        shotUI.SetActive(false );
        yield return new WaitForEndOfFrame();

        if (CameraMode.isPhoto)
        {
            RenderTexture.active = shotTexture;

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

            RenderTexture.active = null;
        }
    }

    public void OnReturnBtn()
    {
        SceneManager.LoadScene("PhotoZone_Docent");
    }
    void CopyRecordedVideo(string sourcePath, string destinationPath)
    {
        // 녹화된 영상을 복사하는 함수
        File.Copy(sourcePath, destinationPath, true);
    }

}
