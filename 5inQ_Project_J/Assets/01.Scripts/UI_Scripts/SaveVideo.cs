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
        //��ȭ�� ������ ����ϱ� ���� ����� ��θ� �޾ƿ� ������ ����ϱ� ���� �غ� �Ϸ�Ǹ� OnVideoPrepared �޼��带 ȣ���Ѵ�.
        videoPlayer.url = TakeAShot.recordPath;
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.Prepare(); //������ �ε��ϰ� ������� ���·� ����.
    }
    void OnVideoPrepared(VideoPlayer vp)
    {
        //�̸����� ȭ���� ȸ���Ǿ ���̴� ������ �����մϴ�.
        PreviewZone.rectTransform.localEulerAngles = new Vector3(0, 0, -90);
        // UI RawImage�� VideoPlayer�� outputTexture�� ����Ѵ�.
        PreviewZone.texture = vp.texture;
        // ������ ����Ѵ�.
        vp.Play();
    }
    public void OnReturnBtn()
    {
        // PreviewZone �ʱ�ȭ
        PreviewZone.texture = null;
        //PreviewZone.rectTransform.localEulerAngles = Vector3.zero;
        SceneManager.LoadScene("TakeAShot");
    }

    //Arcore�� ����ؼ� ��ȭ�� �����ϸ� startRecording�� �� �� ��θ� ��������⿡ ������ ��ȭ�� �������ڸ��� �̷�����ϴ�..
    public void SaveToGallery_Video()
    {
        //���� �޽����� Ȱ��ȭ��Ű�� Dotween�� ����� 3�ʵ� ������ ������� ����ϴ�.
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
