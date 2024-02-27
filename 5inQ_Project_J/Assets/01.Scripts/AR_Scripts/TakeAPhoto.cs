using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using Unity.XR.CoreUtils;
using UnityEngine.XR.ARCore;
using Google.XR.ARCoreExtensions;
using Unity.Collections;
using UnityEngine.XR.ARSubsystems;
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
    string m_Mp4Path;
    public static string recordPath;
    private bool isRecording = false;
    string path = $"/storage/emulated/0/DCIM";

#if UNITY_ANDROID
    ArStatus? m_SetMp4DatasetResult;
    ArPlaybackStatus m_PlaybackStatus = (ArPlaybackStatus)(-1);
    ArRecordingStatus m_RecordingStatus = (ArRecordingStatus)(-1);
#endif

    static int GetRotation() => Screen.orientation switch
    {
        ScreenOrientation.Portrait => 0,
        ScreenOrientation.LandscapeLeft => 90,
        ScreenOrientation.PortraitUpsideDown => 180,
        ScreenOrientation.LandscapeRight => 0,
        _ => 0
    };


    private void Awake()
    {
        m_Session = GetComponent<ARSession>();
        bool isExist = Directory.Exists(path);
        if (!isExist)
        {
            Directory.CreateDirectory(path);
        }
    }

    public void OnShotBtn()
    {
        StartCoroutine(ScreenShot());
        // "SavePhoto" 씬으로 이동한다.
        SceneManager.LoadScene("SavePhoto");
    }

    IEnumerator ScreenShot()
    {
        shotUI.SetActive(false);
        yield return new WaitForEndOfFrame();

        if (CameraMode.isPhoto)
        {
            // 캡처된 화면을 Texture2D로 생성한다
            Texture2D tex = new Texture2D(shotTexture.width, shotTexture.height, TextureFormat.RGB24, false);

            RenderTexture.active = shotTexture;

            tex.ReadPixels(new Rect(0, 0, shotTexture.width, shotTexture.height), 0, 0);
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
                Debug.Log("녹화시작");
                Debug.Log(isRecording);
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
                            m_Mp4Path = Path.Combine(path, fileName);
                            recordPath = m_Mp4Path;
                            config.SetMp4DatasetFilePath(session, m_Mp4Path);
                            config.SetRecordingRotation(session,0);
                            subsystem.StartRecording(config);
                        }
                    }
                }
#endif
            }
        }
            isRecording = false;
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
                // 녹화 종료
                Debug.Log("녹화종료");
                videoStartBtn.gameObject.SetActive(true);
                videoStopBtn.gameObject.SetActive(false);
                CameraMode.isRecord = false;
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
                SceneManager.LoadScene("SaveVideo");
            }
        }
    }

    public void OnReturnBtn()
    {
        SceneManager.LoadScene("PhotoZone_Docent");
    }

    //IEnumerator StopRecordingCoroutine()
    //{
    //    if (m_Session.subsystem is ARCoreSessionSubsystem subsystem)
    //    {
    //        yield return new WaitUntil(() => subsystem.recordingStatus == ArRecordingStatus.Ok);

    //        session.StopRecording();
    //        yield return new WaitForSeconds(2);
    //    }
    //}

    //private void OnApplicationPause(bool pause)
    //{
    //    if (pause)
    //    {
    //        if (m_Session.subsystem is ARCoreSessionSubsystem subsystem)
    //        {
    //            session = subsystem.session;

    //            using (var config = new ArRecordingConfig(session))
    //            {
    //                config.SetAutoStopOnPause(session, isPaused);
    //            }
    //        }
    //    }
    //}

}
