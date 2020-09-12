using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    Rigidbody body;
    [SerializeField]
    int maxSpeed = 1;
    private Vector3 startPosition;
    [SerializeField]
    float Amplitude = 1.0f;
    void Start()
    {
        body = GetComponent<Rigidbody>();
        startPosition = body.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveVertical();
    }

    void MoveVertical()
    {
        body.position = new Vector3(body.position.x, startPosition.y + Mathf.Sin(Time.time * maxSpeed) * Amplitude, body.position.z);
    }

}
