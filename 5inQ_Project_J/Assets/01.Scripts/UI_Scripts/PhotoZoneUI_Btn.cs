using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PhotoZoneUI_Btn : MonoBehaviour
{
    [Header("ÃÔ¿µ ¸ðµå")]
    [SerializeField] Text photoText;
    [SerializeField] Text videoText;
    [SerializeField] Image shotImage;
    [SerializeField] Sprite videoStartShot;
    [SerializeField] Sprite photoShot;


    Color Highlightcolor = new Color(0, 0.6f, 1);
    Color Normalcolor = new Color(0.5f, 0.5f, 0.5f, 0.8f);

    void Start()
    {
        if (CameraMode.isPhoto)
        {
            HighlightPhotoText();
        }

    }
    public void OnPhotoBtn()
    {
        CameraMode.isPhoto = true;
        CameraMode.isVideo = false;

        HighlightPhotoText();
        NormalVideoText();

        shotImage.sprite = photoShot;
    }

    public void OnVideoBtn()
    {
        CameraMode.isVideo = true;
        CameraMode.isPhoto = false;

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
