using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도

    void Update()
    {
        // 방향 입력 감지
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 이동 방향 계산
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // 이동 속도와 방향을 곱하여 이동량을 계산
        Vector3 moveAmount = moveDirection * moveSpeed * Time.deltaTime;

        // 현재 위치에서 이동량을 더하여 새로운 위치 계산
        transform.position += moveAmount;
    }
}
