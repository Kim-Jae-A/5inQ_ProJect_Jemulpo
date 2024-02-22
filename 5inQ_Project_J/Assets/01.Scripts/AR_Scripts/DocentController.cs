using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocentController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource narration;
    
    // Start is called before the first frame update
    
    void Start()
    {
        //replay.onClick.AddListener(RePlay_Docent);
        StartCoroutine(DocentAnimation());
    }
    IEnumerator DocentAnimation()
    {
        narration.Play();
        yield return new WaitForSeconds(1.2f);
        animator.SetBool("Climb", true);
        yield return new WaitForSeconds(3);
        animator.SetBool("Climb", false);
        animator.SetBool("Hello", true);
        yield return new WaitForSeconds(4.5f);
        animator.SetBool("Hello", false);
        animator.SetBool("Guide", true);
        yield return new WaitForSeconds(4);
        animator.SetBool("Guide", false);
        animator.SetBool("Studying", true);
        yield return new WaitForSeconds(8);
        animator.SetBool("Studying", false);
        animator.SetBool("Many", true);
        yield return new WaitForSeconds(5.6f);
        animator.SetBool("Many", false);
        animator.SetBool("Explain", true);
        yield return new WaitForSeconds(8.3f);
        animator.SetBool("Explain", false);
        animator.SetBool("Good", true);
        yield return new WaitForSeconds(4);
        animator.SetBool("Good", false);
        animator.SetBool("Guide", true);
        yield return new WaitForSeconds(7f);
        animator.SetBool("Guide",false);
        animator.SetBool("Hello", true);
        
    }


    public void RePlay_Docent()
    {
        
        StartCoroutine(DocentAnimation());
    }
}
