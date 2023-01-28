using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    bool fDown;
    bool rDown;
    bool gDown;
    bool isFireReady;
    [HideInInspector] public bool isReloading;
    float fireDelay;

    public GameObject grenadeObj;
    [SerializeField] Transform grenadePos;
    Animator animator;
    PlayerItem playerItem;
    PlayerMove playerMove;
    PlayerAim playerAim;

    void GetInput()
    {
        fDown = Input.GetButton("Fire1");
        gDown = Input.GetButton("Grenade");
        rDown = Input.GetButtonDown("Reload");
    }

    void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
        playerItem = GetComponentInParent<PlayerItem>();
        playerAim = GetComponentInParent<PlayerAim>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Attack();
        Grenade();
        Reload();
    }


    void Attack()
    {
        // equipWeapon = GameObject.Find("Player").GetComponent<PlayerItem>().equipWeapon;
        if (playerItem.equipWeapon == null || playerItem.equipWeapon.curAmmo == 0) return;

        fireDelay += Time.deltaTime;
        isFireReady = playerItem.equipWeapon.rate < fireDelay;

        if (fDown && isFireReady && !isReloading && !playerMove.isDodge)
        {
            playerItem.equipWeapon.Use();
            animator.SetTrigger(playerItem.equipWeapon.type == Weapon.Type.Melee ? "doSwing" : "doShot");
            fireDelay = 0;
        }
    }

    void Grenade()
    {
        if (playerItem.hasGrenades == 0) return;
        if (gDown && !isReloading)
        {
            grenadePos.LookAt(playerAim.aimPos);
            GameObject instantGrenade = Instantiate(grenadeObj, grenadePos.position, grenadePos.rotation);
            Rigidbody grenadeRigid = instantGrenade.GetComponent<Rigidbody>();
            grenadeRigid.AddForce(grenadePos.forward * 20, ForceMode.Impulse);
            grenadeRigid.AddForce(grenadePos.up * 10, ForceMode.Impulse);
            grenadeRigid.AddTorque(Vector3.back * 10, ForceMode.Impulse);

            playerItem.hasGrenades -= 1;
            playerItem.grenades[playerItem.hasGrenades].SetActive(false);
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


        int requiredAmmo = playerItem.equipWeapon.maxAmmo - playerItem.equipWeapon.curAmmo;
        int reAmmo = playerItem.ammo < requiredAmmo ? playerItem.ammo : requiredAmmo;
        playerItem.equipWeapon.curAmmo += reAmmo;
        playerItem.ammo -= reAmmo;
        isReloading = false;
        // 장전 갯수 로직 정확하게 바꾸기
    }
}
