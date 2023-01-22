using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeOrbit : MonoBehaviour
{
    public Transform target;
    public float orbitSpeed;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        // target.position = new Vector3(target.position.x, 1, target.position.z);
        // transform.position = target.position + offset;
        // transform.position = new Vector3(transform.position.x, 2.0f, transform.position.z);
        transform.RotateAround(target.position, Vector3.up, orbitSpeed * Time.deltaTime);
        // offset = transform.position - target.position;
    }
}
