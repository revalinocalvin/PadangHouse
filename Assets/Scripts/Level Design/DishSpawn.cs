using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;

public class DishSpawn : MonoBehaviour
{
    public GameObject[] foods;

    [Header("Assigned Sprite for Dish")]
    public Sprite[] sprites;

    public GameObject dish;
    public GameObject dishUI;
    public GameObject player;

    public GameObject[] slotPoint;
    public bool[] slotAvailable;

    [Header("Assigned Buttons")]
    [SerializeField] private TextMeshProUGUI selectedButton;
    [SerializeField] private TextMeshProUGUI topButton;
    [SerializeField] private TextMeshProUGUI topButton2;
    [SerializeField] private TextMeshProUGUI bottomButton;
    [SerializeField] private TextMeshProUGUI bottomButton2;

    private int choose;

    private void Update()
    {
        if (dishUI.active)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (choose == 5)
                {
                    choose = 0;
                }
                else
                {
                    choose++;
                }
                SelectFoods();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (choose == 0)
                {
                    choose = 5;
                }
                else
                {
                    choose--;
                }
                SelectFoods();
            }            

            if (Input.GetKeyDown(KeyCode.E))
            {
                Input.ResetInputAxes();
                CheckSlot();

                ChooseFood();

                Debug.Log("setting dish ui false");
                dishUI.SetActive(false);
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                playerMovement.enabled = true;

            }
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
                tray.transform.SetParent(slot.transform);
                ObjectSpawner trayInfo = tray.GetComponent<ObjectSpawner>();
                SpriteRenderer asset = tray.GetComponent<SpriteRenderer>();
                asset.sprite = sprites[choose];
                trayInfo.objectToSpawn = foods[choose]; 
                slotAvailable[i] = false;
                
                break;
            }
        }
            
    }

    public void SelectFoods()
    {        
        switch (choose)
        {
            case 0:
                topButton2.text = "Fried Egg";
                topButton.text = "Dendeng";
                selectedButton.text = "Rendang";
                bottomButton.text = "Fried Chicken";
                bottomButton2.text = "Fish";

                break;
            case 1:
                topButton2.text = "Dendeng";
                topButton.text = "Rendang";
                selectedButton.text = "Fried Chicken";
                bottomButton.text = "Fish";
                bottomButton2.text = "Veggies";

                break;
            case 2:
                topButton2.text = "Rendang";
                topButton.text = "Fried Chicken";
                selectedButton.text = "Fish";
                bottomButton.text = "Veggies";
                bottomButton2.text = "Fried Egg";

                break;
            case 3:
                topButton2.text = "Fried Chicken";
                topButton.text = "Fish";
                selectedButton.text = "Veggies";
                bottomButton.text = "Fried Egg";
                bottomButton2.text = "Dendeng";

                break;
            case 4:
                topButton2.text = "Fish";
                topButton.text = "Veggies";
                selectedButton.text = "Fried Egg";
                bottomButton.text = "Dendeng";
                bottomButton2.text = "Rendang";

                break;
            case 5:
                topButton2.text = "Veggies";
                topButton.text = "Fried Egg";
                selectedButton.text = "Dendeng";
                bottomButton.text = "Rendang";
                bottomButton2.text = "Fried Chicken";

                break;

            default:
                break;
        }
    }

    public void CheckSlot()
    {
        for (int i = 0; i < slotAvailable.Length; i++)
        {
            if (slotPoint[i].transform.childCount == 0)
            {
                slotAvailable[i] = true;
            }
            else
            {
                slotAvailable[i] = false;
            }
        }
    }
}