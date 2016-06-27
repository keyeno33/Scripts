using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 0, 60) * Time.deltaTime);
	}
}
