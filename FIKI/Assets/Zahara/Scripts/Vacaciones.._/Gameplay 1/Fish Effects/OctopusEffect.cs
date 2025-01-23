using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "OctopusEffect", menuName = "Fish Effects/Octopus Effect")]
public class OctopusEffect : FishEffectBase
{
    public Sprite inkSprite;
    [Min(0.2f)]public float duration;
    
    public override void ApplyEffect()
    {
        Debug.Log("Octopus Effect");

        var g = GameObject.FindWithTag("Canvas");
        var instance = g.AddComponent<Image>();
        if (instance)
        {
            instance.sprite = inkSprite;

            Destroy(instance, duration);
        }
    }
}