using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerItem playerItem;
    public EnemyTest enemy;
    public float playTime = 754f;

    public GameObject gamePanel;

    public TextMeshProUGUI timerTxt;
    public TextMeshProUGUI killTxt;
    public TextMeshProUGUI deathTxt;

    public TextMeshProUGUI playerHealthTxt;
    public TextMeshProUGUI playerAmmoTxt;
    // public TextMeshProUGUI playerCoinTxt;

    public Image hammerImg;
    public Image handGunImg;
    public Image subMachineGunImg;
    public TextMeshProUGUI weaponAmmoTxt;
    public TextMeshProUGUI grenadeCountTxt;

    public RectTransform healthGroup;
    public RectTransform healthBar;

    void Update()
    {
        playTime -= Time.deltaTime;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        int minute = (int)(playTime / 60);
        int second = (int)(playTime % 60);
        timerTxt.text = string.Format("{0:00}", minute) + ":" + string.Format("{0:00}", second);

        playerHealthTxt.text = playerItem.health + " / " + playerItem.maxHealth;
        playerAmmoTxt.text = playerItem.ammo + " / " + playerItem.maxAmmo;
        killTxt.text = playerItem.kill.ToString();
        deathTxt.text = playerItem.death.ToString();
        grenadeCountTxt.text = playerItem.hasGrenades + " / " + playerItem.maxHasGrenades;

        if (playerItem.hasWeapon == -1)
        {
            weaponAmmoTxt.text = "- / -";
            hammerImg.color = new Color(1, 1, 1, 0);
            handGunImg.color = new Color(1, 1, 1, 0);
            subMachineGunImg.color = new Color(1, 1, 1, 0);
        }
        else if (playerItem.hasWeapon == 0)
        {
            weaponAmmoTxt.text = "- / -";
            hammerImg.color = new Color(1, 1, 1, 1);
            handGunImg.color = new Color(1, 1, 1, 0);
            subMachineGunImg.color = new Color(1, 1, 1, 0);
        }
        else if (playerItem.hasWeapon == 1)
        {
            weaponAmmoTxt.text = playerItem.equipWeapon.curAmmo + " / " + playerItem.equipWeapon.maxAmmo;
            hammerImg.color = new Color(1, 1, 1, 0);
            handGunImg.color = new Color(1, 1, 1, 1);
            subMachineGunImg.color = new Color(1, 1, 1, 0);
        }
        else if (playerItem.hasWeapon == 2)
        {
            weaponAmmoTxt.text = playerItem.equipWeapon.curAmmo + " / " + playerItem.equipWeapon.maxAmmo;
            hammerImg.color = new Color(1, 1, 1, 0);
            handGunImg.color = new Color(1, 1, 1, 0);
            subMachineGunImg.color = new Color(1, 1, 1, 1);
        }
        healthBar.localScale = new Vector3(enemy.currentHealth / enemy.maxHealth, 1, 1);


    }
}
