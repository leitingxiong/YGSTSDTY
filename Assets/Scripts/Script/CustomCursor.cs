using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    private void Awake()
    {

        transform.position = Input.mousePosition;
    }
    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
