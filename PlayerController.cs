
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour{

	public float moveSpeed = 15f;// move speed at 8f per sec
	public float leftOrRightSpeed = 10f;// tilt speed at 10f per sec
	public bool collisionState;        //initial state of collision 
	public float speed = 90f;
	public const string ObjSpeedIncr = "Speed_Cube";
    public const string ObjSpeedDec = "Dec_Speed_Cube";
    public const string ObjScaleinc = "Scale_Inc";
    public const string ObjScaleDec = "Scale_Dc";
	public const string ObjMirror = "Mirror";
    private Rigidbody rb;
	public float hoverForce = 65f;
	public float hoverHeight = 2f;
	public string objName;
    

    void Awake()
    {
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;

    }
    


	// Update is called once per frame
    void Update()
    {


		//transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);//moves the lazer forward at movespeed per second

		if (Input.GetKey (KeyCode.A)) {
			transform.Translate (Vector3.left * leftOrRightSpeed * Time.deltaTime);//If A is pressed turn left at leftOrRightSpeed per second
		}


		if (Input.GetKey (KeyCode.D)) {
			transform.Translate (Vector3.right * leftOrRightSpeed * Time.deltaTime);//If D is pressed turn right at leftOrRightSpeed per second	
		}

    }

	//collision codes
    void OnCollisionEnter(Collision col)
    {
		switch (col.gameObject.name)
		{

		case ObjSpeedIncr:
			objName = ObjSpeedIncr;
			moveSpeed = 100;
			break;
		case ObjSpeedDec:
			objName = ObjSpeedDec;
			moveSpeed = 10;
			break;
		case ObjScaleinc:
			objName = ObjScaleinc;
			break;
		case ObjScaleDec:
			objName = ObjScaleDec;
			break;
		default:
			objName = " ";
			break;
		}

		if (col.gameObject.name == objName) {
			Debug.Log(objName);
			Destroy (col.gameObject);
		}

	}


	void FixedUpdate(){

		//hoverCode
		rb.AddForce (Vector3.forward * moveSpeed);

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





