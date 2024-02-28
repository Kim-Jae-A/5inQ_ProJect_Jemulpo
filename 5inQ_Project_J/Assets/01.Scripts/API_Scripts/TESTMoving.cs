using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTMoving : MonoBehaviour
{
    float x;
    float y;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        transform.Translate(x, 0, y);
    }
}
