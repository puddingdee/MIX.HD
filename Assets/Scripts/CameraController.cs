using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    private Vector3 v = Vector3.zero;
    

    private float rotationX = 0f;
    private float rotationY = 155f;

    [SerializeField] private Vector3 camOffset = new Vector3(0f, 0f, 0f);

    [SerializeField] private Transform focalPoint;
    [SerializeField] float minVerticalAngle = -65;
    [SerializeField] float maxVerticalAngle = 65;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {

            rotationX += Input.GetAxis("Mouse Y");
            rotationY += Input.GetAxis("Mouse X");
            rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

            Quaternion targetRotation = Quaternion.Euler(-rotationX, rotationY, 0);



            

            transform.rotation = targetRotation;
     


    }

    //public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);

}
