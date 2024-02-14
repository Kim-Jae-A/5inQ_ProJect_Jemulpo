using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoUIController : MonoBehaviour
{
    [SerializeField] private Text Info_name;
    [SerializeField] private Text Info_Description;
    [SerializeField] private Image Info_Image;

    private void Start()
    {
        AR_POI selectedARData = JsonDataHolder.Instance.GetSelectedARData();

        if(selectedARData != null)
        {
            Debug.Log("Selected Landmark: " + selectedARData.Landmark);
            Debug.Log("Selected LandmarkInfo: " + selectedARData.LandmarkInfo);
            Info_name.text = selectedARData.Landmark;
            Info_Description.text = selectedARData.LandmarkInfo;

            Sprite sprite = Resources.Load<Sprite>(selectedARData.imagepath);
            if(sprite != null)
            {
                Info_Image.sprite = sprite;
            }
        }
    }
}
