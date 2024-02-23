using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Docent_Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Slider Docent_slider;
    [SerializeField] private AudioClip gaebalone;
    [SerializeField] private Button BackToDescription;
    public AudioSource narration;
    // Start is called before the first frame update
    void Start()
    {
        BackToDescription.onClick.AddListener(Return_Docent_Description);//뒤로가기버튼 이벤트 추가.
        narration.Play();
        Docent_slider.maxValue = gaebalone.length;
    }
    void Update()
    {
        float narrationProgress = narration.time;
        if (narration.isPlaying)
        {
            Docent_slider.value = narrationProgress;
        }

        /*
        //애니메이션의 state정보를 슬라이더바에 적용
        //하지만 state가 변경될 때 마다 슬라이더바가 최신화 됨.
        AnimatorStateInfo Docent_stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = Docent_stateInfo.length;
        float animationTime = Docent_stateInfo.normalizedTime % 1;
        Docent_slider.value = animationTime / animationLength;
        */
    }
 
    private void Return_Docent_Description()
    {
        SceneManager.LoadScene("PhotoZone_Docent");
    }
}
