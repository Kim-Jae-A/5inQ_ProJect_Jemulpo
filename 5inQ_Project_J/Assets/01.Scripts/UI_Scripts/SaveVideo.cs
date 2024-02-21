using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SaveVideo : MonoBehaviour
{
    [SerializeField] private RawImage PreviewZone;
    [SerializeField] GameObject saveMessage;
    [SerializeField] private VideoPlayer videoPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        saveMessage.SetActive(false);
        // 영상 파일을 byte 배열로 읽어들인다.
        string videoFilePath = Path.Combine(Application.persistentDataPath, "RecordedVideo.mp4");
        
        videoPlayer.url = videoFilePath;
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.Prepare();
    }
    void OnVideoPrepared(VideoPlayer vp)
    {
        // UI RawImage에 VideoPlayer의 outputTexture를 출력한다.
        PreviewZone.texture = vp.texture;

        // 영상을 재생한다.
        vp.Play();
    }
}
