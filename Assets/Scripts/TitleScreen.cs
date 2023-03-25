using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public GameObject[] cars;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cars.Length; i++)
        {
            if (i > 0)
                cars[i].transform.Rotate(new Vector3(0, 1, 0));
            else cars[i].transform.Rotate(new Vector3(0, -1, 0));
        }
    }

    public void LoadCarSelect()
    {
        SceneManager.LoadScene("CarSelection");
    }
}
