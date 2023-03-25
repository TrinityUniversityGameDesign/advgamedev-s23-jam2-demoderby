using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SelectCar : MonoBehaviour
{
    public GameObject[] cars;
    public string[] carNames;
    public TMP_Text carNameText;
    public GameObject selectedCar;

    void Start()
    {
        selectedCar = cars[0];
        carNameText.text = carNames[0] + "!";
    }

    // Update is called once per frame
    void Update()
    {
        selectedCar.transform.Rotate(0, 1, 0);
    }

    public void buttonClick(int index)
    {
        selectedCar.transform.rotation = Quaternion.Euler(0, 90, 0);
        selectedCar = cars[index];
        carNameText.text = carNames[index] + "!";
    }

    public void startDerby()
    {
        DontDestroyOnLoad(selectedCar);
        Debug.Log("Loading Derby Scene");
        selectedCar.AddComponent<DerbyManager>();
    }
}
