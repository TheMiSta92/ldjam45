using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanAppearOnTrigger : MonoBehaviour {

    protected bool done = false;



    private void OnTriggerEnter2D(Collider2D collision) {
        if (!this.done) {
            if (collision.gameObject.CompareTag("Player")) {
                this.gameObject.transform.parent.gameObject.GetComponent<Animator>().enabled = true;
                Invoke("startIdleAnimation", 1.5f);
                this.done = true;
            }
        }
    }

    protected void startIdleAnimation() {
        if (this.gameObject.activeSelf) {
            if (this.gameObject.GetComponent<Animator>() != null) {
                if (this.gameObject.GetComponent<Animator>().enabled) {
                    this.gameObject.transform.parent.gameObject.GetComponent<Animator>().Play("Idle");
                }
            }
        }
    }

}