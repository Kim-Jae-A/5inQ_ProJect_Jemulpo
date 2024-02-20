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
using UnityEditor.Recorder;
using UnityEditor.Recorder.Encoder;
using UnityEditor.Recorder.Input;


public class TakeAShot : MonoBehaviour
{
    //[Header("버튼 이미지")]
    //[SerializeField] Image shotImage;
    //[SerializeField] Sprite videoStopShot;//2개 사용 X

    [Header("버튼")]
    [SerializeField] GameObject videoStartBtn;
    [SerializeField] GameObject videoStopBtn;
    [SerializeField] GameObject returnBtn;

    [Header("카메라 영역")]
    [SerializeField] GameObject shotUI;
    [SerializeField]private Camera ARcamera;

    [SerializeField] string sceneName;

    private AndroidUtils RecordingManager;
    private bool shouldLoadNextScene = false;

    void Start()
    {
        videoStartBtn.SetActive(false);
        videoStopBtn.SetActive(false);
        
        returnBtn.SetActive(true);
        shotUI.SetActive(true);
        RecordingManager = new AndroidUtils();
    }
    void Update()
    {
        if (shouldLoadNextScene)
        {
            SceneManager.LoadScene("SaveVideo");
        }
    }
   /*public FileInfo OutputFile
    {
        get
        {
            var fileName = _settings.OutputFile + ".mp4";
            return new FileInfo(fileName);
        }
    }*/
    /*private void InitializeRecord()
    {
        var controllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>();
        _recorderController = new RecorderController(controllerSettings);

        var OutputFolder = new DirectoryInfo(Path.Combine(Application.dataPath, "SampleRecording"));
        if (!OutputFolder.Exists)
        {
            OutputFolder.Create();
        }


        _settings = ScriptableObject.CreateInstance<MovieRecorderSettings>();
        _settings.name = "Recorder_AR";
        _settings.Enabled = true;

        _settings.EncoderSettings = new CoreEncoderSettings
        {
            EncodingQuality = CoreEncoderSettings.VideoEncodingQuality.Medium,
            Codec = CoreEncoderSettings.OutputCodec.MP4
        };
        _settings.CaptureAlpha = true;

        var ArCamera = FindObjectOfType<ARCameraManager>().GetComponent<Camera>();
        _settings.ImageInputSettings = new CameraInputSettings
        {
            CameraTag = "MainCamera",
            OutputWidth = 1080,
            OutputHeight = 2400
        };

        _settings.OutputFile = Path.Combine(Application.persistentDataPath, "SampleRecording", "video");

        controllerSettings.AddRecorderSettings(_settings);
        controllerSettings.SetRecordModeToManual();
        controllerSettings.FrameRate = 30.0f;

        RecorderOptions.VerboseMode = false;
        _recorderController.PrepareRecording();
    }*/

    public void OnShotBtn()
    {
        StartCoroutine(ScreenShot());
        Debug.Log("이게왜 실행되냐");
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
            CameraMode.isVideo = false;
            CameraMode.isRecord = true;
            if(CameraMode.isRecord == true)
            {
                RecordingManager.StartRecording();
                Debug.Log("녹화시작");
            }
        }
    }

    //비디오 촬영을 끝내는 버튼을 눌렀을 때
    public void OnRecordDoneBtn()
    {
        
        //촬영 중이면
        if (CameraMode.isRecord)
        {
            CameraMode.isRecord = false;
            CameraMode.isRecordDone = true;
            videoStartBtn.SetActive (true);
            videoStopBtn.SetActive (false);
            if(CameraMode.isRecordDone == true)
            {
                // 녹화 종료
                RecordingManager.StopRecording();
                Debug.Log("녹화종료");
                shouldLoadNextScene = true;
                CameraMode.isRecordDone = false;
            }
        }
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

    public void OnReturnBtn()
    {
        SceneManager.LoadScene(sceneName);
    }
   
}
