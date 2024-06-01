using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject[] foods;
    public Sprite[] sprites;

    public GameObject dish;

    public GameObject[] slotPoint;
    public bool[] slotAvailable;

    private int choose;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChooseFood();
        }
    }

    public void ChooseFood()
    {
        for (int i = 0; i < slotAvailable.Length; i++)
        {
            if (slotAvailable[i] == true)
            {
                GameObject slot = slotPoint[i];
                GameObject tray = Instantiate(dish, slot.transform.position, Quaternion.identity);
                ObjectSpawner trayInfo = tray.GetComponent<ObjectSpawner>();
                trayInfo.objectToSpawn = foods[0]; 
                slotAvailable[i] = false;
                
                break;
            }
        }

    }
}
