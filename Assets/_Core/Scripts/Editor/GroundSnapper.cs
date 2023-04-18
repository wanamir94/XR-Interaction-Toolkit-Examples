// COPYRIGHT BY WAN MUHAMAD AMIR BIN WAN ISA - wanamirisa@gmail.com

using UnityEngine;
using UnityEditor;

namespace Wan
{
    public static class GroundSnapper
    {
        private const string ActionName = "Snap to Ground";

        [MenuItem("Tools/ETAS/Snap To Ground %g")]
        public static void Perform()
        {
            RegisterUndo();

            foreach (Transform transform in Selection.transforms)
                MoveToGround(transform);
        }

        private static void RegisterUndo()
        {
            Undo.RegisterCompleteObjectUndo(Selection.transforms, ActionName);
        }

        private static void MoveToGround(Transform transform)
        {
            Collider ownCollider = transform.gameObject.GetComponentInChildren<Collider>();
            Collider tempCollider = null;
            if (ownCollider == null || !ownCollider.enabled)
            {
                tempCollider = transform.gameObject.AddComponent<BoxCollider>();
            }

            bool hitDown = Physics.Raycast(transform.position, Vector3.down, out RaycastHit downHit);
            bool hitUp = Physics.Raycast(downHit.point, Vector3.up, out RaycastHit upHit);

            if (hitDown && hitUp)
            {
                Vector3 translation = new Vector3(0, downHit.point.y - upHit.point.y, 0);
                transform.Translate(translation, Space.World);
            }

            if (tempCollider != null)
            {
                GameObject.DestroyImmediate(tempCollider);
            }
        }
    }
}