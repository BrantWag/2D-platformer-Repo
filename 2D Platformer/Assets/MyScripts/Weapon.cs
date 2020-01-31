﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float fireRate = 0;
    public float Damage = 10;
    public LayerMask whatNotToHit;

    float timeToFire = 0;
    public Transform firePoint;

    // Start is called before the first frame update
    void Awake()
    {
        transform.Find("FirePoint");
        if (firePoint == null) 
        {
            Debug.LogError("NO firepoint? WHAT?!?");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else {
            if (Input.GetButton("Fire1") && Time.time > timeToFire) {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }
    void Shoot() {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition-firePointPosition,100,whatNotToHit);
        Debug.DrawLine(firePointPosition, mousePosition);
        if (hit.collider != null) {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log(" We Hit " + hit.collider.name + " and did " + Damage + " damage.");
        }
    }
}
