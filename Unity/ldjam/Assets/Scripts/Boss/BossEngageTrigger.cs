using UnityEngine;

public class BossEngageTrigger : MonoBehaviour {

    [SerializeField] protected sBoss1 boss;

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            this.gameObject.GetComponent<Collider2D>().isTrigger = false;
            this.boss.DoSequence();
        }
    }

}