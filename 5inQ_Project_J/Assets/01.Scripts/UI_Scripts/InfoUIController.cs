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

    public Image mainPanel;
    public Image naviPanel;
    public GameObject lineObj;

    [SerializeField] private string PreviousScene;
    private double _endlongitude;
    private double _endlatitude;


    private void Start()
    {
        //JsonDataHolder에 이전 씬에서 넘어온 인스턴스의 데이터를 받아옴 
        AR_POI selectedARData = JsonDataHolder.Instance.GetSelectedARData();

        //받아온 데이터가 null이 아니라면
        if (selectedARData != null)
        {
            //받아온 json데이터들을 text에 할당

            Info_name.text = selectedARData.Name;
            Info_Description.text = selectedARData.Info;
            _endlongitude = Convert.ToDouble(selectedARData.longitude);
            _endlatitude = Convert.ToDouble(selectedARData.latitude);


            //이미지 또한 받아온 데이터의 이미지 경로를 할당
            Sprite sprite = Resources.Load<Sprite>(selectedARData.imagepath);
            if (sprite != null)
            {
                //받아온 이미지 할당.
                Info_Image.sprite = sprite;
            }
            //Playerprefs로 받은 데이터를 바탕으로 상세설명창의 Title을 설정
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

    //촬영하기 창으로 넘어감(여기도 촬영하기 버튼을 누른 상세설명창으로 다시 돌아오기 위해 Playerprefs 설정.)
    public void OnShootingButton()
    {
        string selectedButton_ChangeScene = PlayerPrefs.GetString("selectedButton", "Photozone");
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("PreviousScene", currentScene);
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
        if (mainPanel.gameObject.activeSelf)
        {
            SceneManager.LoadScene(PreviousScene);
            return;
        }
        else if (naviPanel.gameObject.activeSelf)
        {
            mainPanel.gameObject.SetActive(true);
            if (lineObj.transform.childCount > 0)
            {
                for (int i = 0; i < lineObj.transform.childCount; i++)
                {
                    Destroy(lineObj.transform.GetChild(i).gameObject);
                }
                lineObj.GetComponent<LineRenderer>().positionCount = 0;
            }
            naviPanel.gameObject.SetActive(false);           
        }
    }
    public void DirectionButton()
    {
        mainPanel.gameObject.SetActive(false);
        naviPanel.gameObject.SetActive(true);
        //photo_Docent_static.DrawingStart();
        Photo_Docent_Static.instance.DrawingStart();
        Photo_Docent_Direction.instance.DirectionStart(_endlongitude, _endlatitude);
        
    }
}
