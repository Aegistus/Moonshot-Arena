using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float bulletForce = 20f;
    public float kickBack = 5f;
    public float cooldownTime = 2f;
    [HideInInspector]
    public bool onCooldown = false;
    public LayerMask physicsObjectLayer;

    private Camera cam;
    private Rigidbody playerRB;
    private float timer;

    private void Start()
    {
        cam = Camera.main;
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !onCooldown)
        {
            Fire();
        }
        if (onCooldown)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                onCooldown = false;
            }
        }
    }

    private RaycastHit rayHit;
    public void Fire()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, physicsObjectLayer))
        {
            Rigidbody rb = rayHit.collider.GetComponent<Rigidbody>();
            if (rb)
            {
                if (rb.isKinematic)
                {
                    rb.isKinematic = false;
                }
                rb.velocity += cam.transform.forward * bulletForce;
            }
        }
        playerRB.velocity += -cam.transform.forward * kickBack;
        onCooldown = true;
        timer = cooldownTime;
    }


}
