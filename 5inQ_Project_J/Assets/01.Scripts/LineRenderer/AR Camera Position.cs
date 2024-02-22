using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCameraPosition : MonoBehaviour
{
    private LinRendererManager lineRenderer;

    private void Awake()
    {
        lineRenderer = FindObjectOfType<LinRendererManager>(); // LinRendererManager 스크립트를 찾습니다.
        if (lineRenderer == null)
        {
            Debug.LogError("LinRendererManager script not found in the scene.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        UpdateARCameraPosition(); // 초기화할 때 한 번 호출하여 AR 카메라의 위치를 설정합니다.
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateARCameraPosition()
    {
        if (lineRenderer != null)
        {
            // LinRendererManager에서 AR 카메라의 위치를 가져옵니다.
            Vector3 arCameraPosition = lineRenderer.ARCameraPosition;
            Vector3 direction = arCameraPosition - transform.position;

            // 방향 벡터를 회전하여 타겟 방향으로 오브젝트를 회전시킵니다.
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // y 값을 5로 설정합니다.
            arCameraPosition.y = 1f;

            // 이 오브젝트의 로컬 위치로 설정합니다.
            transform.rotation = targetRotation;
            transform.localPosition = arCameraPosition;
            
        }
    }
}
