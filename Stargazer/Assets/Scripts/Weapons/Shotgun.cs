﻿using UnityEngine;
using UnityEngine.UI;

public class Shotgun : Equipment {

    public GameObject bulletPrefab;
    private float currentReloadTime = 0;
    private float timeBetweenTwoShots = .2f;
    private float shotsFired = 0;
    private const float MAX_RELOAD_TIME = 2.5f;
    private const float BULLET_SPEED = 12.0f;
    private const float BULLET_RANGE = 100.0f;
    private const float MAX_AMMO = 10;
    private float currentAmmo = 9;
    Vector3[] position = new Vector3[10];
    GameObject[] bulletClone = new GameObject[10];
    
    public override void OnActivate() {
            if (currentAmmo >= 0) {
                if (this.currentReloadTime <= 0) {
                Text txtAmmo = GameObject.Find("UI/AmmoCounter").GetComponent<Text>();
                txtAmmo.text = "Ammo: " + currentAmmo + "/" + MAX_AMMO;

                for (int i = 0; i < bulletClone.Length; i++) {
                    int rnd1 = Random.Range(-5, 5);
                    int rnd2 = Random.Range(-5, 5);
                    Vector3 bulletDirection = Calculate.HeadingBasedDirection(this.transform.position, this.transform.eulerAngles + new Vector3(rnd1, rnd2, 0));
                    position[i] = this.transform.GetChild(0).position + bulletDirection * (.1f * i);
                    bulletClone[i] = Instantiate(bulletPrefab, position[i], this.transform.rotation) as GameObject;
                    bulletClone[i].transform.localScale = new Vector3(.05f, .05f, .05f);
                    bulletClone[i].GetComponent<Bullet>().Init(bulletDirection, BULLET_SPEED, BULLET_RANGE);
                }

                if (shotsFired == 0) {
                    this.currentReloadTime = timeBetweenTwoShots;
                    shotsFired = 1;
                }
                else if (shotsFired == 1) {
                    this.currentReloadTime = MAX_RELOAD_TIME;
                    shotsFired = 0;
                }

                currentAmmo--;
            }
        }
    }

    void Update() {
        if (!(this.currentReloadTime <= 0)) {
            this.currentReloadTime -= Time.deltaTime;
        }
    }
}