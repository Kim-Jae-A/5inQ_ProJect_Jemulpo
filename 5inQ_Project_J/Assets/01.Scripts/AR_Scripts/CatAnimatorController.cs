using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CatAnimatorController : MonoBehaviour
{
    public ARTrackedImage trackedImage;
    public bool isMoving = false;
    float speed = 1.0f;

    private void Update()
    {
        if (isMoving)
        {

        }
    }
    void MoveWithinImageBounds()
    {
        GetComponent<Animator>().SetBool("IsWalking", true);
        float step = speed * Time.deltaTime;

        Vector3 targetPosition = trackedImage.transform.position;
        Vector3 nextPosition = Vector3.MoveTowards(transform.position, targetPosition, step);
        if (Vector3.Distance(nextPosition, trackedImage.transform.position) <= trackedImage.size.x / 2) // 이미지의 반경 내에 있는지 확인
        {
            transform.position = nextPosition;
        }
        else
        {
            // 걷는 애니메이션 종료
            GetComponent<Animator>().SetBool("IsWalking", false);
            isMoving = false;
        }
    }
}
