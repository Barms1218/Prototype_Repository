using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : Weapon
{
    float specialAttackDelay = 2f;
    

    /// <summary>
    /// 
    /// </summary>
    protected override void ShootWeapon()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        var projectileScript = projectile.GetComponent<Projectile>();

        projectileScript?.MoveToTarget(Vector2.right);

    }
    /// <summary>
    /// 
    /// </summary>
    protected override void SpecialAttack()
    {
        if (currentAmmo == data.MaxAmmo
            && !reloading)
        {
            StartCoroutine(SpecialAttackDelay());
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator SpecialAttackDelay()
    {
        yield return new WaitForSeconds(specialAttackDelay);
        hud.ReduceAmmoCount(2);
        ShootWeapon();
    }
}
