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

    }

    void UpdateARCameraPosition()
    {
        if (lineRenderer != null)
        {
            // LinRendererManager���� AR ī�޶��� ��ġ�� �����ɴϴ�.
            Vector3 arCameraPosition = lineRenderer.ARCameraPosition;
            Vector3 direction = arCameraPosition - transform.position;

            // ���� ���͸� ȸ���Ͽ� Ÿ�� �������� ������Ʈ�� ȸ����ŵ�ϴ�.
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // y ���� 5�� �����մϴ�.
            arCameraPosition.y = 1f;

            // �� ������Ʈ�� ���� ��ġ�� �����մϴ�.
            transform.rotation = targetRotation;
            transform.localPosition = arCameraPosition;
            
        }
    }
}
