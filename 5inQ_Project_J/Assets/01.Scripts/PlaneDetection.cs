using UnityEngine;

public class TouchController : MonoBehaviour
{
    public GameObject objectToPlace;

    // Single touch 관련 변수
    private bool isTouching = false;
    private Vector2 touchPosition;

    // Multi touch 관련 변수
    private Vector2[] previousTouches = new Vector2[2];
    private float previousDistance;

    void Update()
    {
        // Single touch 기능
        HandleSingleTouch();

        // Multi touch 기능
        HandleMultiTouch();
    }

    void HandleSingleTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isTouching = true;
                touchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                touchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isTouching = false;
            }
        }

        if (isTouching)
        {
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3 positionToPlace = hit.point;
                Instantiate(objectToPlace, positionToPlace, Quaternion.identity);
            }
        }
    }

    void HandleMultiTouch()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
                Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;

                float previousTouchDeltaMag = (touch1PrevPos - touch2PrevPos).magnitude;
                float touchDeltaMag = (touch1.position - touch2.position).magnitude;

                float deltaMagnitudeDiff = previousTouchDeltaMag - touchDeltaMag;

                float scaleFactor = deltaMagnitudeDiff * 0.01f; // 조절 가능한 스케일링 팩터 조절

                // 오브젝트 크기 조절
                Vector3 newScale = objectToPlace.transform.localScale + new Vector3(scaleFactor, scaleFactor, scaleFactor);
                objectToPlace.transform.localScale = newScale;
            }
        }
    }
}
