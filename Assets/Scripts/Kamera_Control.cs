using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera_Control : MonoBehaviour
{
    public bool sona_geldikmi;
    public GameObject kamera_Son_konum;
    public Transform target;
    Vector3 target_offset;
    void Start()
    {
        target_offset = transform.position - target.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if(!sona_geldikmi)
        transform.position = Vector3.Lerp(transform.position, target.position + target_offset, .125f);
        else
        {
            transform.position = Vector3.Lerp(transform.position, kamera_Son_konum.transform.position, .015f);
        }
    }
}
