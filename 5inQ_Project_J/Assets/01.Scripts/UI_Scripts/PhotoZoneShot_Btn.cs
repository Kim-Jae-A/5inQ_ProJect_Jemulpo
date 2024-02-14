using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Playables;
using static NativeCamera;
using UnityEngine.Video;

public class PhotoZoneShot_Btn : MonoBehaviour
{
    [Header("ÃÔ¿µ ¸ðµå")]
    [SerializeField] Text photoText;
    [SerializeField] Text videoText;
    [SerializeField] Image shotImage;
    [SerializeField] Sprite videoStartShot;
    [SerializeField] Sprite videoStopShot;
    [SerializeField] Sprite photoShot;
        

    Color Highlightcolor = new Color(0, 0.6f, 1);
    Color Normalcolor = new Color(0.5f, 0.5f, 0.5f, 0.8f);

    public static bool isPicture = true;
    public static bool isVideo = false;

    void Start()
    {
        if (isPicture)
        {
            HighlightPhotoText();
        }

    }
    public void OnPhotoBtn()
    {
        isPicture = true;
        isVideo = false;

        HighlightPhotoText();
        NormalVideoText();

        shotImage.sprite = photoShot;
    }

    public void OnVideoBtn()
    {
        isVideo = true;
        isPicture = false;

        HighlightVideoText();
        NormalPhotoText();

        shotImage.sprite = videoStartShot;
    }



    #region TextStyle
    public void HighlightPhotoText()
    {
        photoText.fontStyle = FontStyle.Bold;
        photoText.DOColor(Highlightcolor, 0);
    }

    public void NormalPhotoText()
    {
        photoText.fontStyle = FontStyle.Normal;
        photoText.DOColor(Normalcolor, 0);
    }

    public void HighlightVideoText()
    {
        videoText.fontStyle = FontStyle.Bold;
        videoText.DOColor(Highlightcolor, 0);
    }

    public void NormalVideoText()
    {
        videoText.fontStyle = FontStyle.Normal;
        videoText.DOColor(Normalcolor, 0);
    }

    #endregion
}
