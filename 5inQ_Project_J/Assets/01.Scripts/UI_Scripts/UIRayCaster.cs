using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIRayCaster : MonoBehaviour
{
    [SerializeField] private Image photozone;
    [SerializeField] private Image docent;
    [SerializeField] private GameObject photozone_ListPage;
    [SerializeField] private GameObject docent_ListPage;
    [SerializeField] private ScrollRect ListPage_Change_Content;
    private Color photozone_Color;
    private Color docent_Color;

    private void Start()
    {
        photozone_Color = photozone.color;
        docent_Color = docent.color;
        photozone_Color.a = 1f;
        docent_Color.a = 0f;
        photozone.color = photozone_Color;
        docent.color = docent_Color;
        photozone_ListPage.SetActive(true);
        docent_ListPage.SetActive(false);
        ListPage_Change_Content.content = photozone_ListPage.GetComponent<RectTransform>();    
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
                        photozone_ListPage.SetActive(true);
                        docent_ListPage.SetActive(false);
                        ListPage_Change_Content.content = photozone_ListPage.GetComponent<RectTransform>();
                    }

                    else if (result.gameObject.CompareTag("DocentButton"))
                    {
                        Debug.Log("도슨트 버튼 선택");
                        photozone_Color.a = 0f;
                        docent_Color.a = 1f;
                        photozone.color = photozone_Color;
                        docent.color = docent_Color;
                        photozone_ListPage.SetActive(false);
                        docent_ListPage.SetActive(true);
                        ListPage_Change_Content.content = docent_ListPage.GetComponent<RectTransform>();
                    }
                }
            }
        }
    }
}
