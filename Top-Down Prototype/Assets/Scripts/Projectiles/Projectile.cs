﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Fields
    
    private float timeToLive;
    private Weapon weapon;
    [SerializeField] private ProjectileData projectileData;
    private GameObject intendedTarget;
    #endregion

    #region Properties

    public float WeaponDamage => projectileData.Damage;

    #endregion

    void Start()
    {
        timeToLive = projectileData.TimeToLive;
    }
    void Update()
    {
        timeToLive -= Time.deltaTime;

        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }
    }
    

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(projectileData.Damage, this.gameObject, 
            projectileData.ProjectileForce);
        }
        Destroy(gameObject);        
    }

    public void MoveToTarget(Vector2 force)
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        float angle = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
        Quaternion target = Quaternion.Euler(0, 0, angle);

        transform.rotation = target;
        body.AddRelativeForce(force.normalized * 
            projectileData.AmountOfForce, ForceMode2D.Impulse);
    }
}
