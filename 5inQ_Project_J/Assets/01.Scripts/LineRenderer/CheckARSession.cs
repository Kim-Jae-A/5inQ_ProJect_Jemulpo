using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CheckARSession : MonoBehaviour
{
    // ARSession�� ������ ���� ����
    private ARSession arSession;

    private void Start()
    {
        // ARSession ������Ʈ�� ã�� �Ҵ�
        arSession = FindObjectOfType<ARSession>();

        // ARSession�� ���� ��� ��� ���
        if (arSession == null)
        {
            Debug.LogWarning("ARSession�� ���� �����ϴ�. ARSession�� �߰��ϰų� Ȱ��ȭ�ϼ���.");
            return;
        }

        // ARSession�� �ʱ�ȭ�Ǿ����� Ȯ��
        if (!arSession.subsystem.running)
        {
            Debug.LogWarning("ARSession�� �ʱ�ȭ���� �ʾҽ��ϴ�. ARSession�� �����ϼ���.");
            return;
        }

        // ARSession�� �ʱ�ȭ�Ǿ��� ���� ���� ���, ���⿡ �����Ͽ� �ʿ��� �۾� ����
        Debug.Log("ARSession�� �ʱ�ȭ�Ǿ����ϴ�. �ʿ��� �۾��� �����ϼ���.");
    }

    private void Update()
    {
        // ARSession�� null�� ��� ��ȯ
        if (arSession == null)
            return;

        // ���⿡ ARSession�� ���� �۾��� �߰��ϼ���.
    }
}
