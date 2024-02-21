using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using ColorUtility = UnityEngine.ColorUtility;

public class MapUI_ColorChange : MonoBehaviour
{
    Text _text;
    public Button[] button;
    public GameObject _Maps;
    MapUI_Enum[] _Enum;
    Button selectedButton;
    Color buleColor;
    Color grayColor;

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#0094FF", out buleColor);
        ColorUtility.TryParseHtmlString("#B9BCBE", out grayColor);
        _Enum = _Maps.GetComponentsInChildren<MapUI_Enum>();

        foreach (Button btn in button)
        {
            btn.onClick.AddListener(() => SelectButton(btn));
        }
    }
    void SelectButton(Button button)
    {
        if (selectedButton != null)
        {
            // 이전에 선택된 버튼의 색상을 기본 색상으로 변경
            selectedButton.GetComponent<Image>().color = grayColor;
            _text = selectedButton.GetComponentInChildren<Text>();
            _text.color = grayColor;
            EnumSetActive((int)selectedButton.GetComponent<MakerManager>().type);
        }

        // 현재 선택된 버튼의 색상을 선택 색상으로 변경
        button.GetComponent<Image>().color = buleColor;
        _text = button.GetComponentInChildren<Text>();
        _text.color = buleColor;
        EnumSetActive((int)button.GetComponent<MakerManager>().type);

        // 선택된 버튼 업데이트
        selectedButton = button;
    }

    void EnumSetActive(int num)
    {
        switch (num)
        {
            case 0:
                foreach (MapUI_Enum m in _Enum)
                {
                    m.gameObject.SetActive(true);
                }
                break;         
            case 1:
                foreach (MapUI_Enum m in _Enum)
                {
                    if ((int)m.type == 1)
                    {
                        m.gameObject.SetActive(true);
                    }
                    else
                    {
                        m.gameObject.SetActive(false);
                    }
                }
                break; 
            case 2:
                foreach (MapUI_Enum m in _Enum)
                {
                    if ((int)m.type == 2)
                    {
                        m.gameObject.SetActive(true);
                    }
                    else
                    {
                        m.gameObject.SetActive(false);
                    }
                }
                break;
            case 3:
                foreach (MapUI_Enum m in _Enum)
                {
                    if ((int)m.type == 3)
                    {
                        m.gameObject.SetActive(true);
                    }
                    else if ((int)m.type == 4)
                    {
                        m.gameObject.SetActive(true);
                    }
                    else 
                    { 
                        m.gameObject.SetActive(false); 
                    }
                }
                break;
        }
    }
}
