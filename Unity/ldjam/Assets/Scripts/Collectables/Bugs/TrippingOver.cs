using UnityEngine;

[AddComponentMenu("LDJAM/Collectables/Bugs/Tripping Over")]
public class TrippingOver : ACollectable {

    protected Rigidbody2D rb;



    private void Start() {
        this.rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }



    protected override void applyEffect() {
        this.rb.constraints = RigidbodyConstraints2D.None;
    }

    protected override void undoEffect() {
        this.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

}