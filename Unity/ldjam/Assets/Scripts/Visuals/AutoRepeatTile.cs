using UnityEngine;

[AddComponentMenu("LDJAM/Visuals/Automatic Tile Repeater")]
public class AutoRepeatTile : MonoBehaviour {

    [SerializeField] [Range(2, 50)] protected int amount = 5;
    [SerializeField] protected float offset = 1f;



    private void Start() {
        GameObject toDuplicate = this.gameObject;
        Transform parent = this.gameObject.transform.parent;
        for (int i = 1; i < amount; i++) {
            GameObject newGo = Instantiate(toDuplicate, parent);
            Destroy(newGo.GetComponent<AutoRepeatTile>());
            newGo.name = this.gameObject.name + " - Auto Clone " + i;
            toDuplicate = newGo;
            newGo.transform.Translate(this.offset, 0f, 0f);
        }
    }

}
