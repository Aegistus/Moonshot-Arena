using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHealth : Health
{
    public float colorResetTime = 1f;
    public Color damagedColor;
    public Color normalColor;

    private MeshRenderer meshRend;

    private void Start()
    {
        meshRend = GetComponent<MeshRenderer>();
        meshRend.material.color = normalColor;
    }

    public override void Damage(int damage)
    {
        meshRend.material.color = damagedColor;
        StartCoroutine(ResetColor());
        base.Damage(damage);
    }

    public override void Kill()
    {
        Destroy(gameObject);
    }

    private IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(colorResetTime);
        meshRend.material.color = normalColor;
    }
}
