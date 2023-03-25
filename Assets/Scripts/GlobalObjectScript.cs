using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GlobalObjectScript : MonoBehaviour
{
    public List<GameObject> cars = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < cars.Count; i++){
            NonPlayableCar npc = cars[i].GetComponent<NonPlayableCar>();
            if(npc.health <= 0){
                npc.destruction();
                cars.RemoveAt(i);
            }
        }

        if(cars.Count == 0){
            SceneManager.LoadScene("WinScreen");
        }
    }
}
