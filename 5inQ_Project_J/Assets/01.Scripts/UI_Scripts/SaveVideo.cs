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
        // ���� ������ byte �迭�� �о���δ�.
        string videoFilePath = Path.Combine(Application.persistentDataPath, "RecordedVideo.mp4");
        
        videoPlayer.url = videoFilePath;
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.Prepare();
    }
    void OnVideoPrepared(VideoPlayer vp)
    {
        // UI RawImage�� VideoPlayer�� outputTexture�� ����Ѵ�.
        PreviewZone.texture = vp.texture;

        // ������ ����Ѵ�.
        vp.Play();
    }
}
