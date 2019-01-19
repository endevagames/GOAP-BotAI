using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TreeDamageBahaviour : DamageController
{
    public override void doDeath()
    {
        GameObject Stack = Instantiate(Resources.Load("Environment/WoodStack"), gameObject.transform.position, new Quaternion(0, 90, 90, 90)) as GameObject; 
        for(int i = 0; i < Stack.transform.childCount; i++)
        {
            var child = Stack.transform.GetChild(i);
            child.tag = "Firewood";
            child.transform.parent = null;
        }
        Destroy(Stack.gameObject);
        Destroy(gameObject);
    }
}
