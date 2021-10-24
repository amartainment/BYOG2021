using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2D : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject plinkoo2D;
    public Transform world3D;
    public Transform world2D;
    public Transform startPosition;
    private float gravityMagnitude;
    public GameManager manager3D;
    public GameObject plinkoo3D;
    public Transform exitBlock;
    public EntryPortal entryPortal;
    void Start()
    {
        gravityMagnitude = Physics2D.gravity.magnitude;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartMatch(GameObject plinkoo)
    {
        plinkoo2D = Instantiate(plinkoo, startPosition.position, Quaternion.identity, world2D);
    }

    void CalculateRotationForce()
    {

    }

    public void addMoney(float amount)
    {
        manager3D.AddMoney(amount);
        PushOut3DPlinkoo();
    }

    public void PushOut3DPlinkoo()
    {
        plinkoo3D.transform.position = exitBlock.transform.position;
        plinkoo3D.SetActive(true);
        
        plinkoo3D.GetComponent<PlinkooBehavior>().GoToWait();
        
        entryPortal.RecordExit();
    }
}
