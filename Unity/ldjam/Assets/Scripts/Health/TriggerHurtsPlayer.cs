using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("LDJAM/Fights/Trigger hurts Player")]
public class TriggerHurtsPlayer : MonoBehaviour {

    [SerializeField] protected float damage = 1f;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player") && this.gameObject.GetComponent<Renderer>().enabled) {
            sGameEventManager.Access().Trigger_CharacterHurt(this.damage);
        }
    }

}
