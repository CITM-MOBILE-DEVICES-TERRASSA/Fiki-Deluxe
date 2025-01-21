using UnityEngine;

public class IconAnimator : MonoBehaviour
{
    public enum IconType
    {
        Sword,
        Shield
    }

    public IconType iconType;
    public float swordSwingSpeed = 100f;
    public float shieldScaleSpeed = 2f;
    public float shieldScaleRange = 0.2f;

    private float initialScale;
    private bool isScalingUp = true;

    private ButtonManager buttonManager;

    void Start()
    {
        buttonManager = GameObject.FindObjectOfType<ButtonManager>();
        initialScale = transform.localScale.x;
    }

    void Update()
    {

        if (iconType == IconType.Sword && buttonManager.attack && !buttonManager.defense)
        {
            AnimateSword();
        }
        else if (iconType == IconType.Shield && buttonManager.defense && !buttonManager.attack)
        {
            AnimateShield();
        }
    }

    private void AnimateSword()
    {

        float rotationZ = Mathf.Sin(Time.time * swordSwingSpeed) * 15f;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }


    private void AnimateShield()
    {

        float scale = transform.localScale.x;

        if (isScalingUp)
        {

            scale += shieldScaleSpeed * Time.deltaTime;
            if (scale >= initialScale + shieldScaleRange)
            {
                scale = initialScale + shieldScaleRange;
                isScalingUp = false;
            }
        }
        else
        {
            scale -= shieldScaleSpeed * Time.deltaTime;
            if (scale <= initialScale - shieldScaleRange)
            {
                scale = initialScale - shieldScaleRange;
                isScalingUp = true;
            }
        }
        transform.localScale = new Vector3(scale, scale, transform.localScale.z);
    }
}