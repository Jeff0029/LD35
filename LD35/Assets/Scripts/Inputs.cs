using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class Inputs : MonoBehaviour {

    public Hashtable keyMapping = new Hashtable();
    public List<GameObject> projectiles = new List<GameObject>();
    private GameObject activeWeapon;
    public bool bCanFire = false;
    public ItemType currentDesiredBulletType;
    bool lastMouseLeftClick = false;

    public AudioClip change;
    public AudioClip switchClip;
    AudioSource source;

    // Use this for initialization
    void Start() {
        keyMapping.Add("Boomerang",KeyCode.Alpha1);
        keyMapping.Add("Gun", KeyCode.Alpha2);
        keyMapping.Add("Bow", KeyCode.Alpha3);
        keyMapping.Add("ShootLeft", KeyCode.Mouse0);
        keyMapping.Add("ShootRight", KeyCode.Mouse1);
        keyMapping.Add("PreviousProjectile", KeyCode.Q);
        keyMapping.Add("NextProjectile", KeyCode.E);

        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (DictionaryEntry entry in keyMapping)
        {
            if (Input.GetKeyDown((KeyCode)entry.Value))
            {
                Invoke((string)entry.Key, 0);
            }
        }
    }

    void Boomerang()
    {
        currentDesiredBulletType = ItemType.boomerang;
        if (ShowAllBut("Boomerang"))
        {
            source.clip = switchClip;
            source.Play();
        }
    }

    void Gun()
    {
        currentDesiredBulletType = ItemType.Gun;
        if (ShowAllBut("Gun"))
        {
            source.clip = switchClip;
            source.Play();
        }
        
    }

    void Bow()
    {
        currentDesiredBulletType = ItemType.Bow;
        if (ShowAllBut("Bow"))
        {
            source.clip = switchClip;
            source.Play();
        }
        
    }

    void Shoot(bool isLeftClick = true)
    {
        if (bCanFire && activeWeapon != null)
        {
            GameObject newProjectile = gameObject.GetComponent<Shooting>().shoot(activeWeapon.GetComponent<Pickup>().itemType, activeWeapon.transform.FindChild("Orientation"));
            projectiles.Add(newProjectile);
            if (newProjectile.GetComponent<MoveCurved>() != null)
            {
                newProjectile.GetComponent<MoveCurved>().isLeft = isLeftClick;
            }
            
        }
    }

    void ShootRight()
    {
        lastMouseLeftClick = false;
        Shoot(false);
    }

    void ShootLeft()
    {
        lastMouseLeftClick = true;
        Shoot(true);
    }
    
    void PreviousProjectile()
    {

        if (activeWeapon == null)
            return;

        int childIndex = (int)currentDesiredBulletType;
        int childCount = ItemType.GetNames(typeof(ItemType)).Length;
        if (childCount <= 1)
        {
            // Nothing to switch to
            return;
        }

        int prevIndex = childIndex == 0 ? childCount-1 : childIndex - 1;

        source.clip = change;
        source.Play();
        ChangeAllProjectiles(GetPrefabName((ItemType)prevIndex));
    }

    void NextProjectile()
    {
        if (activeWeapon == null)
            return;

        int childIndex = (int)currentDesiredBulletType;
        int childCount = ItemType.GetNames(typeof(ItemType)).Length;
        if (childCount <= 1)
        {
            // Nothing to switch to
            return;
        }

        int nextIndex = childIndex+1 == childCount ? 0 : childIndex + 1;

        source.clip = change;
        source.Play();
        ChangeAllProjectiles(GetPrefabName((ItemType)nextIndex));
    }

    void ChangeAllProjectiles(string typeToChange) {
         
        List<GameObject> oldList = new List<GameObject>(projectiles);
        foreach (GameObject projectile in oldList)
        {
            Transform coords = projectile.transform;
            GameObject newProjectile = (GameObject)Instantiate(Resources.Load("Prefabs/" + typeToChange), coords.position, coords.rotation);
            projectiles.Add(newProjectile);

            if (newProjectile.GetComponent<MoveCurved>() != null)
            {
                newProjectile.GetComponent<MoveCurved>().isLeft = lastMouseLeftClick;
            }
        }
        
        for (int i = 0; i < oldList.Count; i++) {
            projectiles.Remove(oldList[i]);
            Destroy(oldList[i]);
        }
    }

    string GetPrefabName(ItemType type)
    {
        switch (type)
        {
            case ItemType.boomerang:
                return "BoomerangProjectile";
                break;
            case ItemType.Gun:
                return "Bullet";
                break;
            case ItemType.Bow:
                return "Arrow";
                break;
        }
        return "";
    }

    public bool ShowAllBut(string objNameToShow) {
        Transform wpHolder = transform.FindChild("FirstPersonCharacter").FindChild("WeaponHolder");

        // We did not find the weapon.  don't do anything
        if (wpHolder.FindChild(objNameToShow) == null)
        {
            return false;
        }

        for (int i = 0; i < wpHolder.childCount; i++)
        {
            GameObject curChild = wpHolder.GetChild(i).gameObject;
            curChild.SetActive(false);

            if (curChild.name == objNameToShow)
            {
                curChild.SetActive(true);
                activeWeapon = curChild;
            }
        }
        return true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ShootingZones")
        {
            bCanFire = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "ShootingZones")
        {
            bCanFire = false;
        }
    }
}
