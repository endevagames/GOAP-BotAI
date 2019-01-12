using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipSwordCheckComplete : GOAPActionCheckComplete 
{
    public override bool checkComplete(GOAPAgent agent)
    {
        var carryingList = agent.carryingList;
        for(int i = 0; i < carryingList.Count; i++)
        {
            var item = carryingList[i];
            if(item.name == "Sword")
            {
                if(item.count > 0) return true;
                    else return false;
            }
        }
        return false;
    }
}