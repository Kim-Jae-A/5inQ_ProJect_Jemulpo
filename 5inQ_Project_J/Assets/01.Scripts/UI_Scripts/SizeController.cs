using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeController : MonoBehaviour
{
    public List<Text> textComponentsToEnlarge1; // 첫 번째 확대되어야 하는 텍스트 컴포넌트를 저장하는 리스트
    public List<Text> textComponentsToEnlarge2; // 두 번째 확대되어야 하는 텍스트 컴포넌트를 저장하는 리스트
    public List<Text> textComponentsToMove; // 확대되는 텍스트와 동일한 순서로 이동해야 하는 텍스트를 저장하는 리스트
    public float enlargedSize1 = 30f; // 첫 번째 텍스트를 확대할 크기
    public float enlargedSize2 = 50f; // 두 번째 텍스트를 확대할 크기
    public float yOffset1 = 10f; // 첫 번째 텍스트의 이동량
    public float yOffset2 = 20f; // 두 번째 텍스트의 이동량
    private List<int> originalSizes1; // 첫 번째 텍스트의 원래 크기를 저장하는 리스트
    private List<int> originalSizes2; // 두 번째 텍스트의 원래 크기를 저장하는 리스트
    private List<Vector3> originalPositions; // 각 텍스트의 원래 위치를 저장하는 리스트
    private bool isEnlarged = false; // 텍스트가 확대되었는지 여부를 나타내는 변수

    private void Start()
    {
        originalSizes1 = new List<int>(); // 리스트 초기화
        originalSizes2 = new List<int>(); // 리스트 초기화
        originalPositions = new List<Vector3>(); // 리스트 초기화

        // 첫 번째 확대되어야 하는 텍스트의 원래 크기 저장
        foreach (Text textComponent in textComponentsToEnlarge1)
        {
            originalSizes1.Add(textComponent.fontSize);
        }

        // 두 번째 확대되어야 하는 텍스트의 원래 크기 저장
        foreach (Text textComponent in textComponentsToEnlarge2)
        {
            originalSizes2.Add(textComponent.fontSize);
        }

        // 확대되는 텍스트와 동일한 순서로 이동해야 하는 텍스트의 원래 위치 저장
        foreach (Text textComponent in textComponentsToMove)
        {
            // 텍스트의 위치 정보 저장
            originalPositions.Add(textComponent.rectTransform.position);
        }
    }

    public void ToggleTextSize()
    {
        // 첫 번째 확대되어야 하는 텍스트에 대해 크기 변경
        for (int i = 0; i < textComponentsToEnlarge1.Count; i++)
        {
            if (isEnlarged)
            {
                textComponentsToEnlarge1[i].fontSize = originalSizes1[i]; // 텍스트를 원래 크기로 변경
            }
            else
            {
                textComponentsToEnlarge1[i].fontSize = Mathf.RoundToInt(enlargedSize1); // 텍스트를 확대된 크기로 변경
            }
        }

        // 두 번째 확대되어야 하는 텍스트에 대해 크기 변경
        for (int i = 0; i < textComponentsToEnlarge2.Count; i++)
        {
            if (isEnlarged)
            {
                textComponentsToEnlarge2[i].fontSize = originalSizes2[i]; // 텍스트를 원래 크기로 변경
            }
            else
            {
                textComponentsToEnlarge2[i].fontSize = Mathf.RoundToInt(enlargedSize2); // 텍스트를 확대된 크기로 변경
            }
        }

        // 각 텍스트에 대해 이동
        for (int i = 0; i < textComponentsToMove.Count; i++)
        {
            // 텍스트만 y축으로 이동
            float yOffset = isEnlarged ? 0f : (i < textComponentsToEnlarge1.Count ? -yOffset1 : -yOffset2);
            Vector3 newPosition = originalPositions[i] + new Vector3(0f, yOffset, 0f);
            textComponentsToMove[i].rectTransform.position = newPosition;
        }

        isEnlarged = !isEnlarged; // 확대 여부를 토글
    }
}
