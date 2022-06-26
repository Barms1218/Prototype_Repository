using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(InputController))]
public class PlayerWeaponHandler : WeaponHandler
{
    #region Fields

    private List<GameObject> weaponList = new List<GameObject>();

    #endregion

    #region Properties

    public GameObject Gun
    {
        set { gun = value; }
        get => gun;
    }
    public List<GameObject> WeaponList => weaponList;
    public Weapon CurrentWeapon
    {
        set { currentWeapon = value; }
        get => currentWeapon;
    }

    #endregion

    private void Awake()
    {
        weaponList.Add(gun);
        currentWeapon = gun.GetComponent<Weapon>();
        EntityInput.OnReload += Reload;
        EntityInput.OnSpecialAttack += SpecialAttack;
        EntityInput.OnFire += Fire;
    }

    private void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = mousePos - transform.position;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;        
        currentWeapon.Aim(angle, GameObject.FindGameObjectWithTag("Cursor").transform);
    }

    #region Event Methods

    protected override void Reload()
    {
        base.Reload();
    }

    protected override void Fire()
    {
        base.Fire();
    }

    protected override void SpecialAttack()
    {
        currentWeapon.SpecialAttack();
    }

    #endregion

}
