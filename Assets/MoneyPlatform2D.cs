using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPlatform2D : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager2D manager2D;
    public float moneyValue = 10;

    void Start()
    {
        manager2D = GameObject.FindGameObjectWithTag("manager2D").GetComponent<GameManager2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("plinkoo2D"))
        {
            manager2D.addMoney(moneyValue);
            collision.collider.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
