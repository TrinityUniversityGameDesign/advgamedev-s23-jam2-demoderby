using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPath : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    
    public List<Transform> nodes = new List<Transform>();
    [SerializeField]
    private int currentIndex = 0;
    [SerializeField]
    private Transform currentNode;

    public LayerMask groundMask;
    private Transform groundCheck;

    

    public float speed = 400f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundCheck = transform.Find("GroundCheck");
        currentNode = nodes[0];
    }

    // Update is called once per frame
    void Update()
    {
        bool grounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundMask);
        //Vector3 direction = new Vector3(transform.position.x + currentNode.position.x, 0, transform.position.z + currentNode.position.z);
        //transform.rotation = Quaternion.Euler(direction);
        // transform.LookAt(currentNode);
        // Vector3 angle = transform.rotation.eulerAngles;
        // angle.x = 0;
        // angle.z= 0;
        // transform.rotation = Quaternion.Euler(angle);


    }
}
