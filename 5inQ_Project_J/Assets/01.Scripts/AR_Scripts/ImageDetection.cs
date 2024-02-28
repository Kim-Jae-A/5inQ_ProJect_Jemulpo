using UnityEngine;
using UnityEngine.UI;
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
            if (trackedImage.trackingState == TrackingState.None) //�̹����� �� �̻� �������� �ʴ� ���
            {
                if (trackedImage.transform.childCount > 0) //�ڽ� ������Ʈ�� �����ϸ�
                {
                    Destroy(trackedImage.transform.GetChild(0));//�ı�
                }
            }
            else//�̹����� �����Ǵ� ���
            {
                if (trackedImage.transform.childCount > 0)//�ڽ� ������Ʈ�� �����ϸ�
                {
                    //���� ��ġ, ȸ����
                    trackedImage.transform.GetChild(0).position = trackedImage.transform.position - (Vector3.up * 0.1f);
                    trackedImage.transform.GetChild(0).rotation = trackedImage.transform.rotation;
                }
            }
        }
    }

}
