using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {

    public float speed = 17;
	// Update is called once per frame
	void FixedUpdate () {
	    transform.Rotate(new Vector3(0, 0, speed));
	}
}
