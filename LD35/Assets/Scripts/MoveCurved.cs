using UnityEngine;
using System.Collections;

public class MoveCurved : MonoBehaviour {
    public float speed;
    public float curvingSpeed;
    public bool isLeft;
    int left;
    Rigidbody rBody;

	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody>();
        left = isLeft ? -1 : 1;
        curvingSpeed *= left;

        // Only rotate with the Y axis
        transform.rotation = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up);
    }

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, curvingSpeed * Time.deltaTime,0));
    }
}
