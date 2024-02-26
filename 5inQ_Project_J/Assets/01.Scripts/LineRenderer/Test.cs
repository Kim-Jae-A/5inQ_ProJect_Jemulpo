using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DebugOutputCoroutine());
    }

    IEnumerator DebugOutputCoroutine()
    {
        while (true)
        {
            Debug.Log("Coroutine is running!");
            yield return new WaitForSeconds(1f); // 1초마다 디버그 출력
        }
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Debug.Log("test");
    }
    void Update()
    {
        Debug.Log("test");
    }
}
