using System;
using UnityEngine;
using System.Collections;

public class PlayerController1 : MonoBehaviour
{

    public float moveSpeed = 15f;// move speed at 8f per sec
    public float leftOrRightSpeed = 10f;// tilt speed at 10f per sec
    public bool collisionState;        //initial state of collision 
    public float speed = 90f;
    public string[] specObjects = new string[] { "Speed_Cube", "Dec_Speed_Cube", "Scale_Inc", "Scale_Dc" };
    public const string ObjSpeedIncr = "Speed_Cube";
    public const string ObjSpeedDec = "Dec_Speed_Cube";
    public const string ObjScaleinc = "Scale_Inc";
    public const string ObjScaleDec = "Scale_Dc";
    public const string ObjMirror = "Mirror";
    private Rigidbody rb;
    public float hoverForce = 65f;
    public float hoverHeight = 2f;
    public string objName;
    public GameObject Mirror;
    Vector3 forceDirection = Vector3.forward;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {

        //transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);//moves the lazer forward at movespeed per second

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * leftOrRightSpeed * Time.deltaTime);//If A is pressed turn left at leftOrRightSpeed per second
        }


        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * leftOrRightSpeed * Time.deltaTime);//If D is pressed turn right at leftOrRightSpeed per second	
        }

    }

    //collision codes
    void OnCollisionEnter(Collision col)
    {


        switch (col.gameObject.name)
        {

            case ObjMirror:

                gameObject.transform.Rotate(0, 90, 0);
                changeDir(col);
                moveSpeed = 60;
                break;

            case ObjSpeedIncr:
                moveSpeed = 100;
                Destroy(col.gameObject);
                break;

            case ObjSpeedDec:
                moveSpeed = 40;
                Destroy(col.gameObject);
                break;

            case ObjScaleinc:
                //transform.localScale += new Vector3(1.5f, 1.5f, 1.5f);
                Debug.Log(ObjScaleinc);
                Destroy(col.gameObject);
                break;

            case ObjScaleDec:
                //transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
                Debug.Log(ObjScaleDec);
                Destroy(col.gameObject);
                break;

            default:
                printObjName(col);
                break;
        }
    }
    void changeDir(Collision col)
    {
        if(transform.rotation.eulerAngles.x >= 45)
        {
            //function for check rotation.
            forceDirection = Vector3.right;
        }
        else if(transform.rotation.eulerAngles.y <= 45){
            //function for check rotation
            forceDirection = Vector3.left;
        }
    }

    void printObjName(Collision col)
    {
        for (int i = 0; i < specObjects.Length; i++)
        {
            if (col.gameObject.name == specObjects[i])
            {
                Debug.Log(specObjects[i]);
            }
        }
    }
    // check rotation of laser 
    void FixedUpdate()
    {
        //hoverCode
        rb.AddForce(forceDirection * moveSpeed);

        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
            rb.AddForce(appliedHoverForce, ForceMode.Acceleration);
        }
    }

    void lazerReflection()
    {

    }
}

