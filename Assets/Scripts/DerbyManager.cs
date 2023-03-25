using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DerbyManager : MonoBehaviour
{
    public GameObject player;
    public Camera mainCam;

    private Vector3 startPosition = new Vector3(-28.4f, 42f, 11.5f);

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        player.tag = "Player";
        player.AddComponent<Rigidbody>();
        player.AddComponent<CarMovement>();
        player.transform.position = startPosition;
        mainCam.transform.parent = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
