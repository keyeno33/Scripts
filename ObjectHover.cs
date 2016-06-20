using UnityEngine;
using System.Collections;

public class ObjectHover : MonoBehaviour {

	private Rigidbody rb;
	public float hoverForce = 65f;
	public float hoverHeight = 2f;

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

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
