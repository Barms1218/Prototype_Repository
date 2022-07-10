﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Fields
    
    [SerializeField]
    int damage;
    [SerializeField]
    float timeToLive;
    [SerializeField]
    float forceMagnitude;

    #endregion

    void Update()
    {
        timeToLive -= Time.deltaTime;

        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHaveHealth damageable = collision.gameObject.GetComponent<IHaveHealth>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage, this.gameObject, 0);
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
            forceMagnitude, ForceMode2D.Impulse);
    }
}
