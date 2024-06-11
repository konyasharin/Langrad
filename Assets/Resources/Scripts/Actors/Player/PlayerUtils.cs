using UnityEngine;

namespace Resources.Scripts.Actors.Player
{
    public static class PlayerUtils
    {
        public static Vector2[] GetCorners()
        {
            Vector2[] playerCorners = new Vector2[4];
            var bounds = PlayerCharacter.Instance.Collider.bounds;
            playerCorners[0] = new Vector2(bounds.min.x, bounds.min.y);
            playerCorners[1] = new Vector2(bounds.max.x, bounds.min.y);
            playerCorners[2] = new Vector2(bounds.min.x, bounds.max.y);
            playerCorners[3] = new Vector2(bounds.max.x, bounds.max.y);
            return playerCorners;
        }

        public static Vector2 GetNearestCorner(Vector2 position)
        {
            Vector2[] corners = GetCorners();
            Vector2 nearestCorner = corners[0];
            for (int i = 1; i < corners.Length; i++)
            {
                if (Vector2.Distance(corners[i], position) < Vector2.Distance(nearestCorner, position))
                {
                    nearestCorner = corners[i];
                }
            }

            return nearestCorner;
        }
    }
}
