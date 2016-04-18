using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Transform startingPoint = GameObject.Find("StartingPoint").transform;
        transform.position = startingPoint.position;
        transform.rotation = startingPoint.localRotation;
    }
}
