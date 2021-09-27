using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{

    [SerializeField] Quaternion Rotation = new Quaternion(0, 0, 0, 1);
    public float tumble;

    private float maxRotationSpeed;
    private Vector3 rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Random.rotation;
        tumble = Random.value;
        rotSpeed = Random.insideUnitSphere * maxRotationSpeed;
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotSpeed * Time.deltaTime);

    }
}
