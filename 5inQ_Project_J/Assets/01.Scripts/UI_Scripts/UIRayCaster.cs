using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIRayCaster : MonoBehaviour
{

    [Header("����Ʈ ������ ���� ��ư")]
    [SerializeField] private Toggle photozone;
    [SerializeField] private Toggle docent;
    [SerializeField] private ToggleGroup toggleGroup;
    private ScrollViewManager scrollViewManager;

    [SerializeField] private string AR_InfoScene;
    [SerializeField] private string PreviousScene;

    //�������� ����Ʈ ����Ʈ â���� �������� ����Ʈ�� ������ ���� �ڵ��Դϴ�. Playerprefs�� ����Ͽ� ��� ��ư�� ���õǾ����� ������ �޾ƿ� �����Ͽ� ���ÿ� ��ư�� ���õǴ� ���� �����մϴ�.
    private void Start()
    {
        string selectedButton = PlayerPrefs.GetString("selectedButton", "Photozone");
        if(selectedButton == "Photozone")
        {
            photozone.isOn = true;
            docent.isOn = false;
        }
        else if(selectedButton == "Docent")
        {
            photozone.isOn = false;
            docent.isOn = true;
        }

       
        //ListPage_Change_Content.content = ListPage.GetComponent<RectTransform>();
        scrollViewManager = GetComponentInChildren<ScrollViewManager>();
        
        photozone.onValueChanged.AddListener(OnToggleValueChanged);
        docent.onValueChanged.AddListener(OnToggleValueChanged);
    }

    //����� ���õǾ��� �� ����Ʈ�� ��Ҹ� �����մϴ�. (���õ� ����� �󼼼���â���� �̵��ϰ� �ڷΰ��� ��ư�� ������ ���� ����Ʈâ�� ������ �����Ͽ� ���ƿ��� �� ����.)
    public void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            if (photozone.isOn)
            {
                Debug.Log("������ ��� ����.");
                PlayerPrefs.SetString("selectedButton", "Photozone");
                scrollViewManager.OnPhotozoneButtonClicked();
            }
            else if (docent.isOn)
            {
                Debug.Log("����Ʈ ��� ����");
                PlayerPrefs.SetString("selectedButton", "Docent");
                scrollViewManager.OnDocentButtonClicked();
            }
        }
    }
    
    public void OnReturnButton()
    {
        SceneManager.LoadScene(PreviousScene);
    }
}
