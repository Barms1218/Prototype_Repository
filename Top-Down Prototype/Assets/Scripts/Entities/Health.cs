using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IHaveHealth
{

    [SerializeField] private int maxHealth = 100;
    [SerializeField] UnityEvent onDeath;
    private float _health;

    public int MaxHealth => maxHealth;

    float IHaveHealth.Health
    {
       get => _health;
       set => _health = value; 
    }

    private void Awake()
    {
        _health = maxHealth;
    }
    public void ReduceHealth(float amount, GameObject damageSource)
    {
        if (_health > 0)
        {
            _health -= amount;
            if (gameObject.CompareTag("Player") || gameObject.CompareTag("Enemy"))
            {
                var bloodSplatter = BloodPool.SharedInstance.GetPooledObject();
                if (bloodSplatter != null)
                {
                    bloodSplatter.transform.SetPositionAndRotation(
                        damageSource.transform.position, damageSource.transform.rotation);
                    bloodSplatter.SetActive(true);
                }
                AudioManager.Play(AudioClipName.BulletHit);
            }
            if (TryGetComponent(out Animator _animator))
            {
                _animator.SetTrigger("Hurt");
            }
        }
        if (_health <= 0)
        {
            _health = maxHealth;
            onDeath.Invoke();
        }


    }

    public void RestoreHealth(float amount)
    {
        if (_health < maxHealth)
        {
            _health += amount;
            if (_health > maxHealth)
            {
                _health = maxHealth;
            }
        }
    }
}
