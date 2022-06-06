using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NotificationType
{
    LogRegistry
}

public abstract class Observer : MonoBehaviour
{
    public abstract void OnNotify(object vlaue, NotificationType notificationType);
}
