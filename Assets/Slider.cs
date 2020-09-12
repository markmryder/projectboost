using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    Rigidbody body;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
