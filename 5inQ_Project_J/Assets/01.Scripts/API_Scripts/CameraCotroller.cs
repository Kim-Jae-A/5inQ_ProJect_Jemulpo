using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class CameraCotroller : MonoBehaviour
{
    float speed = 10.0f;
    private Vector2 nowPos, prePos;
    private Vector3 movePos;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
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
    }
}
