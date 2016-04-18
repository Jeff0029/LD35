using UnityEngine;
using System.Collections;

public class ItemGrabber : MonoBehaviour {

    public AudioClip pickup;
    bool [] currentTypes = new bool[ItemType.GetNames(typeof(ItemType)).Length];
    // Use this for initialization
    void Start () {
        for (int i = 0; i < currentTypes.Length; i++)  
        {
            currentTypes[i] = false;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnTriggerEnter(Collider collided)
    {
        Pickup pickup = collided.gameObject.GetComponent<Pickup>();
        if (collided.tag == "Item" && !pickup.isPickedUp)
        {
            pickup.isPickedUp = true;
            PickupItem(collided.transform, pickup.thisRotationWhenHold);
        }

    }

    void PickupItem(Transform item, Vector3 rotation)
    {
        item.SetParent(transform.FindChild("FirstPersonCharacter").FindChild("WeaponHolder"));
        item.localEulerAngles = rotation;
        item.localPosition = new Vector3(0, 0, 0);
        GetComponent<AudioSource>().PlayOneShot(pickup);
        GetComponent<Inputs>().ShowAllBut(item.name);
        GetComponent<Inputs>().currentDesiredBulletType = item.GetComponent<Pickup>().itemType;
    }
}
