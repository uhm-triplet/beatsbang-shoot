using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    public GameObject[] weapons;
    public int hasWeapon = -1;
    bool swapped;

    public GameObject[] grenades;
    public int hasGrenades;

    public int ammo;
    public int health;


    public int maxAmmo;
    public int maxHealth;
    public int maxHasGrenades;

    public int kill;
    public int death;

    GameObject nearObject;
    [HideInInspector] public Weapon equipWeapon;
    Animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Interaction();
        Swap();
    }

    void Interaction()
    {
        if (nearObject != null)
        {
            if (nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                hasWeapon = item.value;
                swapped = true;
                Destroy(nearObject);
            }
        }
    }

    void Swap()
    {
        if ((hasWeapon == 0 || hasWeapon == 1 || hasWeapon == 2) && swapped)
        {
            if (equipWeapon != null) equipWeapon.gameObject.SetActive(false);
            equipWeapon = weapons[hasWeapon].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);

            animator.SetTrigger("doSwap");
            swapped = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            Item item = other.GetComponent<Item>();
            hasWeapon = item.value;
            swapped = true;
            Destroy(other.gameObject);
        }

        if (other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch (item.type)
            {
                case Item.Type.Ammo:
                    ammo += item.value;
                    if (ammo > maxAmmo)
                        ammo = maxAmmo;
                    break;
                case Item.Type.Heart:
                    health += item.value;
                    if (health > maxHealth)
                        health = maxHealth;
                    break;
                case Item.Type.Grenade:
                    if (hasGrenades == maxHasGrenades)
                        break;
                    grenades[hasGrenades].SetActive(true);
                    hasGrenades += item.value;
                    break;
            }
            Destroy(other.gameObject);
        }

    }
}
