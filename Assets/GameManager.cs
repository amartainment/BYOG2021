using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float money = 5;
    public TextMeshProUGUI moneyUI;
    
    void Start()
    {
        DisplayMoneyUI();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void DisplayMoneyUI ()
    {
        moneyUI.text = "$ "+money.ToString();
    }

    public void SpendMoney(float amount)
    {
        if(amount < money)
        {
            money -= amount;
            DisplayMoneyUI();
            
        } else
        {
            Debug.Log("Not enough money!");
            
        }
    }

    public void AddMoney(float amount)
    {
        money += amount;
        DisplayMoneyUI();
    }
}
