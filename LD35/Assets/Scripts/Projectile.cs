using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour {

    float timeoutDestroy = 20;
	// Use this for initialization
	void Start () {
        StartCoroutine("destroyAfter", timeoutDestroy);
    }

    IEnumerator destroyAfter(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Item" && other.tag != "Projectile" && other.tag != "ShootingZones")
        {
            GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
            player.GetComponent<Inputs>().projectiles.Remove(gameObject);

            if (other.tag == "Enemy")
            {
                other.GetComponent<TurretAI>().Death();
            }

            Destroy(gameObject);
        }
    }
}
