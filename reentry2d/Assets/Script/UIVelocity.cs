using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIVelocity : MonoBehaviour
{


    Text velocity;
    Rigidbody rb;
    public GameObject Robot;

    // Start is called before the first frame update
    void Start()
    {
        Robot = GameObject.FindWithTag("Player");
        velocity = this.GetComponent<Text>();
        rb = Robot.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        velocity.text = "Velocity: " + rb.velocity.ToString();

    }
}
