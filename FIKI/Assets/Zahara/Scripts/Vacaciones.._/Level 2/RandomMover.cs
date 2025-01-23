using UnityEngine;

public class RandomMover : MonoBehaviour
{
    public float moveSpeed = 300f;
    public RectTransform canvasRect;
    private Vector2 targetPosition;
    
    void Start()
    {
        SetNewTargetPosition();
    }

    void Update()
    {
        if (canvasRect == null)
        {
            Debug.LogError("No se asignó el Canvas al script RandomMover.");
            return;
        }

        //mover hacia la pos objetivo
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.MoveTowards(
            rectTransform.anchoredPosition,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        //si llega a pos objetivo, definimos una nueva
        if (Vector2.Distance(rectTransform.anchoredPosition, targetPosition) < 1f)
        {
            SetNewTargetPosition();
        }
    }

    void SetNewTargetPosition()
    {        
        float x = Random.Range(-canvasRect.rect.width / 2, canvasRect.rect.width / 2);
        float y = Random.Range(-canvasRect.rect.height / 2, canvasRect.rect.height / 2 - 375);
        targetPosition = new Vector2(x, y);
    }
}
