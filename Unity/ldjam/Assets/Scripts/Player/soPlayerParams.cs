using UnityEngine;

[CreateAssetMenu(fileName="New Player Parameters", menuName="LDJAM/Character/Parameters")]
public class soPlayerParams : ScriptableObject {

    public float accelerationGain = 20f;
    public float maxAcceleration = 10f;
    public float maxVelocity = 49.2f;
    public float jumpHeight = 500f;

}