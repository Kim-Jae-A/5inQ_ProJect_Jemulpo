using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
//#if UNITY_ANDROID
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;
//#endif

[RequireComponent(typeof(ARSession))]
public class Recording : MonoBehaviour
{
    [SerializeField] private Button StartRecordBtn;
    [SerializeField] private Button EndRecordBtn;

    [SerializeField]private ARSession m_Session;
    private Camera m_ARCamera;//추가
    RenderTexture m_RenderTexture;//추가

#if UNITY_ANDROID
    ArStatus? m_SetMp4DatasetResult;
    ArPlaybackStatus m_PlaybackStatus = (ArPlaybackStatus)(-1);
    ArRecordingStatus m_RecordingStatus = (ArRecordingStatus)(-1);
#endif

    string m_Mp4Path;

    void Awake()
    {
        m_Session = GetComponent<ARSession>();
        m_ARCamera = GetComponent<Camera>();//추가

        XROrigin m_xrorigin = FindObjectOfType<XROrigin>();
        if(m_xrorigin != null)
        {
            m_ARCamera = m_xrorigin.Camera;
            if(m_ARCamera != null)
            {
                // 추가: 렌더 텍스처 생성
                m_RenderTexture = new RenderTexture(Screen.width, Screen.height, 24);
                m_ARCamera.targetTexture = m_RenderTexture;
            }
        }

    }

    static int GetRotation() => Screen.orientation switch
    {
        ScreenOrientation.Portrait => 0,
        ScreenOrientation.LandscapeLeft => 90,
        ScreenOrientation.PortraitUpsideDown => 180,
        ScreenOrientation.LandscapeRight => 270,
        _ => 0
    };

    public void StartRecordingSession()
    {
        StartRecordBtn.gameObject.SetActive(false);
        EndRecordBtn.gameObject.SetActive(true);
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
                    m_Mp4Path = Path.Combine(Application.persistentDataPath, fileName);
                    config.SetMp4DatasetFilePath(session, m_Mp4Path);
                    config.SetRecordingRotation(session, GetRotation());
                    
                    subsystem.StartRecording(config);
                }
            }
        }
#endif
    }

    public void StopRecordingSession()
    {
        StartRecordBtn.gameObject.SetActive(true);
        EndRecordBtn.gameObject.SetActive(false);
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
    }

}