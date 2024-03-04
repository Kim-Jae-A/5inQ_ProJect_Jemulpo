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

    //토글이 선택되었을 때 리스트의 요소를 변경합니다. (선택된 요소의 상세설명창으로 이동하고 뒤로가기 버튼을 눌렀을 때의 리스트창의 정보를 저장하여 돌아왔을 때 유지.)
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
    
    public void OnReturnButton()
    {
        SceneManager.LoadScene(PreviousScene);
    }
}
