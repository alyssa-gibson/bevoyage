using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthStatusData", menuName = "StatusObjects/Health", order = 1)]
public class CharStatus : ScriptableObject
{
    public string charName = "name";
    public float[] position = new float[2];
    public GameObject characterGameObject;
    public int level = 1;
    public float maxHealth = 100;
    public float health = 100;
    public float power = 20;
    public float critRate = 30;
    public float defense = 20;
}
