using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public IWeapon currentWeapon;

    private void Start()
    {
        currentWeapon = GetComponentInChildren<IWeapon>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentWeapon.StartAttack();
        }
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(currentWeapon.EndAttack());
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(currentWeapon.Reload());
        }
    }

    public void SwapWeapon(IWeapon weapon)
    {
        currentWeapon = weapon;
    }
}
