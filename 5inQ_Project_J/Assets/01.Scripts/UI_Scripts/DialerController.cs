using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialerController : MonoBehaviour
{
    private const string phoneNumber = "112";

    public void OpenDialerApp()
    {
        /*
        // 안드로이드에서는 CALL_PHONE 권한이 필요
        if (Application.platform == RuntimePlatform.Android)
        {
            // CALL_PHONE 권한이 있는지 확인
            if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.CallPhone))
            {
                // 사용자에게 권한 요청
                UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.CallPhone);
                return;
            }
        }
        */
        // 안드로이드의 Intent를 사용하여 다이얼러를 열도록 요청
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        string actionCall = intentClass.GetStatic<string>("ACTION_DIAL");

        // 인텐트 객체 생성
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent", actionCall);

        // 전화번호를 설정하여 다이얼러에 표시
        intentObject.Call<AndroidJavaObject>("setData", new AndroidJavaObject("android.net.Uri", "tel:" + phoneNumber));

        // 유니티 액티비티를 가져와서 인텐트를 시작
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        // 연결 오류를 위한 try-catch 블록 추가
        try
        {
            currentActivity.Call("startActivity", intentObject);
            Debug.Log("연결 완료");
        }
        catch (System.Exception e)
        {
            Debug.LogError("연결 오류: " + e.Message);
        }
        finally
        {
            // 자원 해제
            intentObject.Dispose();
        }
    }
}
