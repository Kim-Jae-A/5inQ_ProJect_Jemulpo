using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RestApi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            
        }
        else
        {
            Debug.Log("���ͳ� ����");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
