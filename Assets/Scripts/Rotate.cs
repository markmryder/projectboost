using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    
    [SerializeField] [Range(1, 30)] float degreeStep = 30.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = degreeStep * Time.deltaTime;
        transform.Rotate(Vector3.forward, step);
    }
}
