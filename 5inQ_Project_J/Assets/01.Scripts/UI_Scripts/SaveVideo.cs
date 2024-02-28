using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using DG.Tweening;
using UnityEngine.Android;

public class SaveVideo : MonoBehaviour
{
    [SerializeField] private RawImage PreviewZone;
    [SerializeField] GameObject saveMessage;
    [SerializeField] private VideoPlayer videoPlayer;
   
    void Start()
    {
        saveMessage.SetActive(false);
        videoPlayer.url = TakeAShot.recordPath;
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.Prepare();
    }
    void OnVideoPrepared(VideoPlayer vp)
    {
        // UI RawImage에 VideoPlayer의 outputTexture를 출력한다.
        PreviewZone.rectTransform.localEulerAngles = new Vector3(0, 0, -90);
        PreviewZone.texture = vp.texture;
        //WPreviewZone.rectTransform.localEulerAngles = new Vector3(0, 270, 0);
        // 영상을 재생한다.
        vp.Play();
    }
    public void OnReturnBtn()
    {
        // PreviewZone 초기화
        PreviewZone.texture = null;
        //PreviewZone.rectTransform.localEulerAngles = Vector3.zero;
        SceneManager.LoadScene("TakeAShot");
    }
    public void SaveToGallery_Video()
    {
        saveMessage.SetActive(true);
        var canvasGroup = saveMessage.GetComponent<CanvasGroup>();
        if(canvasGroup == null)
        {
            canvasGroup = saveMessage.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 1.0f;
        canvasGroup.DOFade(0f, 3f).OnComplete(() =>
        {
            saveMessage.SetActive(false);
        });


    }
}
