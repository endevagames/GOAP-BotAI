using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The GOAPActionCheckComplete class is a class that is used by GOAPActions to determine whether or not it is complete.
//When creating a GOAPAction, you have to include a checkComplete
public class GOAPActionCheckComplete : MonoBehaviour
{
    public virtual bool checkComplete(GOAPAgent agent) { return true;}
}
