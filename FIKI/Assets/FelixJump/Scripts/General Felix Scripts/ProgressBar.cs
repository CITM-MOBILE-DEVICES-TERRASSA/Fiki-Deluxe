using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Image barImage;
    [SerializeField] Image backgroundImage;
    [SerializeField] Sprite[] barSprites; 
    [SerializeField] Sprite[] backgroundSprites;

    private int barSpriteIndex = 0;
    private int backgroundSpriteIndex = 0;

    private void Awake()
    {
        StartCoroutine(StartCountdown(2.0f));
    }

    private IEnumerator StartCountdown(float duration)
    {
        float timeRemaining = duration;

        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            float progress = timeRemaining / duration; 
            barImage.fillAmount = progress; 

            ChangeBarSprite(progress);
            ChangeBackgroundSprite(progress);

            yield return null;
        }

        ScenesManager.Instance.LoadScene("GameSelector");
    }

    void ChangeBarSprite(float progress)
    {
        if (barSprites.Length > 0) 
        {
            int newBarSpriteIndex = Mathf.FloorToInt(progress * barSprites.Length);
            newBarSpriteIndex = Mathf.Clamp(newBarSpriteIndex, 0, barSprites.Length - 1);

            if (newBarSpriteIndex != barSpriteIndex)
            {
                barSpriteIndex = newBarSpriteIndex;
                barImage.sprite = barSprites[barSpriteIndex]; 
            }
        }
    }

    void ChangeBackgroundSprite(float progress)
    {
        if (backgroundSprites.Length > 0) 
        {
            int newBackgroundSpriteIndex = Mathf.FloorToInt(progress * backgroundSprites.Length);
            newBackgroundSpriteIndex = Mathf.Clamp(newBackgroundSpriteIndex, 0, backgroundSprites.Length - 1);

            if (newBackgroundSpriteIndex != backgroundSpriteIndex)
            {
                backgroundSpriteIndex = newBackgroundSpriteIndex;
                backgroundImage.sprite = backgroundSprites[backgroundSpriteIndex]; 
            }
        }
    }
}
