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

    [Header("도슨트 포토존 상태 버튼")]
    [SerializeField] private Toggle photozone;
    [SerializeField] private Toggle docent;
    [SerializeField] private ToggleGroup toggleGroup;

    [Header("스크롤뷰")]
    [SerializeField] private GameObject ListPage;
    [SerializeField] private ScrollRect ListPage_Change_Content;

    private ScrollViewManager scrollViewManager;

    [SerializeField] private string AR_InfoScene;
    [SerializeField] private string PreviousScene;

    //포토존과 도슨트 리스트 창에서 포토존과 리스트의 구분을 위한 코드입니다. Playerprefs를 사용하여 어느 버튼이 선택되었는지 정보를 받아와 저장하여 동시에 버튼이 선택되는 것을 방지합니다.
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
    //토글이 선택되었을 때 리스트의 요소를 변경합니다. 
    public void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            if (photozone.isOn)
            {
                Debug.Log("포토존 토글 선택.");
                PlayerPrefs.SetString("selectedButton", "Photozone");
                scrollViewManager.OnPhotozoneButtonClicked();
            }
            else if (docent.isOn)
            {
                Debug.Log("도슨트 토글 선택");
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
                        Debug.Log("포토존 버튼 선택.");
                        PlayerPrefs.SetString("selectedButton", "Photozone");
                        photozone_Color.a = 1f;
                        docent_Color.a = 0f;
                        photozone.color = photozone_Color;
                        docent.color = docent_Color;
                        scrollViewManager.OnPhotozoneButtonClicked();
                    }

                    else if (result.gameObject.CompareTag("DocentButton"))
                    {
                        Debug.Log("도슨트 버튼 선택");
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
                        //이 왜 진?
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

    //리스트의 요소를 클릭했을 때 클릭한 요소의 정보창으로 이동, 뒤로가기 버튼.
    public void OnARList_ElementClicked()
    {
        SceneManager.LoadScene(AR_InfoScene);
    }
    public void OnReturnButton()
    {
        SceneManager.LoadScene(PreviousScene);
    }
}
