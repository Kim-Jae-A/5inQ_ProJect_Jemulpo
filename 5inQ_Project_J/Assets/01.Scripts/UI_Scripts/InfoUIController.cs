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
        //JsonDataHolder�� ���� ������ �Ѿ�� �ν��Ͻ��� �����͸� �޾ƿ� 
        AR_POI selectedARData = JsonDataHolder.Instance.GetSelectedARData();

        //�޾ƿ� �����Ͱ� null�� �ƴ϶��
        if (selectedARData != null)
        {
            //�޾ƿ� json�����͵��� text�� �Ҵ�

            Info_name.text = selectedARData.Name;
            Info_Description.text = selectedARData.Info;
            _endlongitude = Convert.ToDouble(selectedARData.longitude);
            _endlatitude = Convert.ToDouble(selectedARData.latitude);


            //�̹��� ���� �޾ƿ� �������� �̹��� ��θ� �Ҵ�
            Sprite sprite = Resources.Load<Sprite>(selectedARData.imagepath);
            if (sprite != null)
            {
                //�޾ƿ� �̹��� �Ҵ�.
                Info_Image.sprite = sprite;
            }
            //Playerprefs�� ���� �����͸� �������� �󼼼���â�� Title�� ����
            string selectedButton = PlayerPrefs.GetString("selectedButton", "Photozone");
            if(selectedButton == "Photozone") 
            {
                Title.text = "AR ������";
            }
            else if(selectedButton == "Docent")
            {
                Title.text = "AR ����Ʈ";
            }
        }
    }

    //�Կ��ϱ� â���� �Ѿ(���⵵ �Կ��ϱ� ��ư�� ���� �󼼼���â���� �ٽ� ���ƿ��� ���� Playerprefs ����.)
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
