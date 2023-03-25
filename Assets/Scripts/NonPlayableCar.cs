using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayableCar : MonoBehaviour
{
    public int health = 3;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(health <= 0){
        //     destruction();
        // }
    }

    void OnCollisionEnter(Collision target){
        if(target.transform.tag == "Player"){
            //takeDamage();
            Rigidbody targetRB = target.transform.GetComponent<Rigidbody>();
            if(targetRB.velocity.magnitude >= 10){
                takeDamage(3);
                Debug.Log(targetRB.velocity.magnitude);
            }
            else if(targetRB.velocity.magnitude >= 7){
                takeDamage(2);
                Debug.Log(targetRB.velocity.magnitude);
            }
            else if(targetRB.velocity.magnitude >= 3){
                takeDamage();
                Debug.Log(targetRB.velocity.magnitude);
            }
            else{
                Debug.Log(targetRB.velocity.magnitude);
            }
        }
    }

    public void takeDamage(int dmg = 1){
        health -= dmg;
    }

    public void destruction(){
        GetComponent<NavMeshTest>().enabled = false;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        rb.AddForce(0,3,0);
        Destroy(gameObject, 7f);
        //Debug.Log("destroyed");
    }
}
