using System;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //public bool collisionState; 
    public float speed = 90f;
    public string[] specObjects = new string[] { "Speed_Cube", "Dec_Speed_Cube", "Scale_Inc", "Scale_Dc" };
    public const string objSpeedIncr = "Speed_Cube";
    public const string objSpeedDec = "Dec_Speed_Cube";
    public const string objScaleinc = "Scale_Inc";
    public const string objScaleDec = "Scale_Dc";
    public const string objMirrorRight = "MirrorRight";
    public const string objMirrorLeft = "MirrorLeft";
    public const string walls = "Wall";
    public string objCollision;
    public float hoverForce = 65f;
    public float hoverHeight = 2f;
    public string objName;
    //public Mirror = GameObject Mirror;

    private int hitCount;
    private float leftOrRightSpeed = 10f;// tilt speed at 10f per sec
    private float moveSpeed = 45f;// move speed at ... per sec
    private Vector3 directionLeft = new Vector3(0, -90, 0);
    private Vector3 directionRight = new Vector3(0, 90, 0);
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        //rb.useGravity = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        //transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);//moves the lazer forward at movespeed per second

        if (Input.GetKey(KeyCode.A)) {
            transform.Translate(Vector3.left* leftOrRightSpeed * Time.deltaTime);//If A is pressed turn left at leftOrRightSpeed per second
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.Translate(Vector3.right * leftOrRightSpeed * Time.deltaTime);//If D is pressed turn right at leftOrRightSpeed per second	
        }
    }

    //collision codes
    string OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.name)
        {
            case objMirrorRight:
                    transform.Rotate(directionRight);
                    //transform.Translate(new Vector3(speed,0,0));
                    //Movement(new Vector3(10,0,0));
                break;

            case objMirrorLeft:
                    transform.Rotate(directionLeft);
                    //transform.Translate(new Vector3(speed,0,0));
                    //Movement(new Vector3(10,0,0));

                break;

            case objSpeedIncr:
                moveSpeed = 100;
                Destroy(col.gameObject);
                break;

            case objSpeedDec:
                moveSpeed -=100;
                Destroy(col.gameObject);
                break;

            case objScaleinc:
                //transform.localScale += new Vector3(1.5f, 1.5f, 1.5f);
                Debug.Log(objScaleinc);
                Destroy(col.gameObject);
                break;

            case objScaleDec:
                //transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
                Debug.Log(objScaleDec);
                Destroy(col.gameObject);
                break;

            case walls:
                Destroy(gameObject);
                break;

            default:
                Debug.Log(col.gameObject.name);
                break;
        }
        return objCollision = col.gameObject.name;
    }

    Vector3 Movement(Vector3 direction) 
    {
        rb.AddForce(direction);
      //  transform.Translate(direction * Time.deltaTime);
        return direction;
    }

    void FixedUpdate() {

        //hoverCode
        /*if (objCollision == "MirrorRight" && hitCount < 1)
        {
            transform.Rotate(directionRight);
            //transform.Translate(new Vector3(speed,0,0));
            //Movement(new Vector3(10,0,0));
            hitCount++;
        }
        else if(objCollision == "MirrorLeft" && hitCount < 1)
        {
            transform.Rotate(directionLeft);
            // Movement(Vector3.left);
            hitCount++;
        }
        else
        {
            //Movement(new Vector3(0,0,moveSpeed));
        }*/

        //Movement(Vector3.forward);

        Ray ray = new Ray (transform.position, -transform.up);
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit, hoverHeight))
		{
			float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
			Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
			rb.AddForce(appliedHoverForce, ForceMode.Acceleration);
		}
	}
}
