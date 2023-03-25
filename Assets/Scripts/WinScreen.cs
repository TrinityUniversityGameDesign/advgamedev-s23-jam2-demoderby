using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public List<GameObject> carPrefab = new List<GameObject>();
    public List<GameObject> activeCars = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("spawnUp", 0f, 5f);
        //InvokeRepeating("spawnDown", 2f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToTitle(){
        SceneManager.LoadScene("TitleScene");
    }

    public void spawnUp(){
        GameObject car = Instantiate(carPrefab[Random.Range(0, carPrefab.Count - 1)], new Vector3(0,5,0), Quaternion.Euler(0,90,0), GameObject.Find("Canvas").transform);
        car.SetActive(true);
    }

    public void spawnDown(){
        GameObject car = Instantiate(carPrefab[Random.Range(0, carPrefab.Count - 1)], new Vector3(0,-5,0), Quaternion.Euler(0,90,0), GameObject.Find("Canvas").transform);
        car.SetActive(true);
    }
}
