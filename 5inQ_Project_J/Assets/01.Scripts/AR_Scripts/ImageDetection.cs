using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageDetection : MonoBehaviour
{
    ARTrackedImageManager imageManager;
    private Vector3 PrefabOffset;

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

            GameObject prefab = Resources.Load<GameObject>(imageName);

            if(prefab != null)
            {
                GameObject obj = Instantiate(prefab);
                obj.transform.SetParent(trackedImage.transform);
                obj.transform.Rotate(new Vector3(0, 90, -90));
                CatAnimatorController catani = obj.GetComponent<CatAnimatorController>();
                catani.trackedImage = trackedImage;
                catani.isMoving = true; 
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
                    trackedImage.transform.GetChild(0).position = trackedImage.transform.position;
                    trackedImage.transform.GetChild(0).rotation = trackedImage.transform.rotation;
                    trackedImage.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            /*if (trackedImage.transform.childCount > 0)
            {
                trackedImage.transform.GetChild(0).position = trackedImage.transform.position;
                trackedImage.transform.GetChild(0).rotation = trackedImage.transform.rotation;
                trackedImage.transform.GetChild(0).gameObject.SetActive(true);
            }*/
        }

        foreach (ARTrackedImage trackedImage in args.removed)
        {
            if (trackedImage.transform.childCount > 0)
            {
                Destroy(trackedImage.transform.GetChild(0).gameObject);
            }
        }
    }

    private void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnImageTrackedEvent;
    }
}
