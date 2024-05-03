using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CanvasManager
{
    public static IEnumerator Move(CanvasMove move, RectTransform rectTransform)
    {
        float currentMovementTime = 0f;
        while (currentMovementTime < move.totalMovementTime)
        {
            currentMovementTime += Time.deltaTime;
            rectTransform.anchoredPosition =
                Vector2.Lerp(move.fromPosition, move.toPosition, currentMovementTime / move.totalMovementTime);
            yield return null;
        }
    }
}
