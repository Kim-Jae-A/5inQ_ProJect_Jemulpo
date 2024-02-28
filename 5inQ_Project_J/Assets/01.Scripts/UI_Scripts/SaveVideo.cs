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
        //녹화된 영상을 재생하기 위해 저장된 경로를 받아와 영상을 재생하기 위해 준비가 완료되면 OnVideoPrepared 메서드를 호출한다.
        videoPlayer.url = TakeAShot.recordPath;
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.Prepare(); //비디오를 로드하고 재생가능 상태로 변경.
    }
    void OnVideoPrepared(VideoPlayer vp)
    {
        //미리보기 화면이 회전되어서 보이는 현상을 방지합니다.
        PreviewZone.rectTransform.localEulerAngles = new Vector3(0, 0, -90);
        // UI RawImage에 VideoPlayer의 outputTexture를 출력한다.
        PreviewZone.texture = vp.texture;
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

    //Arcore를 사용해서 녹화를 진행하면 startRecording을 할 때 경로를 지정해줬기에 저장은 녹화를 시작하자마자 이루어집니다..
    public void SaveToGallery_Video()
    {
        //저장 메시지를 활성화시키고 Dotween을 사용해 3초뒤 서서히 사라지게 만듭니다.
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
