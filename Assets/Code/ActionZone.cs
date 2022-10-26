using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionZone : MonoBehaviour
{
    public ActionZoneProps props;
}

[System.Serializable]
public class ActionZoneProps
{
    public Action action;
    public Sprite sprite;
    public GameObject nextCamera;
    public Transform nextSpawnPoint;
    public int index;
}
