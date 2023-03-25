using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed = 1000f; //forward
    public float tSpeed = 1000f; //turn

    private Vector3 forwardForce;
    private Vector3 turnForce;

    public LayerMask groundMask;
    private Rigidbody rb;
    private Transform groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundCheck = transform.Find("GroundCheck");
    }

    private float currVel;
    // Update is called once per frame
    void Update()
    {
        bool grounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundMask);
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horiz, 0f, vert);
        //Debug.Log(rb.velocity.magnitude);
        if (Mathf.Abs(horiz) > 0.1f)
        {
            transform.Rotate(new Vector3(0f, horiz, 0f));
        }

        Vector3 moveForce = transform.forward * vert;
        rb.AddForce(speed * Time.deltaTime * moveForce);
    }
}
