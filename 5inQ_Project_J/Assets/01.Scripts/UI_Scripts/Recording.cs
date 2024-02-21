using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class Recording : MonoBehaviour
{
    [SerializeField]private static Recording instance;
    [SerializeField] private Text time, fileName;
    [SerializeField] private Transform arCam;
    private StreamWriter file;
    private void Awake()
    {
        instance = this;    
    }
   
    




}

    


