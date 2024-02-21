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
    float currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        BackToDescription.onClick.AddListener(Return_Docent_Description);
        StartCoroutine(UpdateDocentAnimationLength());
    }
    private IEnumerator UpdateDocentAnimationLength()
    {
        AnimatorClipInfo[] clipInfos = animator.GetCurrentAnimatorClipInfo(0);
        foreach (var info in clipInfos)
        {
            AnimationTotalLength += info.clip.length;
        }
        foreach(var info in clipInfos)
        {
            yield return new WaitForSeconds(info.clip.length);

            currentTime += info.clip.length;
            Docent_slider.value = currentTime / AnimationTotalLength;
        }

    }
    private void Return_Docent_Description()
    {
        SceneManager.LoadScene("PhotoZone_Docent");
    }

    // Update is called once per frame
    void Update()
    {
        //�ִϸ��̼��� state������ �����̴��ٿ� ����
        //������ state�� ����� �� ���� �����̴��ٰ� �ֽ�ȭ ��.
        /*
        AnimatorStateInfo Docent_stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = Docent_stateInfo.length;
        float animationTime = Docent_stateInfo.normalizedTime % 1;
        Docent_slider.value = animationTime / animationLength;
        */
    }
}
