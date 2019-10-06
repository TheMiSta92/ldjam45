using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class ACollectable : MonoBehaviour {

    [SerializeField] private string title = "Untitled Collectable";
    [SerializeField] private string filename = "Untitled.cs";
    [SerializeField] [TextArea] private string description = "This is a collectable without a description.";
    [SerializeField] private string code = "throw new System.NotImplementedException();";
    [SerializeField] private bool destroyAfterCollect = true;
    [SerializeField] private bool showFileName = true;



    private void Awake() { 
        Collider2D collider = this.gameObject.GetComponent<Collider2D>();
        if (collider == null) throw new System.Exception("Collectable Component of GO " + this.gameObject.name + " is missing it's collider!");
        if (!collider.isTrigger) throw new System.Exception("Collectable Component of GO " + this.gameObject.name + " has a collider which is not a trigger!");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            this.applyEffect();
            sGameEventManager.Access().Trigger_Collected(this);
            sConsoleTextWriter.Access().ResetSpeed();
            sConsoleTextWriter.Access().ShowText(this.GetCode());
            if (this.destroyAfterCollect) {
                Destroy(this.gameObject);
            } else {
                this.gameObject.GetComponent<Renderer>().enabled = false;
                this.gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }



    public string GetTitle() { return this.title; }
    public string GetFilename() { return this.filename; }
    public string GetDescription() { return this.description; }
    public string GetCode() { return this.code; }
    public bool ShouldShowFileName() { return this.showFileName; }



    protected abstract void applyEffect();
    protected abstract void undoEffect();

}