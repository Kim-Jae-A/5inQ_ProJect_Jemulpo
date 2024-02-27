using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARGuide : MonoBehaviour
{
    LinRendererManager LRM;
    DistanceCalculator distanceCalculator;
    public Transform ARcamera; // 

    public Text text;
    public List<Vector3> target = new List<Vector3>();
    public List<int> pointIndex = new List<int>();
    public List<string> instructions = new List<string>();
    int nowGuide = 0;
    int targetNum;
    bool targetPositionCoroutineCompleted = false;
    private void Awake()
    {
        LRM = GetComponent<LinRendererManager>();
        distanceCalculator = GetComponent<DistanceCalculator>();

    }
    // Start is called before the first frame update
    void Start()
    {

        JsonCall();
        targetNum = pointIndex[nowGuide];
        StartCoroutine("Targetpositon");
    }

    IEnumerator Targetpositon()
    {
        while (LRM.LongLat == null)
        {
            yield return null;
        }
        foreach (Vector3 position in LRM.LongLat)
        {
            target.Add(position);
        }
        targetPositionCoroutineCompleted = true;
    }

    private void Update()
    {
        if (!targetPositionCoroutineCompleted)
        {
            return;
        }
        if (nowGuide != -1 && nowGuide < pointIndex.Count)
        {
            // �÷��̾�� Ÿ�� ������Ʈ ���� �Ÿ��� ���
            float distance = Vector3.Distance(ARcamera.position, target[targetNum]);

            // ���� �Ÿ��� 1���� �̳����
            if (distance <= 3f)
            {
                nowGuide++;
                if (nowGuide < pointIndex.Count)
                {
                    targetNum = pointIndex[nowGuide];
                }
                else
                {
                    // ��� ���̵� ����Ʈ�� �������� ���� ó��
                    text.text = "�ȳ��� �����մϴ�.";
                    return;
                }
            }

            string distanceString = distanceCalculator.DC(LRM.Latitude[targetNum], LRM.Longitude[targetNum]);
            text.text = distanceString + "�̵� ��\n" + instructions[nowGuide];
        }
    }

    void JsonCall()
    {
        if (JsonManager.instance != null && JsonManager.instance.data != null &&
            JsonManager.instance.data.route.trafast.Count > 0)
        {
            TraFast firstTraFast = JsonManager.instance.data.route.trafast[0];
            foreach (Guide guideInfo in firstTraFast.guide)
            {
                pointIndex.Add(guideInfo.pointIndex - 1);
                instructions.Add(guideInfo.instructions);
            }
        }
    }
}
