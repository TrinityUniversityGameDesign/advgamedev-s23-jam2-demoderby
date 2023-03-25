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

    private int carIndex;

    void Start()
    {
        selectedCar = cars[0];
        carIndex = 0;
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
        carIndex = index;
    }

    public void startDerby()
    {
        DontDestroyOnLoad(selectedCar);
        Debug.Log("Loading Derby Scene");
        selectedCar.transform.rotation = Quaternion.Euler(0, 0, 0);
        selectedCar.name = "Player";
        if (carIndex <= 6)
        {
            selectedCar.transform.localScale = new Vector3(1, 1, 1);
        }
        else selectedCar.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        SceneManager.LoadScene("DerbyScene");
    }
}
