using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TakeAShot : MonoBehaviour
{
    [Header("버튼 이미지")]
    [SerializeField] Image shotImage;
    [SerializeField] Sprite videoStopShot;

    [Header("비디오 모드 버튼")]
    [SerializeField] GameObject videoStartBtn;
    [SerializeField] GameObject videoStopBtn;

    void Start()
    {
        videoStartBtn.SetActive(true);
        videoStopBtn.SetActive(false);
    }

    public void OnShotBtn()
    {
        StartCoroutine(ScreenShot());
    }

    IEnumerator ScreenShot()
    {
        yield return new WaitForEndOfFrame();

        if (CameraMode.isPhoto)
        {
            // 캡처된 화면을 Texture2D로 생성한다
            Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            tex.Apply();

            // 캡처된 화면을 PNG 형식의 byte 배열로 변환한다.
            byte[] bytes = tex.EncodeToPNG();
            Destroy(tex);

            // byte 배열을 PNG 파일로 저장한다.
            string fileName = "ImageName";
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            File.WriteAllBytes(filePath, bytes);

            // "SavePhoto" 씬으로 이동한다.
            SceneManager.LoadScene("SavePhoto");
        }
    }

    //비디오 시작 버튼을 눌렀을 때
    public void OnVideoStartBtn()
    {
        videoStartBtn.SetActive(false);
        videoStopBtn.SetActive(true);
        //비디오 모드면
        if (CameraMode.isVideo)
        {
            CameraMode.isVideo = false;
            CameraMode.isRecord = true;
        }
    }

    //비디오 촬영을 끝내는 버튼을 눌렀을 때
    public void OnRecordDoneBtn()
    {
        //촬영 중이면
        if (CameraMode.isRecord)
        {
            SceneManager.LoadScene("SavePhoto");
        }
    }







}






