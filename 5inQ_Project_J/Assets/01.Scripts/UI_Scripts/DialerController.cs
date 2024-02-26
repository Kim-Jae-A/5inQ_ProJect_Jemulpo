using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialerController : MonoBehaviour
{
    private const string phoneNumber = "112";

    public void OpenDialerApp()
    {
        /*
        // �ȵ���̵忡���� CALL_PHONE ������ �ʿ�
        if (Application.platform == RuntimePlatform.Android)
        {
            // CALL_PHONE ������ �ִ��� Ȯ��
            if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.CallPhone))
            {
                // ����ڿ��� ���� ��û
                UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.CallPhone);
                return;
            }
        }
        */
        // �ȵ���̵��� Intent�� ����Ͽ� ���̾󷯸� ������ ��û
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        string actionCall = intentClass.GetStatic<string>("ACTION_DIAL");

        // ����Ʈ ��ü ����
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent", actionCall);

        // ��ȭ��ȣ�� �����Ͽ� ���̾󷯿� ǥ��
        intentObject.Call<AndroidJavaObject>("setData", new AndroidJavaObject("android.net.Uri", "tel:" + phoneNumber));

        // ����Ƽ ��Ƽ��Ƽ�� �����ͼ� ����Ʈ�� ����
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        // ���� ������ ���� try-catch ��� �߰�
        try
        {
            currentActivity.Call("startActivity", intentObject);
            Debug.Log("���� �Ϸ�");
        }
        catch (System.Exception e)
        {
            Debug.LogError("���� ����: " + e.Message);
        }
        finally
        {
            // �ڿ� ����
            intentObject.Dispose();
        }
    }
}
