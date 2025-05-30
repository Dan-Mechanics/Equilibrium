using System.Collections.Generic;
using UnityEngine;

namespace Equilibrium
{
    public static class Utils
    {
        public static float Root(float value) 
        {
            return Mathf.Pow(Mathf.Pow(value, 2f) + Mathf.Pow(value, 2f), 0.5f);
        }
        
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

        public static bool RandomBool() 
        {
            return Random.value < 0.5f;
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

        public static int GetSmallestFloat(int[] array) 
        {
            int index = 0;
            int value = array[index];

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < value)
                {
                    index = i;
                    value = array[index];
                }
            }

            return index;
        }

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
        public static void GetClosest(Component[] components, int length, Vector3 toPoint, out ClosestPair closest)
        {
            float tempDist = Vector3.Distance(toPoint, components[0].transform.position);
            
            closest = default;
            closest.Set(components[0], tempDist);

            for (int i = 1; i < length; i++)
            {
                tempDist = Vector3.Distance(toPoint, components[i].transform.position);

                if (tempDist < closest.distance || i == 0)
                    closest.Set(components[i], tempDist);
            }
        }

        public static bool IsTime(float time) => Time.time >= time;

        public static Vector2Int GetPosFromIndex(int i, int dominantSideLength)
        {
            int x = i % (dominantSideLength + 1);
            int z = i / (dominantSideLength + 1);

            return new Vector2Int(x, z);
        }

        public static void PipeTo<T>(List<MonoBehaviour> source, List<T> dest) 
        {
            dest.Clear();
            source.ForEach(x => dest.Add(x.GetComponent<T>()));
            source.Clear();
        }

        public static Vector3 GetVertexWorldSpace(int index, Vector3[] verts, ITerrainable terrainable)
        {
            return verts[index] + new Vector3(-terrainable.GetSize() / 2f, 0f, -terrainable.GetSize() / 2f);
        }

        public static Vector3 GetRandomVertexWorldSpace(Vector3[] verts, ITerrainable terrainable)
        {
            return GetVertexWorldSpace(Random.Range(0, verts.Length), verts, terrainable);
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