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
        yield return new WaitForSeconds(0.7f);
        animator.SetBool("Hello", true);
        yield return new WaitForSeconds(4);
        animator.SetBool("Hello", false);
        animator.SetBool("Guide", true);
        yield return new WaitForSeconds(4);
        animator.SetBool("Guide", false);
        animator.SetBool("Studying", true);
        yield return new WaitForSeconds(8);
        animator.SetBool("Studying", false);
        animator.SetBool("Many", true);
        yield return new WaitForSeconds(4);
        animator.SetBool("Many", false);
        animator.SetBool("Good", true);
        yield return new WaitForSeconds(4);
        animator.SetBool("Good", false);
        animator.SetBool("Hello", true);
        yield return new WaitForSeconds(1.3f);
        animator.SetBool("Hello", false);
        
    }


    public void RePlay_Docent()
    {
        
        StartCoroutine(DocentAnimation());
    }
}
