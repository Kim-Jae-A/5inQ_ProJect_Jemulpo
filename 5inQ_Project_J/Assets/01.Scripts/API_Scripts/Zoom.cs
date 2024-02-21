using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    [SerializeField] private RectTransform _zoomTargetRt;

    private readonly float _ZOOM_IN_MAX = 16f;
    private readonly float _ZOOM_OUT_MAX = 0.3f;
    private readonly float _ZOOM_SPEED = 1.5f;

    float speed = 10.0f;
    private Vector2 nowPos, prePos;
    private Vector3 movePos;

    private bool _isZooming = false;

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            _isZooming = false;
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                prePos = touch.position - touch.deltaPosition;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                nowPos = touch.position - touch.deltaPosition;
                movePos = Time.deltaTime * speed * (Vector3)(prePos - nowPos);
                transform.Translate(movePos);
                prePos = touch.position - touch.deltaPosition;
            }
        }
        else if (Input.touchCount == 2)
        {
            ZoomAndPan();
        }
        
    }

    private void ZoomAndPan()
    {
        if (_isZooming == false)
        {
            _isZooming = true;
        }

        /* get zoomAmount */
        var prevTouchAPos = Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition;
        var prevTouchBPos = Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition;
        var curTouchAPos = Input.GetTouch(0).position;
        var curTouchBPos = Input.GetTouch(1).position;
        var deltaDistance =
            Vector2.Distance(Normalize(curTouchAPos), Normalize(curTouchBPos))
            - Vector2.Distance(Normalize(prevTouchAPos), Normalize(prevTouchBPos));
        var currentScale = _zoomTargetRt.localScale.x;
        var zoomAmount = deltaDistance * currentScale * _ZOOM_SPEED; // zoomAmount == deltaScale

        /* clamp & zoom */
        var zoomedScale = currentScale + zoomAmount;
        if (zoomedScale < _ZOOM_OUT_MAX)
        {
            zoomedScale = _ZOOM_OUT_MAX;
            zoomAmount = 0f;
        }
        if (_ZOOM_IN_MAX < zoomedScale)
        {
            zoomedScale = _ZOOM_IN_MAX;
            zoomAmount = 0f;
        }
        _zoomTargetRt.localScale = zoomedScale * Vector3.one;

        /* apply offset */
        // offset is a value against movement caused by scale up & down
        var pivotPos = _zoomTargetRt.anchoredPosition;
        var fromCenterToInputPos = new Vector2(
                Input.mousePosition.x - Screen.width * 0.5f,
                Input.mousePosition.y - Screen.height * 0.5f);
        var fromPivotToInputPos = fromCenterToInputPos - pivotPos;
        var offsetX = (fromPivotToInputPos.x / zoomedScale) * zoomAmount;
        var offsetY = (fromPivotToInputPos.y / zoomedScale) * zoomAmount;
        _zoomTargetRt.anchoredPosition -= new Vector2(offsetX, offsetY);

        /* get moveAmount */
        var deltaPosTouchA = Input.GetTouch(0).deltaPosition;
        var deltaPosTouchB = Input.GetTouch(1).deltaPosition;
        var deltaPosTotal = (deltaPosTouchA + deltaPosTouchB) * 0.5f;
        var moveAmount = new Vector2(deltaPosTotal.x, deltaPosTotal.y);

        /* clamp & pan */
        var clampX = (Screen.width * zoomedScale - Screen.width) * 0.5f;
        var clampY = (Screen.height * zoomedScale - Screen.height) * 0.5f;
        var clampedPosX = Mathf.Clamp(_zoomTargetRt.localPosition.x + moveAmount.x, -clampX, clampX);
        var clampedPosY = Mathf.Clamp(_zoomTargetRt.localPosition.y + moveAmount.y, -clampY, clampY);
        _zoomTargetRt.anchoredPosition = new Vector3(clampedPosX, clampedPosY);
    }

    private Vector2 Normalize(Vector2 position)
    {
        var normlizedPos = new Vector2(
            (position.x - Screen.width * 0.5f) / (Screen.width * 0.5f),
            (position.y - Screen.height * 0.5f) / (Screen.height * 0.5f));
        return normlizedPos;
    }
}
