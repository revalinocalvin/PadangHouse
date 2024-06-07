using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Assigned Icons")]
    [SerializeField] private GameObject selectedIcon;
    [SerializeField] private GameObject topIcon;
    [SerializeField] private GameObject bottomIcon;

    [Header("Assigned Sprites")]
    [SerializeField] public Sprite Rendang;
    [SerializeField] public Sprite Chicken;
    [SerializeField] public Sprite Fish;
    [SerializeField] public Sprite Veggies;
    [SerializeField] public Sprite Eggs;
    [SerializeField] public Sprite Dendeng;

    /*public Image selectImage;
    public Image topImage;
    public Image bottomImage;*/

    private int choose;

    private void Start()
    {
        
    }

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
                AudioManager.Instance.DoAudio("click");
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
                AudioManager.Instance.DoAudio("click");
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
        Image selectImage = selectedIcon.GetComponent<Image>();
        Image topImage = topIcon.GetComponent<Image>();
        Image bottomImage = bottomIcon.GetComponent<Image>();

        switch (choose)
        {
            case 0:
                topButton2.text = "Fried Egg";
                topButton.text = "Dendeng";
                selectedButton.text = "Rendang";
                bottomButton.text = "Fried Chicken";
                bottomButton2.text = "Fish";

                topImage.sprite = Dendeng;
                selectImage.sprite = Rendang;
                bottomImage.sprite = Chicken;

                break;
            case 1:
                topButton2.text = "Dendeng";
                topButton.text = "Rendang";
                selectedButton.text = "Fried Chicken";
                bottomButton.text = "Fish";
                bottomButton2.text = "Veggies";

                topImage.sprite = Rendang;
                selectImage.sprite = Chicken;
                bottomImage.sprite = Fish;

                break;
            case 2:
                topButton2.text = "Rendang";
                topButton.text = "Fried Chicken";
                selectedButton.text = "Fish";
                bottomButton.text = "Veggies";
                bottomButton2.text = "Fried Egg";

                topImage.sprite = Chicken;
                selectImage.sprite = Fish;
                bottomImage.sprite = Veggies;

                break;
            case 3:
                topButton2.text = "Fried Chicken";
                topButton.text = "Fish";
                selectedButton.text = "Veggies";
                bottomButton.text = "Fried Egg";
                bottomButton2.text = "Dendeng";

                topImage.sprite = Fish;
                selectImage.sprite = Veggies;
                bottomImage.sprite = Eggs;

                break;
            case 4:
                topButton2.text = "Fish";
                topButton.text = "Veggies";
                selectedButton.text = "Fried Egg";
                bottomButton.text = "Dendeng";
                bottomButton2.text = "Rendang";

                topImage.sprite = Veggies;
                selectImage.sprite = Eggs;
                bottomImage.sprite = Dendeng;

                break;
            case 5:
                topButton2.text = "Veggies";
                topButton.text = "Fried Egg";
                selectedButton.text = "Dendeng";
                bottomButton.text = "Rendang";
                bottomButton2.text = "Fried Chicken";

                topImage.sprite = Eggs;
                selectImage.sprite = Dendeng;
                bottomImage.sprite = Rendang;

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