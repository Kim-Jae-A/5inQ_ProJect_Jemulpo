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
//#if UNITY_ANDROID
//        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
//        {
//            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
//        }

//        string destPath = Path.Combine(Application.persistentDataPath, "DCIM", "Camera", Path.GetFileName(TakeAShot.recordPath));
//        File.Copy(TakeAShot.recordPath, destPath, true);

//        using (var javaClass = new AndroidJavaClass("android.media.MediaScannerConnection"))
//        {
//            javaClass.CallStatic("scanFile", new object[] {
//                new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"),
//                new string[] { destPath },
//                null,
//                null
//            });
//        }
//#else
//        Debug.LogWarning("Gallery saving is only supported on Android.");
//#endif
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
