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
    public const string Mirror = "Mirror";
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
    private Vector3 forceDirection;
    private Rigidbody rb;
    private Vector3 rotated_right = new Vector3(0,90,0);
    private bool rotated_left;
    private bool lessThanY = false;
    private bool greaterThanY = false;
    private bool objRotation false;

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
            case Mirror:
                    changeDir(col);
                    transform.Rotate(directionRight);
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

    void changeDir(Collision col)
    {
        //call checkRotateCollider 
        checkRotateCollider(col);
        //check return of collider rotation
        checkRotateCollider(col);
        // rotate right if collider is 45 degrees or more
        //call checkRotateObject
        if (greaterThanY)
        {
            col.transform.rotation.eulerAngles.y = 90;
        }
        // rotate left if collider is 45 degrees or less
        else if (lessThanY)
        {
            col.transform.rotation.eulerAngles.y = -90;
        }


        // decrease by 180 if game object is 90 or greater and mirror is reflecting left
        // increase by 180 if game object is less than 90 and mirror is reflecting right
        //Change direction 
    }

    bool checkRotateCollider(Collision col)
    {
        // Check if col object's rotation is 45 degrees or less.
        if(col.transform.localEulerAngles.y >= 90)
        {
            return greaterThanY = true;
        }
        else if(col.transform.localEulerAngles.y <= -90)
        {
            return lessThanY = true;
        }
        return false;
    }

    bool checkRotateObject()
    {
        //if game object rotation y is greater than 90 then return y
        if (gameObject.transform.rotation.eulerAngles.y < 90)
        {

        }
        //if game object rotation y if less than 90 then return y
        else if (gameObject.transform.rotation.eulerAngles.y <= -90)
        {

        }
        return objRotation = true;
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
