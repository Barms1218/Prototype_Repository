using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityTakeDamage))]
public class InvincibilityFrames : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] int knockbackStrength = 10;
    [SerializeField] float invincibleTime = 1f;
    private IDamageable takeDamage;
    private WaitForSeconds invincibility;

    private void Start()
    {
        invincibility = new WaitForSeconds(invincibleTime);
        takeDamage = gameObject.GetComponent<IDamageable>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessHit(collision.gameObject);
    }

    private void ProcessHit(GameObject gameObject)
    {
        var hitObject = gameObject;

        if (hitObject.CompareTag("Enemy"))
        {
            controller.IsAlive = false;
            takeDamage.CanTakeDamage = false;
            var pushDirection = transform.position - hitObject.transform.position;
            controller.Rigidbody2D.AddForce(pushDirection * knockbackStrength,
                ForceMode2D.Impulse);
            StartCoroutine(Recover());
        }
    }

    private IEnumerator Recover()
    {
        yield return invincibility;
        controller.IsAlive = true;
        takeDamage.CanTakeDamage = true;
    }
}
