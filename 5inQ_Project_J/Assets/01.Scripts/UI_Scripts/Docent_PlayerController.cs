using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Docent_PlayerController : MonoBehaviour
{
    [SerializeField] private Slider Docent_slider;//애니메이션 진행률표시 슬라이더
    [SerializeField] private Button BackToDescription;//이전 씬 이동
    [SerializeField] private Button Replay; //애니메이션 다시보기 버튼
    public Docent_AnimationController animationController;//프리팹이 생성되면 연결되어야 하기 때문에 public 사용


    // 프리팹 생성 이벤트를 위한 델리게이트와 이벤트
    public delegate void DocentCreated(); 
    public static event DocentCreated OnARDocentCreated;

    void Start()
    {

        Replay.gameObject.SetActive(false);
        Docent_slider.gameObject.SetActive(false);
        OnARDocentCreated += () => //프리팹이 생성되면 OnARDocentCreated 이벤트를 통해 replay버튼과 슬라이더를 활성화.
        {
            Replay.gameObject.SetActive(true);
            Docent_slider.gameObject.SetActive(true);
            Docent_slider.maxValue = animationController.narration.clip.length;//gaebalone.length;  
            Replay.onClick.AddListener(animationController.RePlay_Docent);
        };
        //animationController = GetComponent<Docent_AnimationController>();   
        BackToDescription.onClick.AddListener(Return_Docent_Description);//뒤로가기버튼 이벤트 추가.
    }
    void Update()
    {
        //프리팹이 생성되지 않으면 
        if (animationController != null && !Replay.gameObject.activeSelf)
        {
            OnARDocentCreated?.Invoke();
        }

        //프리팹이 생성되면 애니메이션의 오디오소스의 시간을 슬라이더바의 value로 지정해줘. 진행률을 나타낸다.
        float narrationProgress = animationController.narration.time;
        if (animationController.narration.isPlaying)
        {
            Docent_slider.value = narrationProgress;
        }
    }

    //이전 씬으로 돌아가기(도슨트 상세설명창에서 도슨트 애니메이션 실행 씬으로 넘어왔을 때 선택한 도슨트 상세설명창 씬으로 돌아갑니다.)
    private void Return_Docent_Description()
    {
        string previousScene = PlayerPrefs.GetString("PreviousScene");
        if(!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
    }
}