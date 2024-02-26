using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeController : MonoBehaviour
{
    public List<Text> textComponentsToEnlarge1; // ù ��° Ȯ��Ǿ�� �ϴ� �ؽ�Ʈ ������Ʈ�� �����ϴ� ����Ʈ
    public List<Text> textComponentsToEnlarge2; // �� ��° Ȯ��Ǿ�� �ϴ� �ؽ�Ʈ ������Ʈ�� �����ϴ� ����Ʈ
    public List<Text> textComponentsToMove; // Ȯ��Ǵ� �ؽ�Ʈ�� ������ ������ �̵��ؾ� �ϴ� �ؽ�Ʈ�� �����ϴ� ����Ʈ
    public float enlargedSize1 = 30f; // ù ��° �ؽ�Ʈ�� Ȯ���� ũ��
    public float enlargedSize2 = 50f; // �� ��° �ؽ�Ʈ�� Ȯ���� ũ��
    public float yOffset1 = 10f; // ù ��° �ؽ�Ʈ�� �̵���
    public float yOffset2 = 20f; // �� ��° �ؽ�Ʈ�� �̵���
    private List<int> originalSizes1; // ù ��° �ؽ�Ʈ�� ���� ũ�⸦ �����ϴ� ����Ʈ
    private List<int> originalSizes2; // �� ��° �ؽ�Ʈ�� ���� ũ�⸦ �����ϴ� ����Ʈ
    private List<Vector3> originalPositions; // �� �ؽ�Ʈ�� ���� ��ġ�� �����ϴ� ����Ʈ
    private bool isEnlarged = false; // �ؽ�Ʈ�� Ȯ��Ǿ����� ���θ� ��Ÿ���� ����

    private void Start()
    {
        originalSizes1 = new List<int>(); // ����Ʈ �ʱ�ȭ
        originalSizes2 = new List<int>(); // ����Ʈ �ʱ�ȭ
        originalPositions = new List<Vector3>(); // ����Ʈ �ʱ�ȭ

        // ù ��° Ȯ��Ǿ�� �ϴ� �ؽ�Ʈ�� ���� ũ�� ����
        foreach (Text textComponent in textComponentsToEnlarge1)
        {
            originalSizes1.Add(textComponent.fontSize);
        }

        // �� ��° Ȯ��Ǿ�� �ϴ� �ؽ�Ʈ�� ���� ũ�� ����
        foreach (Text textComponent in textComponentsToEnlarge2)
        {
            originalSizes2.Add(textComponent.fontSize);
        }

        // Ȯ��Ǵ� �ؽ�Ʈ�� ������ ������ �̵��ؾ� �ϴ� �ؽ�Ʈ�� ���� ��ġ ����
        foreach (Text textComponent in textComponentsToMove)
        {
            // �ؽ�Ʈ�� ��ġ ���� ����
            originalPositions.Add(textComponent.rectTransform.position);
        }
    }

    public void ToggleTextSize()
    {
        // ù ��° Ȯ��Ǿ�� �ϴ� �ؽ�Ʈ�� ���� ũ�� ����
        for (int i = 0; i < textComponentsToEnlarge1.Count; i++)
        {
            if (isEnlarged)
            {
                textComponentsToEnlarge1[i].fontSize = originalSizes1[i]; // �ؽ�Ʈ�� ���� ũ��� ����
            }
            else
            {
                textComponentsToEnlarge1[i].fontSize = Mathf.RoundToInt(enlargedSize1); // �ؽ�Ʈ�� Ȯ��� ũ��� ����
            }
        }

        // �� ��° Ȯ��Ǿ�� �ϴ� �ؽ�Ʈ�� ���� ũ�� ����
        for (int i = 0; i < textComponentsToEnlarge2.Count; i++)
        {
            if (isEnlarged)
            {
                textComponentsToEnlarge2[i].fontSize = originalSizes2[i]; // �ؽ�Ʈ�� ���� ũ��� ����
            }
            else
            {
                textComponentsToEnlarge2[i].fontSize = Mathf.RoundToInt(enlargedSize2); // �ؽ�Ʈ�� Ȯ��� ũ��� ����
            }
        }

        // �� �ؽ�Ʈ�� ���� �̵�
        for (int i = 0; i < textComponentsToMove.Count; i++)
        {
            // �ؽ�Ʈ�� y������ �̵�
            float yOffset = isEnlarged ? 0f : (i < textComponentsToEnlarge1.Count ? -yOffset1 : -yOffset2);
            Vector3 newPosition = originalPositions[i] + new Vector3(0f, yOffset, 0f);
            textComponentsToMove[i].rectTransform.position = newPosition;
        }

        isEnlarged = !isEnlarged; // Ȯ�� ���θ� ���
    }
}
