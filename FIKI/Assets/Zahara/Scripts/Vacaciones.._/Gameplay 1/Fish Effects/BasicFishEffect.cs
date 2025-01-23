using UnityEngine;

[CreateAssetMenu(fileName = "BasicFishEffect", menuName = "Fish Effects/Basic Fish Effect")]
public class BasicFishEffect : FishEffectBase
{
    
    public override void ApplyEffect()
    {
        Debug.Log("Basic Fish Effect");
    }
}