using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Globals : MonoBehaviour
{
    public UnityEvent startDerby = new UnityEvent();

    // Update is called once per frame
    void Update()
    {
        startDerby.AddListener(GlobalStartDerby);
    }

    public void GlobalStartDerby()
    {
        Debug.Log("Derby Started");
    }
}
