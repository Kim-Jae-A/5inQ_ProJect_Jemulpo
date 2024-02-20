using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARCore;
namespace UnityEngine.XR.ARFoundation.Samples
{
    class LogUtil
    {
        List<string> m_Entries = new List<string>();

        string m_FullLog = "";

        int m_Count;

        bool m_Dirty;

        public int maxCount { get; set; } = 10;

        public void Log(string msg)
        {
            m_Entries.Add($"{m_Count}> {msg}");
            m_Count++;
            m_Dirty = true;
        }

        public override string ToString()
        {
            if (m_Dirty)
            {
                var removeCount = m_Entries.Count - maxCount;
                if (removeCount > 0)
                {
                    m_Entries.RemoveRange(0, removeCount);
                }

                m_FullLog = string.Join("\n", m_Entries);
                m_Dirty = false;
            }

            return m_FullLog;
        }
    }

    [RequireComponent(typeof(ARSession))]
    public class Recording : MonoBehaviour
    {
        LogUtil m_Log = new LogUtil();
        ARSession session;
#if UNITY_ANDROID
    ArStatus? m_SetMp4DatasetResult;
    ArPlaybackStatus m_PlaybackStatus = (ArPlaybackStatus)(-1);
    ArRecordingStatus m_RecordingStatus = (ArRecordingStatus)(-1);
#endif
        string m_PlaybackStatusMessage;

        string m_RecordingStatusMessage;

        string m_Mp4Path;
        private void Awake()
        {
            session = GetComponent<ARSession>();
            m_Mp4Path = Path.Combine(Application.persistentDataPath, "arcore_session.mp4");
        }
        static int GetRotation() => Screen.orientation switch
        {
            ScreenOrientation.Portrait => 0,
            ScreenOrientation.LandscapeLeft => 90,
            ScreenOrientation.PortraitUpsideDown => 180,
            ScreenOrientation.LandscapeRight => 270,
            _ => 0
        };

        void Log(string msg)
        {
            Debug.Log(msg);
            m_Log.Log(msg);
        }

        static string GetFileSize(string path)
        {
            var byteCount = new FileInfo(path).Length;
            const long kiloBytes = 1024;
            const long megaBytes = kiloBytes * kiloBytes;

            if (byteCount / megaBytes > 0)
            {
                var size = (float)byteCount / megaBytes;
                return $"{size:F2} Mb";
            }

            if (byteCount / kiloBytes > 0)
            {
                var size = (float)byteCount / kiloBytes;
                return $"{size:F2} Kb";
            }

            return $"{byteCount} Bytes";
        }

        void OnGUI()
        {
            GUI.skin.label.fontSize = 50;
            GUI.skin.button.fontSize = 50;
            GUI.skin.button.padding = new RectOffset(10, 10, 25, 25);
            GUILayout.Space(50);

#if UNITY_ANDROID
            if (session.subsystem is ARCoreSessionSubsystem subsystem)
            {
                var session = subsystem.session;
                if (session == null)
                    return;

                var playbackStatus = subsystem.playbackStatus;
                var recordingStatus = subsystem.recordingStatus;

                if (!playbackStatus.Playing() &&
                    !recordingStatus.Recording())
                {
                    if (playbackStatus != ArPlaybackStatus.Finished && GUILayout.Button("Start recording"))
                    {
                        using (var config = new ArRecordingConfig(session))
                        {
                            config.SetMp4DatasetFilePath(session, m_Mp4Path);
                            config.SetRecordingRotation(session, GetRotation());
                            var status = subsystem.StartRecording(config);
                            Log($"StartRecording to {config.GetMp4DatasetFilePath(session)} => {status}");
                        }
                    }

                    if (File.Exists(m_Mp4Path) && GUILayout.Button("Start playback"))
                    {
                        var status = subsystem.StartPlayback(m_Mp4Path);
                        Log($"StartPlayback({m_Mp4Path}) => {status}");
                    }
                }

                if (playbackStatus.Playing() &&
                    !recordingStatus.Recording() &&
                    GUILayout.Button("Stop playback"))
                {
                    var status = subsystem.StopPlayback();
                    Log($"StopPlayback() => {status}");
                }

                if (playbackStatus == ArPlaybackStatus.Finished &&
                    GUILayout.Button("Return to live feed"))
                {
                    var status = subsystem.StopPlayback();
                    Log($"StopPlayback() => {status}");
                }

                if (recordingStatus.Recording() &&
                    GUILayout.Button("Stop recording"))
                {
                    var status = subsystem.StopRecording();
                    Log($"StopRecording() => {status}");

                    if (status == ArStatus.Success)
                    {
                        Log(File.Exists(m_Mp4Path)
                            ? $"ARCore session saved to {m_Mp4Path} ({GetFileSize(m_Mp4Path)})"
                            : "Recording completed, but no file was produced.");
                    }
                }

                if (m_PlaybackStatus != playbackStatus)
                {
                    m_PlaybackStatus = playbackStatus;
                    m_PlaybackStatusMessage = $"Playback status: {m_PlaybackStatus}";
                }

                if (m_RecordingStatus != recordingStatus)
                {
                    m_RecordingStatus = recordingStatus;
                    m_RecordingStatusMessage = $"Recording status: {m_RecordingStatus}";
                }

                GUILayout.Label(m_PlaybackStatusMessage);
                GUILayout.Label(m_RecordingStatusMessage);
                GUILayout.Space(50);
                GUILayout.Label(m_Log.ToString());
            }
            else
            {
                GUILayout.Label("No " + nameof(ARCoreSessionSubsystem) + " available. Cannot perform session recording.");
            }
#else
            GUILayout.Label("ARCore session recording is only supported on Android.");
#endif
        }
        public void OnStartRecording()
        {
            if(session.subsystem is ARCoreSessionSubsystem subsystem)
            {
                var session = subsystem.session;
                if (session == null) 
                {
                    return;
                }
                var recordingStatus = subsystem.recordingStatus;
                if (!recordingStatus.Recording())
                {
                    using (var config = new ArRecordingConfig(session))
                    {
                        config.SetMp4DatasetFilePath(session, m_Mp4Path);
                        config.SetRecordingRotation(session, GetRotation());
                        var status = subsystem.StartRecording(config);
                        Log($"StartRecording to {config.GetMp4DatasetFilePath(session)} => {status}");
                    }
                }
            }
        }
        public void OnStopRecording()
        {
            if (session.subsystem is ARCoreSessionSubsystem subsystem)
            {
                var status = subsystem.StopRecording();
                Log($"StopRecording() => {status}");

                if (status == ArStatus.Success)
                {
                    Log(File.Exists(m_Mp4Path)
                        ? $"ARCore session saved to {m_Mp4Path} ({GetFileSize(m_Mp4Path)})"
                        : "Recording completed, but no file was produced.");
                }
            }
        }
        public void OnSaveRecording()
        {
            if (session.subsystem is ARCoreSessionSubsystem subsystem)
            {
                var playbackStatus = subsystem.playbackStatus;

                // 녹화 중이지 않고 재생 중이면 재생 중지
                if (!subsystem.recordingStatus.Recording() && playbackStatus.Playing())
                {
                    var stopPlaybackStatus = subsystem.StopPlayback();
                    Log($"StopPlayback() => {stopPlaybackStatus}");
                }
                Debug.Log($"ARCore session saved to: {m_Mp4Path} ({GetFileSize(m_Mp4Path)})");

                // 바탕화면에 복사
                CopyFileToDesktop(m_Mp4Path);

                // "SavePhoto" 씬으로 이동
                SceneManager.LoadScene("SavePhoto");
            }
        }
        private void CopyFileToDesktop(string sourceFilePath)
        {
            // 바탕화면 경로 가져오기
            string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);

            try
            {
                // 파일 이름 가져오기
                string fileName = Path.GetFileName(sourceFilePath);

                // 바탕화면에 파일 복사
                string destinationFilePath = Path.Combine(desktopPath, fileName);
                File.Copy(sourceFilePath, destinationFilePath, true);

                // 성공적으로 복사되었으면 디버그 메시지 출력
                Debug.Log($"File copied to Desktop: {destinationFilePath}");
            }
            catch (Exception e)
            {
                // 복사 중 에러 발생 시 예외 처리
                Debug.LogError($"Error copying file to Desktop: {e.Message}");
            }
        }
    }
}
    


