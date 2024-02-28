using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// �ڷΰ��� ��ư ��� ���� ��ũ��Ʈ
/// </summary>
public class Change_Scene : MonoBehaviour
{
    public GameObject neviPanel; // ���� ��ġ�� ���� ��ġ �� ��� �̸����Ⱑ �ִ� �г�
    public GameObject infoPanel; // POI ��Ŀ�� �ش��ϴ� ��ġ�� �� ���� ������ �˾�
    public GameObject markerPanel; // ��Ŀ ���
    public GameObject[] marker; // POI ��Ŀ ������
    public GameObject endPoint; // ���� ��ġ ��Ŀ
    public GameObject lineObj; // ��θ� �޾� ȭ�鿡 �׷��ִ� ���� ������Ʈ

    public void NeviScene()
    {
        SceneManager.LoadScene("Map_Scene");
    }

    private void Start()
    {
        StaticMapManager.instance.StartDrawing();
    }

    private void Update()
    {
        if (neviPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) //�ڷΰ��� ��ư�� ������
            {
                OffNeviPanel();
                return;
            }
        }
        if (infoPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) //�ڷΰ��� ��ư�� ������
            {
                OffInfoPanel();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape)) //�ڷΰ��� ��ư�� ������
            {
                SceneManager.LoadScene("Home");
            }
        }

    }

    public void HomeScene() 
    {
        if (neviPanel.activeSelf)
        {
            OffNeviPanel(); // ��ξȳ�â�� ���������� �� ���� â�� �Ѱ� ��� �ȳ� â�� ����     
            return;
        }
        if (infoPanel.activeSelf)
        {
            OffInfoPanel(); // �� ���� ����� ���������� ������â�� ���� �� ȭ������ ����
        }
        else
        {
            SceneManager.LoadScene("Home"); // ��� �г��� ���������� 
        }
    }

    void OffNeviPanel()
    {
        neviPanel.SetActive(false);
        markerPanel.SetActive(true);
        foreach (GameObject obj in marker)
        {
            obj.SetActive(true);
        }
        endPoint.SetActive(false);
    }

    void OffInfoPanel()
    {
        neviPanel.SetActive(true);
        infoPanel.SetActive(false);
        if (lineObj.transform.childCount > 0)
        {
            for (int i = 0; i < lineObj.transform.childCount; i++)
            {
                Destroy(lineObj.transform.GetChild(i).gameObject);
            }
            lineObj.GetComponent<LineRenderer>().positionCount = 0;
        }
    }
}