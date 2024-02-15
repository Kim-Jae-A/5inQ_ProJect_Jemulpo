using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIRayCaster : MonoBehaviour
{
    [SerializeField] private Image photozone;
    [SerializeField] private Image docent;
    [SerializeField] private GameObject ListPage;
    [SerializeField] private ScrollRect ListPage_Change_Content;
    private Color photozone_Color;
    private Color docent_Color;

    private ScrollViewManager scrollViewManager;

    [SerializeField] private string AR_InfoScene;
    [SerializeField] private string PreviousScene;
    private void Start()
    {
        photozone_Color = photozone.color;
        docent_Color = docent.color;
        photozone_Color.a = 1f;
        docent_Color.a = 0f;
        photozone.color = photozone_Color;
        docent.color = docent_Color;
        ListPage_Change_Content.content = ListPage.GetComponent<RectTransform>();
        scrollViewManager = GetComponentInChildren<ScrollViewManager>();
    }
 
    private void Update()
    {   
        if (Input.GetMouseButtonDown(0))
        {
            GraphicRaycaster raycaster = GetComponentInParent<GraphicRaycaster>();

            if(raycaster != null)
            {
                PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
                pointerEventData.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                raycaster.Raycast(pointerEventData, results);
                foreach(RaycastResult result in results)
                {
                    if (result.gameObject.CompareTag("PhotozoneButton"))
                    {
                        Debug.Log("포토존 버튼 선택.");
                        photozone_Color.a = 1f;
                        docent_Color.a = 0f;
                        photozone.color = photozone_Color;
                        docent.color = docent_Color;
                        scrollViewManager.OnPhotozoneButtonClicked();
                    }

                    else if (result.gameObject.CompareTag("DocentButton"))
                    {
                        Debug.Log("도슨트 버튼 선택");
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
    }

    public void OnARList_ElementClicked()
    {
        SceneManager.LoadScene(AR_InfoScene);
    }
    public void OnReturnButton()
    {
        SceneManager.LoadScene(PreviousScene);
    }
}
