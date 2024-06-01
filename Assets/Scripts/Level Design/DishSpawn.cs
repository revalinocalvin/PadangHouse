using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject[] foods;
    public Sprite[] sprites;
    public GameObject dish;
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
        GameObject tray = Instantiate(dish, transform.position, Quaternion.identity);
        ObjectSpawner trayInfo = tray.GetComponent<ObjectSpawner>();
        trayInfo.objectToSpawn = foods[1];

    }


    /*switch (choose)
        {
            case 1:

                break;

            case 2:

                break;

            case 3:

                break;

            case 4:

                break;

            case 5:

                break;

            case 6:

                break;

            default:
                break;
        }*/
}
