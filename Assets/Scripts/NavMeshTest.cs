using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTest : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;

    [SerializeField]
    private List<Transform> allTargets = new List<Transform>();
    [SerializeField]
    private Transform target;

    public float distThreshold = 2f;

    [SerializeField]
    private int currentIndex = 0;

    public float dist;


    void Start()
    {
        currentIndex = Random.Range(0, allTargets.Count - 1);
        agent = GetComponent<NavMeshAgent>();
        target = allTargets[currentIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if(target){
            agent.destination = target.position;
        }

        dist = Vector3.Distance(transform.position, target.position);

        if(dist <= distThreshold){
            //currentIndex = (currentIndex + 1) % (allTargets.Count);
            currentIndex = Random.Range(0, allTargets.Count - 1);
            target = allTargets[currentIndex];
        }
    }
}
