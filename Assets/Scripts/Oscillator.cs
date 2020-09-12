using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f,10f,10f);
    [SerializeField][Range(0,1)]float movementFactor;
    Vector3 startingPos;

    [SerializeField] float period = 2.0f;
    [SerializeField][Range(0,1)] float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon ||
            moveSpeed <= Mathf.Epsilon) { return; }

        float cycles = Time.time * moveSpeed / period;
        const float tau = Mathf.PI * 2;
        float rawSineWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSineWave / 2f + 0.5f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
