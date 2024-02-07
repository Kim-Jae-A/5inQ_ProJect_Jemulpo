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
            Debug.Log("인터넷 연결");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
