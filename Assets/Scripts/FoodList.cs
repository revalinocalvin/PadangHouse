using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodList : MonoBehaviour
{
    public static FoodList instance;
    public static FoodList Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<FoodList>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(FoodList).Name;
                    instance = obj.AddComponent<FoodList>();
                }
            }
            return instance;
        }
    }

    private List<string> foodList = new List<string>();
    private int randomFoodNumber;

    void Start()
    {
        foodList.Insert(0, "Food1"); //Add Food1 to index 0
        foodList.Insert(1, "Food2"); //Add Food2 to index 1
        foodList.Insert(2, "Food3"); //Add Food3 to index 2
        /*foodList.Insert(3, "Food4");
        foodList.Insert(4, "Food5");
        foodList.Insert(5, "Food6");*/
    }

    public string GetRandomFood()
    {
        randomFoodNumber = Random.Range(0, 3); //Get random number 0-2
        return foodList[randomFoodNumber];
    }
}
