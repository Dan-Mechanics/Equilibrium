using System.Collections.Generic;
using UnityEngine;

namespace OuterWilds
{
    public static class Utils
    {
        public static Vector3 Flatten(Vector3 vec, float y = 0f)
        {
            vec.y = y;
            return vec;
        }

        public static void SetVector3(ref Vector3 vec, float x, float y, float z) 
        {
            vec.x = x;
            vec.y = y;
            vec.z = z;
        }

        public static void SetVector2(ref Vector2 vec, float x, float y)
        {
            vec.x = x;
            vec.y = y;
        }

        public static void GiveRandomUpwardsRotation(Transform transform)
        {
            transform.Rotate(Vector3.up * Random.Range(0f, 360f), Space.World);
        }

        /// <summary>
        /// There's prolly a better way to do this using like % or something.
        /// </summary>
        public static int WrapIndex(int index, int movement, int length)
        {
            index += movement;

            if (length <= 0)
                return index;

            while (index >= length) 
            {
                index -= length;
            }

            while (index < 0)
            {
                index += length;
            }

            return index;
        }

        /*public static void StopRigidbody(Rigidbody rb, Vector3 pass) 
        {
            Vector3 vel = rb.velocity;
            vel.x *= pass.x;
            vel.y *= pass.y;
            vel.z *= pass.z;

            rb.AddForce(-vel, ForceMode.VelocityChange);
        }*/

        public static bool TryGetIndexFromPos(int x, int z, int maxX, int maxZ, out int index)
        {
            index = 0;

            if (x < 0 || x > maxX)
                return false;

            if (z < 0 || z > maxZ)
                return false;

            index = x + z * (maxX + 1);
            return true;
        }

        /// <summary>
        /// One could make this non-alloc.
        /// One could remvoe the guard clauses and it would might be better.
        /// </summary>
        public static ClosestPair GetClosest(Component[] components, Vector3 toPoint)
        {
            if (components.Length <= 0)
                return null;

            ClosestPair closest = new ClosestPair(components[0], Vector3.Distance(toPoint, components[0].transform.position));

            if (components.Length == 1)
                return closest;

            float tempDist;

            for (int i = 1; i < components.Length; i++)
            {
                tempDist = Vector3.Distance(toPoint, components[i].transform.position);

                if (tempDist < closest.distance)
                    closest.Set(components[i], tempDist);
            }

            return closest;
        }

        public static bool IsTime(float time) 
        {
            return Time.time >= time;
        }

        public class ClosestPair 
        {
            public Component component;
            public Transform transform => component.transform;
            public float distance;

            public ClosestPair(Component comp, float dist)
            {
                Set(comp, dist);
            }

            public void Set(Component component, float distance)
            {
                this.component = component;
                this.distance = distance;
            }
        }

        public static Vector2Int GetPosFromIndex(int i, int dominantSideLength)
        {
            int x = i % (dominantSideLength + 1);
            int z = i / (dominantSideLength + 1);

            return new Vector2Int(x, z);
        }

        public static Vector3 GetVertexWorldSpace(int index, ref Vector3[] verts, ITerrainable terrainable)
        {
            return verts[index] + new Vector3(-terrainable.GetSize() / 2f, 0f, -terrainable.GetSize() / 2f);
        }

        public static Vector3 GetRandomVertexWorldSpace(ref Vector3[] verts, ITerrainable terrainable)
        {
            return GetVertexWorldSpace(Random.Range(0, verts.Length), ref verts, terrainable);
        }

        public static List<T> GetAll<T>()
        {
            MonoBehaviour[] monoBehaviours = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

            List<T> list = new List<T>();

            for (int i = 0; i < monoBehaviours.Length; i++)
            {
                T t = monoBehaviours[i].GetComponent<T>();
                if (t != null)
                    list.Add(t);
            }

            //return list.ToArray();
            return list;
        }
    }
}