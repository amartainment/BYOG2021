using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPortal : MonoBehaviour

{
    public GameManager2D manager2D;
    List<GameObject> consumedBlocks;
    public Material inUseIndicator;
    public Material standardMaterial;
    public Renderer myRenderer;
    public int limit = 1;
    public int currentUse = 0;
    // Start is called before the first frame update
    void Start()
    {
        consumedBlocks = new List<GameObject>();
        standardMaterial = myRenderer.material;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RunPortal(GameObject plinkoo3D, GameObject plinkoo2D)
    {
        if (currentUse < limit)
        {
            manager2D.StartMatch(plinkoo2D);
            Consume3DBlock(plinkoo3D);
            currentUse++;
        }

    }

    void Consume3DBlock(GameObject plinkoo3D)
    {
        Rigidbody plinkoo_rb = plinkoo3D.GetComponent<Rigidbody>();
        PlinkooBehavior behavior = plinkoo3D.GetComponent<PlinkooBehavior>();
        behavior.GoToSuspend();
        plinkoo_rb.velocity = Vector3.zero;
        plinkoo_rb.transform.position = transform.position;
        consumedBlocks.Add(plinkoo3D);
        plinkoo3D.SetActive(false);
        myRenderer.material = inUseIndicator;
        manager2D.plinkoo3D = plinkoo3D;

    }

    public void ResetPortal()
    {
        myRenderer.material = standardMaterial;
        
    }

    public void RecordExit()
    {
        currentUse--;
        if(currentUse<=0)
        {
            ResetPortal();
        }
    }

}
