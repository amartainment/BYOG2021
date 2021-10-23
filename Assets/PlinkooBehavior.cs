using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlinkooBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject plinkoo2D;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EntryPortal>() != null)
        {
            other.GetComponent<EntryPortal>().RunPortal(gameObject,plinkoo2D);
        }
    }
}
