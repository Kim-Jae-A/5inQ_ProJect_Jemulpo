using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Docent_Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Slider Docent_slider;
    [SerializeField] private Button BackToDescription;

    float AnimationTotalLength = 0;
    float currentLength = 0;
    // Start is called before the first frame update
    void Start()
    {
        BackToDescription.onClick.AddListener(Return_Docent_Description);
        AnimatorClipInfo[] clipInfos = animator.GetCurrentAnimatorClipInfo(0);
        foreach (var info in clipInfos)
        {
            AnimationTotalLength += info.clip.length;
        }

    }
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float clipProgress = stateInfo.normalizedTime % 1;
        float clipLength = stateInfo.length * clipProgress;
        if(clipLength < currentLength)
        {
            currentLength += stateInfo.length;
        }
        float totalProgress = currentLength + clipLength;
        Docent_slider.value = totalProgress;
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

    // Update is called once per frame
}
