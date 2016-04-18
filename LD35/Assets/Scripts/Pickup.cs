using UnityEngine;
using System.Collections;
public enum ItemType
{
    boomerang = 0,
    Gun,
    Bow
}

public class Pickup : MonoBehaviour {
    public bool isPickedUp = false;
    public ItemType itemType;

    private Vector3[] itemRotationWhenHold = { new Vector3(280, 270, 90), new Vector3(0, 90, 260), new Vector3(270, 0, 0) };
    public Vector3 thisRotationWhenHold;
    // Use this for initialization
    void Start () {
        thisRotationWhenHold = itemRotationWhenHold[(int)itemType];
    }
}
