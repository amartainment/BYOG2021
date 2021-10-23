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
}
