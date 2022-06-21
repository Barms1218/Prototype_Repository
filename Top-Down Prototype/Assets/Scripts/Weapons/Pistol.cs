using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{

    public override void Fire(Vector2 direction)
    {
        if (!reloading)
        {
            base.Fire(direction);
            var projectile = Instantiate(projectilePrefab, muzzleTransform.position,
                Quaternion.identity);

            var bulletScript = projectile.GetComponent<Projectile>();

            bulletScript.MoveToTarget(direction);
            AudioManager.Play(AudioClipName.PistolShot);
        }
    }

    public override void SpecialAttack()
    {
        while(true)
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.enabled = true;
        }
    }

    public override void Reload()
    {
        if (!reloading)
        {
            StartCoroutine(StartReload());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator StartReload()
    {
        reloading = true;
        AudioManager.Play(AudioClipName.PistolStartReload);
        yield return new WaitForSeconds(data.ReloadSpeed);

        currentAmmo = data.MaxAmmo;
        hud.CurrentAmmo = currentAmmo;
        reloading = false;
        AudioManager.Play(AudioClipName.PistolStopReload);
    }
}
