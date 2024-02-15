using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapUI_ColorChange : MonoBehaviour, IDeselectHandler
{
    Text text;
    Button button;
    Color buleColor;
    Color grayColor;

    private void Awake()
    {
        button = GetComponent<Button>();
        text = transform.GetComponentInChildren<Text>();
        ColorUtility.TryParseHtmlString("#0094FF", out buleColor);
        ColorUtility.TryParseHtmlString("#B9BCBE", out grayColor);

        button.onClick.AddListener(OnButtonEnter);
    }

    void OnButtonEnter()
    {
        text.color = buleColor;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        text.color = grayColor;
    }
}
