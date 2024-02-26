using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocentController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Button RePlayBtn;
    //SerializeField] public AudioSource narration;
    Docent_Player docent_player;
    void Start()
    {

        RePlayBtn.onClick.AddListener(RePlay_Docent);
        StartCoroutine(DocentAnimation());
        docent_player = GetComponent<Docent_Player>();
    }
    IEnumerator DocentAnimation()
    {
        //narration.Play();
        yield return new WaitForSeconds(1.2f);
        animator.SetBool("Climb", true);
        yield return new WaitForSeconds(3F);
        animator.SetBool("Climb", false);
        animator.SetBool("Hello", true);
        yield return new WaitForSeconds(4.5f);
        animator.SetBool("Hello", false);
        animator.SetBool("Guide", true);
        yield return new WaitForSeconds(4.5f);
        animator.SetBool("Guide", false);
        animator.SetBool("Studying", true);
        yield return new WaitForSeconds(8);
        animator.SetBool("Studying", false);
        animator.SetBool("Many", true);
        yield return new WaitForSeconds(5.6f);
        animator.SetBool("Many", false);
        animator.SetBool("Explain", true);
        yield return new WaitForSeconds(7.3f);
        animator.SetBool("Explain", false);
        animator.SetBool("Good", true);
        yield return new WaitForSeconds(4);
        animator.SetBool("Good", false);
        animator.SetBool("Guide", true);
        yield return new WaitForSeconds(6.5f);
        animator.SetBool("Guide", false);
        animator.SetBool("Hello", true);
        yield return new WaitForSeconds(3f);
        animator.SetBool("Hello", false);
    }


    public void RePlay_Docent()
    {

        StartCoroutine(DocentAnimation());
        docent_player.narration.Play();
    }
}
