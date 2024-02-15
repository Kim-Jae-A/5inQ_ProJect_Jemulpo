using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class CameraCotroller : MonoBehaviour
{
    float speed = 10.0f;
    private Vector2 nowPos, prePos;
    private Vector3 movePos;
    public Text test;
    float m_fSpeed = 0.1f;       // ���� �ӵ��� �����մϴ� 
    float m_fFieldOfView = 60f;     // ī�޶��� FieldOfView�� �⺻���� 60���� ���մϴ�.

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
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
        else if(Input.touchCount == 2)
        {
            Vector2 vecPreTouchPos0 = Input.touches[0].position - Input.touches[0].deltaPosition;
            Vector2 vecPreTouchPos1 = Input.touches[1].position - Input.touches[1].deltaPosition;

            // ���� �� ��ġ�� ���� 
            float fPreDis = (vecPreTouchPos0 - vecPreTouchPos1).magnitude;
            // ���� �� ��ġ�� ��
            float fToucDis = (Input.touches[0].position - Input.touches[1].position).magnitude;


            // ���� �� ��ġ�� �Ÿ��� ���� �� ��ġ�� �Ÿ��� ����
            float fDis = fPreDis - fToucDis;

            // ���� �� ��ġ�� �Ÿ��� ���� �� ��ġ�� �Ÿ��� ���̸� FleldOfView�� �����մϴ�.
            m_fFieldOfView += (fDis * m_fSpeed);

            // �ִ�� 100, �ּҴ� 20���� ���̻� ���� Ȥ�� ���Ұ� ���� �ʵ��� �մϴ�.
            m_fFieldOfView = Mathf.Clamp(m_fFieldOfView, 20.0f, 100.0f);

            Camera.main.fieldOfView = m_fFieldOfView;
        }
    }
}
