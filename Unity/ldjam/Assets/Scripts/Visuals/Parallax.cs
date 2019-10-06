using UnityEngine;

[AddComponentMenu("LDJAM/Visuals/Parallax")]
public class Parallax : MonoBehaviour {

    protected float camStartX;
    protected float objStartX;
    [SerializeField] [Range(.01f, 3f)] protected float effectAmount = .2f;
    [SerializeField] protected bool invertDirection = false;
    [SerializeField] protected int onStage = 1;
    protected bool running = false;



    private void Start() {
        if (this.onStage == 1) {
            this.camStartX = Camera.main.gameObject.transform.position.x;
            this.running = true;
        }
        this.objStartX = this.gameObject.transform.position.x;
        sGameEventManager.Access().OnStageSwitch += stageSwitch;
    }

    private void Update() {
        if (this.running) {
            float camChangeX = Camera.main.gameObject.transform.position.x - this.camStartX;
            float newObjX = this.objStartX - camChangeX * this.effectAmount;
            if (this.invertDirection) {
                newObjX += camChangeX * this.effectAmount * 2f;
            }
            this.gameObject.transform.position = new Vector3(newObjX, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }
    }



    protected void stageSwitch(int stage) {
        if (stage == this.onStage) {
            this.running = true;
        } else {
            this.running = false;
        }
    }

}