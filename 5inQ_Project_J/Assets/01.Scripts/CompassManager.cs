using UnityEngine;
using UnityEngine.UI;

public class CompassExample : MonoBehaviour
{
    public Image arrowImage; // 이미지
    public Text compassText; // 각도 텍스트
    public float smoothFactor = 5f; // 부드러운 회전을 위한 보간 계수

    private Quaternion targetRotation; // 목표 회전 각도

    void Start()
    {
        // Compass 센서가 활성화되어 있는지 확인
        if (!Input.compass.enabled)
        {
            // 나침반 센서 활성화
            Input.compass.enabled = true;

            // InvokeRepeating을 사용하여 UpdateCompass 함수를 주기적으로 호출
            InvokeRepeating("UpdateCompass", 0f, 0.1f);
        }
        else
        {
            // Compass 센서가 이미 활성화된 경우 에러 메시지 출력
            compassText.text = "나침반 기능이 이미 활성화되어 있습니다.";
        }
    }

    void UpdateCompass()
    {
        // 현재의 방위각 가져오기
        float heading = Input.compass.trueHeading;

        // 목표 회전 각도 설정
        targetRotation = Quaternion.Euler(0f, 0f, -heading);
    }

    void Update()
    {
        // 부드러운 회전 처리
        arrowImage.transform.rotation = Quaternion.Slerp(arrowImage.transform.rotation, targetRotation, smoothFactor * Time.deltaTime);

        // 각도를 텍스트로 변환하여 Text에 출력 (반올림된 각도 사용)
        compassText.text = Mathf.Round(-targetRotation.eulerAngles.z).ToString() + "도";
    }

    void OnDestroy()
    {
        // 스크립트가 파괴될 때 Compass 센서 비활성화
        Input.compass.enabled = false;
    }
}
