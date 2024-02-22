using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCameraPosition : MonoBehaviour
{
    private LinRendererManager lineRenderer;

    private void Awake()
    {
        lineRenderer = FindObjectOfType<LinRendererManager>(); // LinRendererManager ��ũ��Ʈ�� ã���ϴ�.
        if (lineRenderer == null)
        {
            Debug.LogError("LinRendererManager script not found in the scene.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateARCameraPosition(); // �ʱ�ȭ�� �� �� �� ȣ���Ͽ� AR ī�޶��� ��ġ�� �����մϴ�.
    }

    // Update is called once per frame
    void Update()
    {
        UpdateARCameraPosition(); // �� �����Ӹ��� AR ī�޶��� ��ġ�� ������Ʈ�մϴ�.
    }

    void UpdateARCameraPosition()
    {
        if (lineRenderer != null)
        {
            // LinRendererManager���� AR ī�޶��� ��ġ�� �����ɴϴ�.
            Vector3 arCameraPosition = lineRenderer.ARCameraPosition;

            // y ���� 5�� �����մϴ�.
            arCameraPosition.y = 5f;

            // �� ������Ʈ�� ���� ��ġ�� �����մϴ�.
            transform.localPosition = arCameraPosition;
        }
    }
}
