using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{

    public override void Fire(Vector2 direction)
    {
        if (!reloading)
        {
            bullet = PistolPool.SharedInstance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.SetPositionAndRotation(
                    muzzleTransform.position, muzzleTransform.rotation);
                bullet.SetActive(true);
                bullet.tag = gameObject.tag;
            }
            var bulletScript = bullet.GetComponent<Projectile>();
            currentAmmo -= ammoPerShot;
            direction.y += Random.Range(-recoil, recoil);
            bulletScript.MoveToTarget(direction);
            AudioManager.Play(AudioClipName.PistolShot);
        }
    }

    protected override IEnumerator StartReload()
    {
        reloading = true;

        yield return reloadDelay;
        currentAmmo = magazineSize;

        AudioManager.Play(AudioClipName.ReloadSound);
        UpdateAmmoUI.Instance.UpdateWeaponAmmo(currentAmmo, maxAmmo);
        reloading = false;
    }

}
