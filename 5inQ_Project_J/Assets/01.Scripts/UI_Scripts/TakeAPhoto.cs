using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using Unity.XR.CoreUtils;
using UnityEngine.XR.ARCore;

[RequireComponent(typeof(ARSession))]
public class TakeAShot : MonoBehaviour
{
    [Header("버튼")]
    [SerializeField] GameObject PictureBtn;
    [SerializeField] GameObject videoStartBtn;
    [SerializeField] GameObject videoStopBtn;

    [Header("카메라 영역")]
    [SerializeField] GameObject shotUI;
    [SerializeField] RenderTexture shotTexture;

    [Header("비디오 녹화")]
    [SerializeField]private ARSession m_Session;
    [SerializeField]Camera m_ARCamera;//추가
    //[SerializeField] RenderTexture m_RenderTexture;//추가
    private bool isRecording = false;
    private string VideoFilePath;

    private void Awake()
    {
        m_Session = GetComponent<ARSession>();
        m_ARCamera = GetComponent<Camera>();//추가

        //m_RenderTexture = new RenderTexture(Screen.width, Screen.height, 24);

        XROrigin m_xrorigin = FindObjectOfType<XROrigin>();
        /*
        if (m_xrorigin != null)
        {
            m_ARCamera = m_xrorigin.Camera;
            if (m_ARCamera != null)
            {
                // 추가: 렌더 텍스처 생성
                m_ARCamera.targetTexture = m_RenderTexture;
            }
        }*/
    }
    static int GetRotation() => Screen.orientation switch
    {
        ScreenOrientation.Portrait => 0,
        ScreenOrientation.LandscapeLeft => 90,
        ScreenOrientation.PortraitUpsideDown => 180,
        ScreenOrientation.LandscapeRight => 270,
        _ => 0
    };
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
            CameraMode.isRecord = true;
            PictureBtn.SetActive(false);
            videoStartBtn.SetActive(false);
            videoStopBtn.SetActive(true);
            isRecording = true;
            if (isRecording)
            {
                videoStartBtn.gameObject.SetActive(false);
                videoStopBtn.gameObject.SetActive(true);
#if UNITY_ANDROID
                if (m_Session.subsystem is ARCoreSessionSubsystem subsystem)
                {
                    var session = subsystem.session;
                    if (session == null)
                        return;

                    var playbackStatus = subsystem.playbackStatus;
                    var recordingStatus = subsystem.recordingStatus;

                    if (!playbackStatus.Playing() && !recordingStatus.Recording())
                    {
                        using (var config = new ArRecordingConfig(session))
                        {
                            string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "ar-video.mp4";
                            VideoFilePath = Path.Combine(Application.persistentDataPath, fileName);
                            config.SetMp4DatasetFilePath(session, VideoFilePath);
                            config.SetRecordingRotation(session, GetRotation());

                            subsystem.StartRecording(config);
                        }
                    }
                }
#endif
                Debug.Log("녹화시작");
                Debug.Log(isRecording);
            }
            isRecording = false;

        }
    }

    //비디오 촬영을 끝내는 버튼을 눌렀을 때
    public void OnRecordDoneBtn()
    {
        Debug.Log(isRecording);
        Debug.Log(CameraMode.isRecord);
        PictureBtn.SetActive(false);
        videoStartBtn.SetActive(true);
        videoStopBtn.SetActive(false);
        //촬영 중이면
        if (CameraMode.isRecord)
        {
            if (!isRecording)
            {
                Debug.Log("녹화종료");
                // 녹화 종료
                videoStartBtn.gameObject.SetActive(true);
                videoStopBtn.gameObject.SetActive(false);
#if UNITY_ANDROID
                if (m_Session.subsystem is ARCoreSessionSubsystem subsystem)
                {
                    var recordingStatus = subsystem.recordingStatus;

                    if (recordingStatus.Recording())
                    {
                        subsystem.StopRecording();
                    }
                }
#endif
                CameraMode.isRecord = false;
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

}
