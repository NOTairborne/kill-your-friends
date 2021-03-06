﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float firerate = 0;
    public int Damage = 10;
    public LayerMask whatNotToHit;

    public Transform BulletTrailPrefab;
    public Transform HitPrefab;
    public Transform MuzzleFlashPrefab;
    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;

    float timeToFire = 0;
    Transform Firepoint;

    // Use this for initialization
    void Awake() {
        Firepoint = transform.Find("Firepoint");
    }

    // Update is called once per frame
    void Update() {
        if (firerate == 0)
        {
            if (Input.GetButtonDown ("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton ("Fire1") & Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / firerate;
                Shoot();
            }
        }
    }

    void Shoot ()
    {
        Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(Firepoint.position.x, Firepoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatNotToHit);

        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.cyan);
        if (hit.collider != null) 
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.DamageEnemy (Damage);
                Debug.Log("We hit" + hit.collider.name + "and did" + Damage + "damage");
            }
        }

        if (Time.time >= timeToSpawnEffect)
        {
            Vector3 hitPos;
            Vector3 hitNormal;

            if (hit.collider == null)
            {
                hitPos = (mousePosition - firePointPosition) * 30;
                hitNormal = new Vector3(9999, 9999, 9999);
            }
                
            else
            {
                hitPos = hit.point;
                hitNormal = hit.normal;
            }

            Effect(hitPos, hitNormal);
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }
    }

    void Effect (Vector3 hitPos, Vector3 hitNormal)
    {
        Transform trail = Instantiate (BulletTrailPrefab, Firepoint.position, Firepoint.rotation)as Transform;
        LineRenderer lr = trail.GetComponent<LineRenderer>();

        if (lr != null)
        {
            lr.SetPosition(0, Firepoint.position);
            lr.SetPosition(1, hitPos);
        }

        Destroy(trail.gameObject, 0.04f);

        if (hitNormal != new Vector3(9999, 9999, 9999))
        {
            Transform hitPartical = Instantiate(HitPrefab, hitPos, Quaternion.FromToRotation (Vector3.up, hitNormal)) as Transform;
            Destroy(hitPartical.gameObject, 1f);
        }

        Transform clone = Instantiate(MuzzleFlashPrefab, Firepoint.position, Firepoint.rotation) as Transform;
        clone.parent = Firepoint;
        float size = UnityEngine.Random.Range(0.6f, 0.9f);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, 0.02f);
    }
}        
         

      
