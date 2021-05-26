using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public int HP;
    FMODUnity.StudioEventEmitter barrierEmitter;

    void Start()
    {
        HP = 100;
        barrierEmitter = this.GetComponent<FMODUnity.StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        checkHP();
    }

    private void checkHP()
    {
        barrierEmitter.SetParameter("WoodHP", HP);
        if (HP <= 0)
        {
            barrierEmitter.SetParameter("WoodHP", HP);
            barrierEmitter.Play();
            Destroy(this.gameObject);
        }
    }

    public void decreaseDuration(int dmg)
    {
        HP -= dmg;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("PlayerWeapon"))
        {
            if(HP > 0)
            {
                decreaseDuration(20);
                collision.gameObject.GetComponent<Collider>().isTrigger = true;
                barrierEmitter.Play();
            }
        }
    }
}
