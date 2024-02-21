using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InfoUIController : MonoBehaviour
{
    [SerializeField] private Text Title; 
    [SerializeField] private Text Info_name;
    [SerializeField] private Text Info_Description;
    [SerializeField] private Image Info_Image;
    [SerializeField] private Button Shooting;

    [SerializeField] private string PreviousScene;


    private void Start()
    {
        AR_POI selectedARData = JsonDataHolder.Instance.GetSelectedARData();

        if (selectedARData != null)
        {
            Debug.Log("Selected Landmark: " + selectedARData.Name);
            Debug.Log("Selected LandmarkInfo: " + selectedARData.Info);
            Info_name.text = selectedARData.Name;
            Info_Description.text = selectedARData.Info;

            Sprite sprite = Resources.Load<Sprite>(selectedARData.imagepath);
            if (sprite != null)
            {
                Info_Image.sprite = sprite;
            }
            string selectedButton = PlayerPrefs.GetString("selectedButton", "Photozone");
            if(selectedButton == "Photozone") 
            {
                Title.text = "AR 포토존";
            }
            else if(selectedButton == "Docent")
            {
                Title.text = "AR 도슨트";
            }
        }
    }

    public void OnShootingButton()
    {
        string selectedButton_ChangeScene = PlayerPrefs.GetString("selectedButton", "Photozone");
        if(selectedButton_ChangeScene == "Photozone")
        {
            SceneManager.LoadScene("TakeAShot");
        }
        else if(selectedButton_ChangeScene == "Docent")
        {
            SceneManager.LoadScene("Docent_Animation");
        }
    }

    public void OnReturnButton()
    {
        SceneManager.LoadScene(PreviousScene);
    }
}
