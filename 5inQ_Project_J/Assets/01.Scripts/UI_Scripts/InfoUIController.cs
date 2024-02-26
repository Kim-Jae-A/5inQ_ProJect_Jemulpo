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
        //JsonDataHolder�� ���� ������ �Ѿ�� �ν��Ͻ��� �����͸� �޾ƿ� 
        AR_POI selectedARData = JsonDataHolder.Instance.GetSelectedARData();

        //�޾ƿ� �����Ͱ� null�� �ƴ϶��
        if (selectedARData != null)
        {
            //�޾ƿ� json�����͵��� text�� �Ҵ�
            Info_name.text = selectedARData.Name;
            Info_Description.text = selectedARData.Info;

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
        SceneManager.LoadScene(PreviousScene);
    }
}
