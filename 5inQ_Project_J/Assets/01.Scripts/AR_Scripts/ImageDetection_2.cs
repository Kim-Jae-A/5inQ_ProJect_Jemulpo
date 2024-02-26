using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageDetection_2 : MonoBehaviour
{
    ARTrackedImageManager trackedImageManager;
    [SerializeField]private List<GameObject> _prefabs = new List<GameObject>();
    Dictionary<string, GameObject> _PrefabsDic = new Dictionary<string, GameObject>();
    private List<ARTrackedImage> _trackedImage = new List<ARTrackedImage>();
    private List<float> _trackedTimer = new List<float>();


    private void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        foreach(GameObject obj in _prefabs)
        {
            string prefab_name = obj.name;
            _PrefabsDic.Add(prefab_name, obj);
        }
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach(ARTrackedImage trackedImage in args.added)
        {
            if (!_trackedImage.Contains(trackedImage))
            {
                _trackedImage.Add(trackedImage);
                _trackedTimer.Add(0);
            }
        }

        foreach (ARTrackedImage trackedImage in args.updated)
        {
            if (!_trackedImage.Contains(trackedImage))
            {
                _trackedImage.Add(trackedImage);
                _trackedTimer.Add(0);
            }
            else
            {
                int imageNum = _trackedImage.IndexOf(trackedImage);
                _trackedTimer[imageNum] = 0;


            }
        }
    }
    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string Refname = trackedImage.referenceImage.name;
        GameObject Trackedobj = _PrefabsDic[Refname];
        Trackedobj.transform.position = trackedImage.transform.position-(Vector3.up * 0.1f);
        Trackedobj.transform.rotation = trackedImage.transform.rotation;
        Trackedobj.SetActive(true);


    }
}
