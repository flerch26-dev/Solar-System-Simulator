using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewerMovement : MonoBehaviour
{
    Vector3 position = new Vector3(0,0,0);

    public float movementSpeed = 100f;

    private float x;
    private float y;
    private Vector3 rotateValue;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        //move player forward
        if(Input.GetKey("w"))
        {
            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        }
        //move player backwards
        if(Input.GetKey("s"))
        {
            transform.position -= transform.forward * Time.deltaTime * movementSpeed;
        }
        //move player to the right
        if(Input.GetKey("d"))
        {
           transform.position += transform.right * Time.deltaTime * movementSpeed;
        }
        //move player to the left
        if(Input.GetKey("a"))
        {
           transform.position -= transform.right * Time.deltaTime * movementSpeed;
        }
        //move player up
        if(Input.GetKey("q"))
        {
            transform.position += transform.up * Time.deltaTime * movementSpeed;
        }
        //move player down
        if(Input.GetKey("e"))
        {
            transform.position -= transform.up * Time.deltaTime * movementSpeed;
        }

        y = Input.GetAxis("Mouse X");
        x = Input.GetAxis("Mouse Y");
        rotateValue = new Vector3(x, y * -1, 0);
        transform.eulerAngles = transform.eulerAngles - rotateValue;
    }
}
