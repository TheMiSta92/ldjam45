using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class ACollectable : MonoBehaviour {

    [SerializeField] private string title = "Untitled Collectable";
    [SerializeField] [TextArea] private string description = "This is a collectable without a description.";
    [SerializeField] private string code = "throw new System.NotImplementedException();";
    [SerializeField] private bool destroyAfterCollect = true;



    private void Awake() { 
        Collider2D collider = this.gameObject.GetComponent<Collider2D>();
        if (collider == null) throw new System.Exception("Collectable Component of GO " + this.gameObject.name + " is missing it's collider!");
        if (!collider.isTrigger) throw new System.Exception("Collectable Component of GO " + this.gameObject.name + " has a collider which is not a trigger!");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            this.applyEffect();
            sGameEventManager.Access().Trigger_Collected(this);
            if (this.destroyAfterCollect) Destroy(this.gameObject);
        }
    }



    public string GetTitle() { return this.title; }
    public string GetDescription() { return this.description; }



    protected abstract void applyEffect();
    protected abstract void undoEffect();

}