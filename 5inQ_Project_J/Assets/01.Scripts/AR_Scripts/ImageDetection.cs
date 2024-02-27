using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageDetection : MonoBehaviour
{
    ARTrackedImageManager imageManager;

    void Awake()
    {
        imageManager = GetComponent<ARTrackedImageManager>();

        imageManager.trackedImagesChanged += OnImageTrackedEvent;
    }

    private void OnImageTrackedEvent(ARTrackedImagesChangedEventArgs args)
    {
        foreach(ARTrackedImage trackedImage in args.added)
        {
            //Reference Image Library�� ����
            string imageName = trackedImage.referenceImage.name;

            //�� ������Ʈ�� Resources���� �̸��� ���� �� �ҷ��� ����
            GameObject prefab = Resources.Load<GameObject>($"AR_Model/{imageName}");

            if(prefab != null) 
            {
                GameObject obj = Instantiate(prefab); //prefab����
                obj.transform.SetParent(trackedImage.transform); //�̹����� �ڽ����� ����
            }
        }


        foreach (ARTrackedImage trackedImage in args.updated)
        {
            if (trackedImage.trackingState == TrackingState.None)
            {
                if (trackedImage.transform.childCount > 0)
                {
                    trackedImage.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            else
            {
                if (trackedImage.transform.childCount > 0)
                {
                    //���� ��ġ, ȸ����
                    trackedImage.transform.GetChild(0).position = trackedImage.transform.position - (Vector3.up * 0.2f);
                    trackedImage.transform.GetChild(0).rotation = trackedImage.transform.rotation;
                    trackedImage.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }

        foreach (ARTrackedImage trackedImage in args.removed)
        {
            trackedImage.transform.GetChild(0).gameObject.SetActive(false);

        }
    }

}
