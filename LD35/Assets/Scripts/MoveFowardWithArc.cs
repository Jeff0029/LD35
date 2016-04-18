using UnityEngine;
using System.Collections;

public class MoveFowardWithArc : MonoBehaviour {

    public float speed;
    Rigidbody rBody;

    Vector3 pointingDirection = new Vector3();
	// Use this for initialization
	void Start () {
        rBody = gameObject.GetComponent<Rigidbody>();
        rBody.AddRelativeForce(Vector3.forward * speed);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.LookAt(transform.position + rBody.velocity);
    }
}
