using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// 뒤로가기 버튼 제어를 위한 스크립트
/// </summary>
public class Change_Scene : MonoBehaviour
{
    public GameObject neviPanel; // 현재 위치와 도착 위치 및 경로 미리보기가 있는 패널
    public GameObject infoPanel; // POI 마커에 해당하는 위치의 상세 정보 페이지 팝업
    public GameObject markerPanel; // 마커 페널
    public GameObject[] marker; // POI 마커 데이터
    public GameObject endPoint; // 도착 위치 마커
    public GameObject lineObj; // 경로를 받아 화면에 그려주는 게임 오브젝트

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
            if (Input.GetKeyDown(KeyCode.Escape)) //뒤로가기 버튼을 누르면
            {
                OffNeviPanel();
                return;
            }
        }
        if (infoPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) //뒤로가기 버튼을 누르면
            {
                OffInfoPanel();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape)) //뒤로가기 버튼을 누르면
            {
                SceneManager.LoadScene("Home");
            }
        }

    }

    public void HomeScene() 
    {
        if (neviPanel.activeSelf)
        {
            OffNeviPanel(); // 경로안내창이 켜져있으면 상세 정보 창을 켜고 경로 안내 창을 끈다     
            return;
        }
        if (infoPanel.activeSelf)
        {
            OffInfoPanel(); // 상세 정보 페널이 켜져있으면 상세정보창을 끄고 맵 화면으로 간다
        }
        else
        {
            SceneManager.LoadScene("Home"); // 모든 패널이 꺼져있을때 
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