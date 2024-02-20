using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonDataUse : MonoBehaviour
{
    //Json 데이터 호출 가능 여부 및 방법 확인용 코드
    void Start()
    {
        // JsonManager 클래스의 인스턴스를 통해 데이터에 접근
        if (JsonManager.instance != null && JsonManager.instance.data != null &&
            JsonManager.instance.data.route.trafast.Count > 0)
        {
            TraFast firstTraFast = JsonManager.instance.data.route.trafast[0];
            // 위에 3줄 코드 입력후 불러올 데이터 foreach문으로 확인 or 출력

            // Path 데이터 확인
            foreach (var point in firstTraFast.path)
            {
                // 리스트 내의 각 포인트 출력
                Debug.Log("Longitude: " + point[0] + ", Latitude: " + point[1]);
            }
            // Section 데이터 확인
            foreach (Section sectionInfo in firstTraFast.section)
            {
                Debug.Log("Point Index: " + sectionInfo.pointIndex);
                Debug.Log("Point Count: " + sectionInfo.pointCount);
                Debug.Log("Distance: " + sectionInfo.distance);
                Debug.Log("Name: " + sectionInfo.name);
                Debug.Log("Congestion: " + sectionInfo.congestion);
                Debug.Log("Speed: " + sectionInfo.speed);
            }

            // Guide 데이터 확인
            foreach (Guide guideInfo in firstTraFast.guide)
            {
                Debug.Log("Point Index: " + guideInfo.pointIndex);
                Debug.Log("Type: " + guideInfo.type);
                Debug.Log("Instructions: " + guideInfo.instructions);
                Debug.Log("Distance: " + guideInfo.distance);
                Debug.Log("Duration: " + guideInfo.duration);
            }
            // 나머지 데이터에 대한 처리
        }
    }
}
