﻿using UnityEngine;
using System.Collections;

public class LaserGun : AWeapon {
	
    private float curAmmunitionSeconds = 0;

    private LineRenderer lineRenderer;

    private float disableLineRenderer = -1;
    private bool alreadyPlaying = false;

    void Awake(){

        base.Awake();

        m_CurrentAmmunition = 15;
        m_Damage = 10;
        m_MagazineCap = 15;
        m_MaxAmmo = 90;

        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update(){

        if (disableLineRenderer <= 0) {

            lineRenderer.enabled = false;
            AudioManager.StopLaserShot();
            alreadyPlaying = false;
        }
        else {

            disableLineRenderer -= Time.deltaTime;
        }

        Shoot(transform.position, Vector3.forward);
    }

    public override void Shoot(Vector3 _start, Vector3 _dir){

        Ray ray = new Ray(_start, _dir);

        RaycastHit hit;

        if (!alreadyPlaying) {

            AudioManager.PlayLaserShot();
        }

        lineRenderer.enabled = true;

        if (m_CurrentAmmunition >= 0 && Physics.Raycast(ray, out hit)) {

            disableLineRenderer = 0.1F;
            AEntity entity = transform.gameObject.GetComponent<AEntity>();
            lineRenderer.SetPositions(new Vector3[]{ transform.position, hit.point });

            if (entity != null) {
                
                entity.TakeDamage(m_Damage * Time.deltaTime);
            }
        }
        else {

            lineRenderer.SetPositions(new Vector3[]{ transform.position, _start + _dir.normalized * 25 });
        }

        curAmmunitionSeconds -= Time.deltaTime;

        if (curAmmunitionSeconds <= 0) {

            m_CurrentAmmunition--;
            curAmmunitionSeconds += 1;
        }
    }
}