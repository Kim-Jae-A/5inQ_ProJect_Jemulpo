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

    [Header("��ũ�Ѻ�")]
    [SerializeField] private GameObject ListPage;
    [SerializeField] private ScrollRect ListPage_Change_Content;

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
    //����� ���õǾ��� �� ����Ʈ�� ��Ҹ� �����մϴ�. 
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
    

    /*private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GraphicRaycaster raycaster = GetComponentInParent<GraphicRaycaster>();

            if (raycaster != null)
            {
                PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
                pointerEventData.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                raycaster.Raycast(pointerEventData, results);
                foreach (RaycastResult result in results)
                {
                    if (result.gameObject.CompareTag("PhotozoneButton"))
                    {
                        Debug.Log("������ ��ư ����.");
                        PlayerPrefs.SetString("selectedButton", "Photozone");
                        photozone_Color.a = 1f;
                        docent_Color.a = 0f;
                        photozone.color = photozone_Color;
                        docent.color = docent_Color;
                        scrollViewManager.OnPhotozoneButtonClicked();
                    }

                    else if (result.gameObject.CompareTag("DocentButton"))
                    {
                        Debug.Log("����Ʈ ��ư ����");
                        PlayerPrefs.SetString("selectedButton", "Docent");
                        photozone_Color.a = 0f;
                        docent_Color.a = 1f;
                        photozone.color = photozone_Color;
                        docent.color = docent_Color;
                        scrollViewManager.OnDocentButtonClicked();
                    }
                    else if (result.gameObject.CompareTag("Element"))
                    {
                        AR_POI selectedData = result.gameObject.GetComponent<SaveListElementData>().GetARJsonData();
                        //�� �� ��?
                        if (selectedData == null)
                        {
                            //Debug.Log("selectedData is null.");
                            return;
                        }
                        JsonDataHolder.Instance.SetSelectedARData(selectedData);
                        OnARList_ElementClicked();
                    }
                }
            }
        }
    }*/

    //����Ʈ�� ��Ҹ� Ŭ������ �� Ŭ���� ����� ����â���� �̵�, �ڷΰ��� ��ư.
    public void OnARList_ElementClicked()
    {
        SceneManager.LoadScene(AR_InfoScene);
    }
    public void OnReturnButton()
    {
        SceneManager.LoadScene(PreviousScene);
    }
}
