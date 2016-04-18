using UnityEngine;
using System.Collections;

public class MoveFoward : MonoBehaviour {

    public float speed;
    Rigidbody rBody;
	// Use this for initialization
	void Start () {
        rBody = gameObject.GetComponent<Rigidbody>();
        rBody.AddRelativeForce(0, 0, speed);
    }
}
