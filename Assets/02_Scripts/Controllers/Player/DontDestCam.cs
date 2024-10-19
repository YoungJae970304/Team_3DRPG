using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestCam : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
