using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon, IFlippable
{
    int numProjectiles = 5;
    [SerializeField]
    float pelletSpread = 0.5f;

    protected override void ShootWeapon()
    {
        for (int i = 0; i < numProjectiles; i++)
        {
            //Quaternion pelletRotation = Quaternion.Euler(0f, 0f, direction.z);
            var pellet = Instantiate(projectilePrefab, muzzleTransform.position, 
                Quaternion.identity);

            var pelletScript = pellet.GetComponent<PelletProjectile>();
            direction.y -= Random.Range(-pelletSpread, pelletSpread);
            pelletScript.MoveToTarget(direction);
        }
        AudioManager.Play(AudioClipName.ShotgunBlast);
    }

    protected override void Reload()
    {
        StartCoroutine(StartReload());
    }

    protected override IEnumerator StartReload()
    {
        reloading = true;

        while (currentAmmo < maxAmmo)
        {
            currentAmmo++;
            hud.AddToAmmoCount(1);
            AudioManager.Play(AudioClipName.ShotgunReload);
            yield return new WaitForSeconds(reloadSpeed);
            reloading = false;
        }
    }

    protected override void SpecialAttack()
    {

    }
}
