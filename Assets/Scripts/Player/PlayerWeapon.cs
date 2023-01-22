using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    bool fDown;
    bool rDown;
    bool isFireReady;
    [HideInInspector] public bool isReloading;
    float fireDelay;
    Animator animator;
    PlayerItem playerItem;
    PlayerMove playerMove;

    void GetInput()
    {
        fDown = Input.GetButton("Fire1");
        rDown = Input.GetButtonDown("Reload");
    }

    void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
        playerItem = GetComponentInParent<PlayerItem>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Attack();
        Reload();
    }


    void Attack()
    {
        // equipWeapon = GameObject.Find("Player").GetComponent<PlayerItem>().equipWeapon;
        if (playerItem.equipWeapon == null) return;

        fireDelay += Time.deltaTime;
        isFireReady = playerItem.equipWeapon.rate < fireDelay;

        if (fDown && isFireReady && !isReloading && !playerMove.isDodge)
        {
            playerItem.equipWeapon.Use();
            animator.SetTrigger(playerItem.equipWeapon.type == Weapon.Type.Melee ? "doSwing" : "doShot");
            fireDelay = 0;
        }
    }

    void Reload()
    {
        if (playerItem.equipWeapon == null) return;

        if (playerItem.equipWeapon.type == Weapon.Type.Melee) return;

        if (playerItem.ammo == 0) return;

        if (rDown && isFireReady && !isReloading)
        {
            animator.SetTrigger("doReload");
            isReloading = true;
            Debug.Log("Reload");

            Invoke("ReloadOut", 3f);
        }
    }

    void ReloadOut()
    {
        int reAmmo = playerItem.ammo < playerItem.equipWeapon.maxAmmo ? playerItem.ammo : playerItem.equipWeapon.maxAmmo;
        playerItem.equipWeapon.curAmmo = reAmmo;
        playerItem.ammo -= reAmmo;
        isReloading = false;
        // 장전 갯수 로직 정확하게 바꾸기
    }
}
