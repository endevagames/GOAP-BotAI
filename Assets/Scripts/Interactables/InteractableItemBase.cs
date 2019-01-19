using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    weapon
}
public class InteractableItemBase : MonoBehaviour
{
    public Vector3 pickupPosition;
    public Vector3 pickupRotation;
    public PickupType pickupType = PickupType.weapon;
}
