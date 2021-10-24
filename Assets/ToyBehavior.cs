using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 20;
    public bool suspended = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        IndependentMouseControls();
    }

    void IndependentMouseControls()
    {
        if(Input.GetMouseButtonDown(0))
        {
            CopiedMouseControls();
            suspended = true;
        }

        if(Input.GetMouseButtonUp(0))
        {
            ResetBoardRotation();
            suspended = false;
        }
    }
    private void OnMouseDrag()
    {
        
        suspended = true;
        CopiedMouseControls();
      
    }

    private void OnMouseUp()
    {
        suspended = false;
        ResetBoardRotation();
    }

    void ResetBoardRotation()
    {
        
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void CopiedMouseControls()
    {
        float rotationX = Input.GetAxis("Mouse X")*rotationSpeed*Mathf.Deg2Rad;
        float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad;
        transform.Rotate(Vector3.forward, -rotationX);
        transform.Rotate(Vector3.right, rotationY);

    }
}
