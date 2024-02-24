using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class PhotoZoneUI_Btn : MonoBehaviour
{
    [Header("촬영 모드")]
    [SerializeField] Text photoText;
    [SerializeField] Text videoText;

    [SerializeField] private GameObject CameraBtn; 
    [SerializeField] private GameObject RecordBtn; 
    [SerializeField] private GameObject RecordDoneBtn;


    Color Highlightcolor = new Color(0, 0.6f, 1);
    Color Normalcolor = new Color(0.5f, 0.5f, 0.5f, 0.8f);

    void Start()
    {
        HighlightPhotoText();
        NormalVideoText();
        CameraBtn.SetActive(true);
        RecordBtn.SetActive(false);
        RecordDoneBtn.SetActive(false);

    }
    public void OnPhotoBtn()
    {
        CameraMode.isPhoto = true;
        CameraMode.isVideo = false;
        CameraMode.isRecord = false;
        CameraMode.isRecordDone = false;
        HighlightPhotoText();
        NormalVideoText();

        CameraBtn.SetActive(true);   
        RecordBtn.SetActive(false);
        RecordDoneBtn.SetActive(false);

        
    }

    public void OnVideoBtn()
    {
        CameraMode.isVideo = true;
        CameraMode.isPhoto = false;
        CameraMode.isRecord = false;
        CameraMode.isRecordDone = false;

        HighlightVideoText();
        NormalPhotoText();

        CameraBtn.SetActive(false);
        RecordBtn.SetActive(true);
        RecordDoneBtn.SetActive(false);
        
    }

    public void OnGalleryBtn()
    {
        // READ_EXTERNAL_STORAGE 권한이 이미 부여되었는지 확인
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.ExternalStorageRead))
        {
            // 권한이 없다면, 권한 요청
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.ExternalStorageRead);

            if (UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.ExternalStorageRead))
            {
                LoadGallery();
            }
        }
        else
        {
            LoadGallery();
        }
    }
    
    void LoadGallery()
    {
        AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = javaObject.Call<AndroidJavaObject>("getPackageManager");
        AndroidJavaObject intent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", "com.sec.android.gallery3d");
        javaObject.Call("startActivity", intent);
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
