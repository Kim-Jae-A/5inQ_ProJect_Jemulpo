using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CatAnimatorController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] Transform target; // ���� ��ǥ ��ġ�� �迭�� �����մϴ�.


    [SerializeField]private float speed = 2.0f;
    [SerializeField]private float rotationspeed = 5.0f;
    [SerializeField] private float scaleUp = 0.1f;
    [SerializeField]private float growthSpeed = 0.1f;
    [SerializeField]private float maxScale = 2.0f;





    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("IsWalking", true);
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        if(transform.localScale.x < maxScale )
        {
            transform.localScale += new Vector3(growthSpeed, growthSpeed, growthSpeed) * Time.deltaTime;
        }
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        if (Vector3.Distance(transform.position, target.position) < 0.001f)
        {
            if (animator)
            {
                animator.SetBool("IsWalking", false);
                transform.localScale = new Vector3(maxScale, maxScale, maxScale);
                StartCoroutine(PlayRandomAnimationAfterDelay(1.5f)); // 3�� �Ŀ� isCry�� true�� �����մϴ�.
            }
        }
    }
    IEnumerator PlayRandomAnimationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // �������� 'IsJumping' �Ǵ� 'IsCry' �ִϸ��̼��� �����մϴ�.
        bool isJumping = (Random.value > 0.5f);
        if (isJumping)
        {
            animator.SetBool("IsJumping", true);
            yield return new WaitForSeconds(1.5f);
            animator.SetBool("IsJumping", false);
        }
        else
        {
            animator.SetBool("IsCry", true);
            yield return new WaitForSeconds(1.5f);
            animator.SetBool("IsCry", false);
        }

        animator.SetBool("IsWalking", false); // �ȴ� �ִϸ��̼��� �ٽ� �����մϴ�.
    }
}
