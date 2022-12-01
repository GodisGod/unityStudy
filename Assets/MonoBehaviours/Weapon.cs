using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject ammoPrefab;
    static List<GameObject> ammoPool;
    public int poolSize;

    public float weaponVelocity;

    private void Awake()
    {
        if (ammoPool == null) {
            ammoPool = new List<GameObject>();
                }
        for (int i =0;i<poolSize;i++) {
            GameObject ammo = Instantiate(ammoPrefab);
            ammo.SetActive(false);
            ammoPool.Add(ammo);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
          
            FireAmmo();
        }
    }

    GameObject SpawnAmmo(Vector3 location) {
        foreach (GameObject ammo in ammoPool) {
            if (ammo.activeSelf == false) {
                ammo.SetActive(true);
                ammo.transform.position = location;
                return ammo;
            }
        }
        return null;
    }

    private void FireAmmo()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject ammo = SpawnAmmo(transform.position);
        if (ammo != null) {
            Arc arc = ammo.GetComponent<Arc>();
            float travelDuration = 1.0f / weaponVelocity;
            StartCoroutine(arc.TravelArc(mousePosition,travelDuration));
        }
    }

    private void OnDestroy()
    {
        ammoPool = null;
    }
}