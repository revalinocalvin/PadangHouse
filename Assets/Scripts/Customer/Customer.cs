using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    CustomerPathing customerPathing;
    CustomerFoodMerged customerFood;

    public int customerStarsAmount = 3;
    [SerializeField] private float customerPatience = 15f;
    public float patienceTimer;
    public bool patienceTimerSet;
    public bool stillPatient;    
    public bool angry = true;
    public Table table;

    [Header("Sprites")]
    public GameObject customerOrder;
    public GameObject FoodOrange;
    public GameObject FoodBlue;
    public GameObject FoodRed;
    public GameObject FoodVeggies;
    public GameObject FoodEggs;
    public GameObject FoodDendeng;
    public GameObject orderIcon;

    public GameObject[] customerStars;

    public AudioSource audioSource;
    public AudioClip[] audios;
    private AudioClip audio;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Audio();
    }
    void Start()
    {
        stillPatient = true;
        patienceTimer = Time.time;
        patienceTimerSet = false;

        customerPathing = GetComponent<CustomerPathing>();
        customerFood = GetComponent<CustomerFoodMerged>();
        customerOrder.SetActive(false);
    }

    void Update()
    {
        if (customerPathing.onChair == true && patienceTimerSet == false && this.CompareTag("Customer"))
        {
            patienceTimerSet = true;
            patienceTimer = Time.time + customerPatience;
        }

        if (customerFood.receivedFood == false)
        {
            Patience();
        }

        OrderSign();
    }

    void Audio()
    {
        int i = Random.Range(0, 4);
        audio = audios[i];

        audioSource.clip = audio;
        audioSource.Play();
    }

    void OrderSign()
    {
        if (customerFood.order == false && customerPathing.onChair == true)
        {
            customerOrder.SetActive(true);
        }
        else
        {
            customerOrder.SetActive(false);
        }
    }

    public void Foodsign(string tag)
    {
        if (tag == "Food1")
        {
            FoodRed.SetActive(true) ;
            orderIcon = FoodRed;
        }
        else if (tag == "Food2")
        {
            FoodBlue.SetActive(true);
            orderIcon = FoodBlue;
        }
        else if (tag == "Food3")
        {
            FoodOrange.SetActive(true);
            orderIcon = FoodOrange;
        }
        else if (tag == "Food4")
        {
            FoodVeggies.SetActive(true);
            orderIcon = FoodVeggies;
        }
        else if (tag == "Food5")
        {
            FoodEggs.SetActive(true);
            orderIcon = FoodEggs;
        }
        else if (tag == "Food6")
        {
            FoodDendeng.SetActive(true);
            orderIcon = FoodDendeng;
        }
    }

    public void Patience()
    {
        
        if (!stillPatient)
        {
            Angry();            

            if (this.gameObject.CompareTag("CustomerGroup"))
            {
                table.CustomerAngry(this);
            }                      
        }
        else if (Time.time >= patienceTimer && patienceTimerSet == true && customerFood.receivedFood == false)
        {
            patienceTimer = Time.time + customerPatience;
            customerStars[customerStarsAmount - 1].SetActive(false);
            customerStars[customerStarsAmount - 1] = null;
            customerStarsAmount -= 1;

            if (customerStarsAmount == 0)
            {
                stillPatient = false;

                if (customerPathing.table == 1)
                { 
                    CustomerManager.Instance.chairAvailable[customerPathing.chairNumber - 1] = true;
                    CustomerManager.Instance.CheckTableAvailable(1);
                }
                else if (customerPathing.table == 2)
                {
                    CustomerManager.Instance.chairAvailable2[customerPathing.chairNumber - 5] = true;
                    CustomerManager.Instance.CheckTableAvailable(2);
                }
                else
                {
                    CustomerManager.Instance.chairAvailable3[customerPathing.chairNumber - 9] = true;
                }
            }
        }
    }

    public void Angry()
    {
        customerPathing.eatingFinished = true;

        this.GetComponent<Transform>().transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);


        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.red;

        if (customerFood.food != null)
        {
            Destroy(customerFood.food);
        }

        customerFood.receivedFood = true;
        CustomerManager.Instance.angryCustomers++;
        customerStarsAmount = 0;

        if (customerPathing.table == 1)
        {
            CustomerManager.Instance.chairAvailable[customerPathing.chairNumber - 1] = true;
            CustomerManager.Instance.CheckTableAvailable(1);
        }
        else if (customerPathing.table == 2)
        {
            CustomerManager.Instance.chairAvailable2[customerPathing.chairNumber - 5] = true;
            CustomerManager.Instance.CheckTableAvailable(2);
        }

    }
}
