using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    public int deathC;
    public float[] position;

    public PlayerData(Player player)
    {
        /*deathC=GameManager.death_count;
        position=new float[3];
        position[0]=GameManager.respawnPoint.transform.position.x;
        position[1]=GameManager.respawnPoint.position.y;
        position[2]=GameManager.respawnPoint.position.z;
        */
    }

}
