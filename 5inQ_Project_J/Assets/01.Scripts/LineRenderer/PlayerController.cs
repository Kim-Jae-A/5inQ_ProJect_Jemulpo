using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // �̵� �ӵ�

    void Update()
    {
        // ���� �Է� ����
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // �̵� ���� ���
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // �̵� �ӵ��� ������ ���Ͽ� �̵����� ���
        Vector3 moveAmount = moveDirection * moveSpeed * Time.deltaTime;

        // ���� ��ġ���� �̵����� ���Ͽ� ���ο� ��ġ ���
        transform.position += moveAmount;
    }
}
