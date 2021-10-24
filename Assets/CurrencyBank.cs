using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CurrencyBank : MonoBehaviour
{
    public PlinkooBehavior.Targets myCurrency;
    public float quantity = 1f;
    public float capacity = 1f;
    public TextMeshProUGUI inventoryText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        inventoryText.text = quantity.ToString() + " " + "/ " + capacity.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool UseResource(PlinkooBehavior.Targets currency)
    {
        if (currency == myCurrency)
        {
            if (quantity > 0)
            {
                quantity--;
                inventoryText.text = quantity.ToString() + " " + "/ " + capacity.ToString();
                return true;
            }
            else
            {
                return false;
            }
        } else
        {
            return false;
        }
    }

    public bool DepositResource(PlinkooBehavior.Targets currency)
    {
        if (currency == myCurrency)
        {
            if (quantity < capacity)
            {
                quantity++;
                inventoryText.text = quantity.ToString() + " " + "/ " + capacity.ToString();
                return true;
            } else
            {
                return false;
            }
        } else
        {
            return false;
        }
    }
}
