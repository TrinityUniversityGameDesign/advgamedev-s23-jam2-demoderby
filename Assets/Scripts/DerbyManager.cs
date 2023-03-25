using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DerbyManager : MonoBehaviour
{
    private Vector3 startPosition = new Vector3(-28.4f, 40.9f, 11.5f);
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("DerbyScene");
    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(this);
    }

    public void GlobalStartDerby()
    {
        this.transform.position = startPosition;
    }
}
