using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CheckARSession : MonoBehaviour
{
    // ARSession을 참조할 변수 선언
    private ARSession arSession;

    private void Start()
    {
        // ARSession 컴포넌트를 찾아 할당
        arSession = FindObjectOfType<ARSession>();

        // ARSession이 없는 경우 경고 출력
        if (arSession == null)
        {
            Debug.LogWarning("ARSession이 씬에 없습니다. ARSession을 추가하거나 활성화하세요.");
            return;
        }

        // ARSession이 초기화되었는지 확인
        if (!arSession.subsystem.running)
        {
            Debug.LogWarning("ARSession이 초기화되지 않았습니다. ARSession을 시작하세요.");
            return;
        }

        // ARSession이 초기화되었고 실행 중인 경우, 여기에 접근하여 필요한 작업 수행
        Debug.Log("ARSession이 초기화되었습니다. 필요한 작업을 수행하세요.");
    }

    private void Update()
    {
        // ARSession이 null인 경우 반환
        if (arSession == null)
            return;

        // 여기에 ARSession에 대한 작업을 추가하세요.
    }
}
