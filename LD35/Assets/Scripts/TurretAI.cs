using UnityEngine;
using System.Collections;

public class TurretAI : MonoBehaviour {

    public AudioClip hit;
    public Transform pivot;
    public Transform barrel;
    public GameObject Halo;
    float damping = 3;
    public float fireRate = 0.5f;
    public float damage = 50;
    Transform target;
    LineRenderer laser;
    bool bReadyToFire = true;
    bool isAlive = true;

    public AudioClip fire;
    AudioSource turretSounds;
    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        laser = GetComponent<LineRenderer>();
        laser.SetVertexCount(2);
        laser.SetWidth(0.1f, 0.25f);
        turretSounds = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (isAlive)
        {
            Vector3 lookPos1 = target.position - pivot.position;
            lookPos1.y = 0;
            Quaternion rotation1 = Quaternion.LookRotation(lookPos1);
            pivot.rotation = Quaternion.Slerp(pivot.rotation, rotation1, Time.deltaTime * damping);

            barrel.transform.rotation = Quaternion.Lerp(barrel.transform.rotation, target.rotation, damping * Time.deltaTime);
            barrel.localEulerAngles = new Vector3(barrel.localEulerAngles.x, 0, 0);
            RaycastHit hit;
            Physics.Raycast(barrel.position, (target.position - barrel.position).normalized, out hit, 50);

            if (hit.transform.tag == "Player" && bReadyToFire && isAlive)
            {
                StartCoroutine("FireLaser", hit.transform);
                //Debug.Log("GAME OVER");
            }
        }
    }

    IEnumerator FireLaser(Transform target) {
        Health targetHP = target.GetComponent<Health>();

        if (targetHP.HP > 0)
        {
            bReadyToFire = false;
            laser.enabled = true;
            laser.SetPosition(0, barrel.position);
            laser.SetPosition(1, target.position);
            turretSounds.clip = fire;
            turretSounds.Play();
            targetHP.HP -= damage;
            yield return new WaitForSeconds(0.2f);
            laser.enabled = false;

            yield return new WaitForSeconds(fireRate);

            bReadyToFire = true;
        }

    }

    public void Death()
    {
        if (isAlive)
        {
            GetComponent<AudioSource>().clip = hit;
            GetComponent<AudioSource>().Play();
            isAlive = false;
            Halo.SetActive(false);
        }
    }

}
