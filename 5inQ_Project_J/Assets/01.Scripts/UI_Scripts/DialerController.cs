using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialerController : MonoBehaviour
{
    public void DialNumber()
    {
        // �ȵ���̵� �÷��������� ����

        try
        {
            using (var unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                var currentActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
                var intentClass = new AndroidJavaClass("android.content.Intent");
                var actionDial = intentClass.GetStatic<string>("ACTION_DIAL");
                var intentObject = new AndroidJavaObject("android.content.Intent", actionDial);

                // ��ȭ��ȣ ����
                intentObject.Call<AndroidJavaObject>("setData", new AndroidJavaClass("android.net.Uri").CallStatic<AndroidJavaObject>("parse", "tel:112"));

                currentActivity.Call("startActivity", intentObject);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("���̾� ���� �Ұ�" + e.Message);
        }

    }
}
