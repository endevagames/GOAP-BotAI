using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public int health;

    private void Update() 
    {
        if(health <= 0) doDeath();    
    }
    public void doDamage(int damage)
    {
        health -= damage;
    }

    public virtual void doDeath() {}
}
