using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIButtonBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float cost;
    public TextMeshProUGUI costText;
    public CurrencyBank myCurrency;
    public GameManager gm;
    public float costIncrease = 2;
    public float capacityIncrease = 1;
    public float capacityInflation = 0.25f;
    
    
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        costText.text = "$ " + cost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryToUpgrade()
    {
        if(gm.SpendMoney(cost))
        {
            
            if (myCurrency.myCurrency == PlinkooBehavior.Targets.food)
            {
                myCurrency.DepositResource(PlinkooBehavior.Targets.food,capacityIncrease);
                
            }
            if (myCurrency.myCurrency == PlinkooBehavior.Targets.water)
            {
                myCurrency.DepositResource(PlinkooBehavior.Targets.water, capacityIncrease);

            }
            if (myCurrency.myCurrency == PlinkooBehavior.Targets.potty)
            {
                myCurrency.UseResource(PlinkooBehavior.Targets.potty, capacityIncrease);          
                myCurrency.capacity += capacityIncrease;
            }
            
            cost += costIncrease;
            capacityIncrease += capacityInflation;
            costText.text = cost.ToString();

        }
    }
}
