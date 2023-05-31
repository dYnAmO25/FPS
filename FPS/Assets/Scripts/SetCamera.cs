using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamera : MonoBehaviour
{
    [SerializeField] GameObject camPos;
    [SerializeField] GameObject camPos2;

    void Start()
    {
        
    }


    void Update()
    {
        if (camPos.activeSelf)
        {
            transform.position = camPos.transform.position;
        }
        else if (camPos2.activeSelf)
        {
            transform.position = camPos2.transform.position;
        }
    }
}
