using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simply defines what to do when a grid object collides with others
public interface GridCollider {

    // Returns true if this object can spawn, false otherwise
    bool CanSpawnWith(GameObject other);

    // Handles a spawn of this object in another object
    void HandleSpawn(GameObject other);

    // Handles a collision with the given GameObject; can be ignored if
    // this object never moves
    // Returns true if this object should continue moving, false otherwise
    bool HandleCollision(GameObject other);

}
