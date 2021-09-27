using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrust = 100f;
    [SerializeField] float rot = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem leftP;
    [SerializeField] ParticleSystem rightP;
    [SerializeField] ParticleSystem thrustP;


    Rigidbody rb;
    AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        ProcessRotation();
        ProcessThrust();
    }

    void ProcessThrust()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            if (!aud.isPlaying) aud.PlayOneShot(mainEngine);
            // aud.Play();
            thrustP.Play();
            rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        }
        else
        {
            aud.Stop();
            thrustP.Stop();
        }
    }

    void ProcessRotation()
    {


        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rot);
            leftP.Stop();
            rightP.Play();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rot);
            rightP.Stop();
            leftP.Play();
        }
        else
        {
            rightP.Stop();
            leftP.Stop();
        }
    }

    private void ApplyRotation(float rotThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}