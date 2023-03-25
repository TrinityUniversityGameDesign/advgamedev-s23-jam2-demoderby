using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed = 1000f; //forward
    public float tSpeed = 1000f; //turn

    private Vector3 forwardForce;
    private Vector3 turnForce;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horiz, 0f, vert);
        //Debug.Log(inputDirection);
        //Debug.Log(rb.velocity.magnitude);
        if (Mathf.Abs(horiz) > 0.1f)
        {
            transform.Rotate(new Vector3(0f, horiz, 0f));
        }

        Vector3 moveForce = transform.forward * vert;
        rb.AddForce(speed * Time.deltaTime * moveForce);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
        }
    }
}
