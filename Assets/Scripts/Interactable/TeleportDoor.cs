using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDoor : MonoBehaviour,IInteractable
{
    public Vector3 teleportPoint;
    public PolygonCollider2D bounds;
    public void TriggerAction()
    {
        PlayerController.instance.transform.position = teleportPoint;
        CinemachineManager.instance.ChangeBounds(bounds);
    }
}
