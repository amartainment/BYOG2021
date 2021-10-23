using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 20;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        CopiedMouseControls();
    }
    void CopiedMouseControls()
    {
        float rotationX = Input.GetAxis("Mouse X")*rotationSpeed*Mathf.Deg2Rad;
        float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad;
        transform.Rotate(Vector3.forward, -rotationX);
        transform.Rotate(Vector3.right, rotationY);

    }
}
