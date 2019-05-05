using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic game entity to hold the debug code.
/// </summary>
public class Entity : MonoBehaviour
{
    /// <summary>
    /// The entity's health.
    /// </summary>
    public int health;

    /// <summary>
    /// If the entity is alive.
    /// </summary>
    public bool isAlive;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isAlive = health > 0 ? true : false;
    }
}
