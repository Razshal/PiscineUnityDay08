﻿using System.Collections;using System.Collections.Generic;using UnityEngine;public class SpawnerScript : MonoBehaviour{    public GameObject prefabToSpawn;    private GameObject actualMob;    private float timer;    public float respawnTime = 3f;    // Use this for initialization    void Start()    {        timer = respawnTime;    }    // Update is called once per frame    void Update()    {        if (!actualMob || actualMob.GetComponent<EnemyScript>().life <= 0)        {            timer -= Time.deltaTime;        }        if (timer <= 0 && prefabToSpawn)        {            actualMob = Instantiate(prefabToSpawn, transform.position, transform.rotation);            timer = respawnTime;        }    }}