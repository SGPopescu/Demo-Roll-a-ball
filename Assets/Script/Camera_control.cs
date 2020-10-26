using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_control : MonoBehaviour
{
    public GameObject sfera;
    private Vector3 offset;
    void Start()
    {
        offset = GetComponent<Transform>().position - sfera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = sfera.transform.position + offset;
    }
}
