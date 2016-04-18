using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

    public GameObject[] projectiles;
    public AudioClip shootClip;

    public GameObject shoot(ItemType projectile, Transform oriantation)
    {
        AudioSource.PlayClipAtPoint(shootClip, transform.position, 0.3f);
        return (GameObject)Instantiate(Resources.Load("Prefabs/" + projectiles[(int)projectile].name), oriantation.position, oriantation.rotation);
    }
}
