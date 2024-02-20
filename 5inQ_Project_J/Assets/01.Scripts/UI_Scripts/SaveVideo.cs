using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveVideo : MonoBehaviour
{
    [SerializeField] private RawImage PreviewZone;
    [SerializeField] GameObject saveMessage;
    [SerializeField] private Camera ArCamera;
    private string videoFolderPath;
    // Start is called before the first frame update
    void Start()
    {
        saveMessage.SetActive(false);
        videoFolderPath = Path.Combine(Application.persistentDataPath, "Movies", "AndroidUtils");
        if (!Directory.Exists(videoFolderPath))
        {
            Directory.CreateDirectory(videoFolderPath);
        }
        videoFolderPath = videoFolderPath.Replace("\\", "/"); // 역슬래시를 슬래시로 변경
        string videoFileName = "Record.mp4";
        string videoFilePath = Path.Combine(videoFolderPath, videoFileName);
        videoFilePath = videoFilePath.Replace("\\", "/"); // 역슬래시를 슬래시로 변경
        string[] files = Directory.GetFiles(videoFolderPath);
        foreach (string file in files)
        {
            Debug.Log("File in directory: " + file);
        }
        GetVideoFile_Preview(videoFilePath);



    }
    private void GetVideoFile_Preview(string videoFilePath)
    {
        if (File.Exists(videoFilePath))
        {
            UnityEngine.Video.VideoPlayer videoPlayer = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
            videoPlayer.playOnAwake = false;
            videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
            videoPlayer.targetCamera = ArCamera;

            videoPlayer.url = videoFilePath;
            videoPlayer.Prepare();

            videoPlayer.prepareCompleted += (source) =>
            {
                PreviewZone.texture = videoPlayer.texture;
                videoPlayer.Play();
            };
        }
        else
        {
            Debug.LogError("Video file not found: " + videoFilePath);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
