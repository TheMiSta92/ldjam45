using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpawn : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        float oldSpawnX = GameObject.FindGameObjectWithTag("Player").transform.parent.position.x;
        float newSpawnX = this.gameObject.transform.position.x;
        GameObject.FindGameObjectWithTag("Player").transform.parent.Translate(newSpawnX - oldSpawnX, 0f, 0f);
        GameObject.FindGameObjectWithTag("Player").transform.Translate(oldSpawnX - newSpawnX, 0f, 0f);
        this.gameObject.GetComponent<Collider>().enabled = false;
    }

}
