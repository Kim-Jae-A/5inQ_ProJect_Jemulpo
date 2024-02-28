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

    void Start()
    {
        ResetTrackedImages();
    }


    public void ResetTrackedImages()
    {
        foreach (ARTrackedImage trackedImage in imageManager.trackables)
        {
            Destroy(trackedImage.transform.GetChild(0)?.gameObject);
        }
    }


    private void OnImageTrackedEvent(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage trackedImage in args.added)
        {
            //Reference Image Library�� ����
            string imageName = trackedImage.referenceImage.name;

            //�� ������Ʈ�� Resources���� �̸��� ���� �� �ҷ��� ����
            GameObject prefab = Resources.Load<GameObject>($"AR_Model/{imageName}");

            if (prefab != null)
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
                    Destroy(trackedImage.transform.GetChild(0));
                }
            }
            else
            {
                if (trackedImage.transform.childCount > 0)
                {
                    //���� ��ġ, ȸ����
                    trackedImage.transform.GetChild(0).position = trackedImage.transform.position - (Vector3.up * 0.2f);
                    trackedImage.transform.GetChild(0).rotation = trackedImage.transform.rotation;
                }
            }
        }

        foreach (ARTrackedImage trackedImage in args.removed)
        {
            Destroy(trackedImage.transform.GetChild(0));

        }


    }

}
