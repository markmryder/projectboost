using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;

    [SerializeField]
    float rcsThrust = 100f;

    [SerializeField]
    float mainThrust = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
        //OnCollisionEnter();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly": print("OK");
                break;
            case "End": print("Win");
                break;
            default:
                print("Dead");
                break;

            
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true;
        
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false;
    }

    private void Thrust()
    {
       

        if (Input.GetKey(KeyCode.Space))
        {
            //float thrustThisFrame = mainThrust * Time.deltaTime;
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
