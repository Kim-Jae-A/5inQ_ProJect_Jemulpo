using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[System.Serializable]
public class AR_POI
{
    public int idx;
    public string Name;
    public string Info;
    public string shooting;
    public string imagepath;
    public string Address;
    public string latitude;
    public string longitude;
}

[System.Serializable]
public class AR_DataList
{
    public List<AR_POI> ARzone_List;
}
public class ScrollViewManager : MonoBehaviour
{
    //스크롤뷰의 요소로 생성할 프리팹과 프리팹을 생성할 content.
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject List_element;

    //포토존과 도슨트 POI
    private TextAsset jsonfile_Photozone;
    private TextAsset jsonfile_Docent;

    void Start()
    {
        //json파일을 로드
        jsonfile_Photozone = Resources.Load<TextAsset>("Json/POIData");
        jsonfile_Docent = Resources.Load<TextAsset>("Json/POIData_Docent");

        string selectedState = PlayerPrefs.GetString("selectedState", "Photozone");

        if (selectedState == "Photozone")
        {
            //초기에는 포토존이 선택되어 있어야 하므로 포토존 json파일을 로드하고 스크롤뷰의 요소로 생성.
            LoadAndShowData(jsonfile_Photozone);
        }
        else if (selectedState == "Docent")
        {
            LoadAndShowData(jsonfile_Docent);
        }


    }

    // 2개의 함수는 스크롤뷰 위 포토존과 도슨트 버튼을 클릭했을 때 실행하는 함수이다.
    // 이 함수로 json파일이 변경되고 데이터를 바꿔서 프리팹을 생성한다.
    public void OnPhotozoneButtonClicked()
    {
        PlayerPrefs.SetString("selectedState", "Photozone");
        LoadAndShowData(jsonfile_Photozone);
    }
    public void OnDocentButtonClicked()
    {
        PlayerPrefs.SetString("selectedState", "Docent");
        LoadAndShowData(jsonfile_Docent);
    }

    //json파일이 존재하면 json파일을 직렬화한다.
    private void LoadAndShowData(TextAsset jsonfile)
    {
        if (jsonfile != null)
        {
            AR_DataList ar_dataList = JsonUtility.FromJson<AR_DataList>(jsonfile.text);

            //content 내부의 오브젝트, 즉 생성된 요소들을 모두 제거하여, 버튼을 눌렀을 때 새로운 json파일의 
            //데이터로 업데이트 하기 전에 기존 내용을 지워주는 부분!
            foreach (Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }
            //위에서 스크롤뷰의 요소들을 지워주면 여기서는 json파일의 데이터를 받아서 content에 요소를 생성한다.
            foreach (AR_POI ARZone_ in ar_dataList.ARzone_List)
            {
                CreateScrollviewContent(ARZone_);
            }
        }
    }

    //AR_POI의 객체의 데이터, 즉 json의 데이터를 이용해서 element를 생성하고 그 element의 Text에 AR_POI의 데이터를
    //연결하여 정보를 띄운다!
    private void CreateScrollviewContent(AR_POI ARZone_poi)
    {
        GameObject element = Instantiate(List_element, content.transform);

        Text title = element.transform.Find("Title and descripttion/Title").GetComponent<Text>();
        Text info = element.transform.Find("Title and descripttion/Description").GetComponent<Text>();
        Image image = element.transform.Find("DescripImage").GetComponent<Image>();


        if (title != null)
        {
            title.text = ARZone_poi.Name;
        }

        if (info != null)
        {
            info.text = ARZone_poi.Info;
        }

        if (image != null)
        {
            Sprite sprite = Resources.Load<Sprite>(ARZone_poi.imagepath);
            if (sprite != null)
            {
                image.sprite = sprite;
            }

        }

        Button button = element.GetComponent<Button>();
        if(button == null)
        {
            button = element.AddComponent<Button>();
        }
        button.onClick.AddListener(() => OnScrollview_ElementClicked(ARZone_poi));

    }

    private void OnScrollview_ElementClicked(AR_POI arData)
    {
        if (arData == null)
        {
            Debug.Log("arData is null.");
            return;
        }
        Debug.Log("OnScrollview_ElementClicked - Data received: " + (arData != null ? arData.Name : "null"));
        JsonDataHolder.Instance.SetSelectedARData(arData);
        SceneManager.LoadScene("PhotoZone_Docent", LoadSceneMode.Single);
    }

}
