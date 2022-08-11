using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Random = System.Random;

namespace NoarExtensions
{
    public static class NoarExtensions
    {
        public static void Filter<T>(this List<T> theList, List<T> elementsToRemove)
        {
            for (int i = theList.Count - 1; i >= 0; i--)
            {
                if(elementsToRemove.Contains(theList[i]))
                    theList.RemoveAt(i);
            }
        }
        
        public static void Merge<T>(this List<T> theList, List<T> otherList)
        {
            for (int i = 0; i < otherList.Count; i++)
            {
                if(!theList.Contains(otherList[i]))
                    theList.Add(otherList[i]);
            }
        }
        
        public static void MoveIndexUp<T>(this List<T> theList, int index)
        {
            int newIndex = Mathf.Clamp(index - 1, 0, theList.Count - 1);
            theList.MoveIndexTo(index, newIndex);
        }
        
        public static void MoveIndexDown<T>(this List<T> theList, int index)
        {
            int newIndex = Mathf.Clamp(index + 1, 0, theList.Count - 1);
            theList.MoveIndexTo(index, newIndex);
        }

        public static void MoveIndexTo<T>(this List<T> theList, int currentIndex, int newIndex)
        {
            T element = theList[currentIndex];
            theList.RemoveAt(currentIndex);
            theList.Insert(newIndex, element);
        }
        
        public static List<T> GetRandom<T>(this List<T> theList, int quantity)
        {
            if (quantity >= theList.Count)
            {
                return theList;
            }
            
            Random random = new Random();
            HashSet<T> hashset = new HashSet<T>();

            while (hashset.Count < quantity)
            {
                hashset.Add(theList[random.Next(0, theList.Count)]);
            }
            
            return new List<T>(hashset);
        }
        
        public static T GetRandom<T>(this List<T> theList)
        {
            return theList[UnityEngine.Random.Range(0, theList.Count)];
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void MoveElements<T>(this IList<T> list, int fromIndex, int toIndex)
        {
            if (toIndex < 0 || toIndex > list.Count - 1)
            {
                return;
            }

            T toMove = list[fromIndex];
            list.RemoveAt(fromIndex);
            list.Insert(toIndex, toMove);
        }

        public static bool TryGetInterface<T>(this GameObject obj, out T found)
            {
                MonoBehaviour[] monoBehaviors = obj.GetComponents<MonoBehaviour>();
                foreach (MonoBehaviour mono in monoBehaviors)
                {
                    if (mono is T)
                    {
                        found = (T)(object)mono;
                        return true;
                    }
                }
                found = default(T);
                return false;
            }

        public static Vector2 GetPointOnRadius(this Vector2 center, float radius)
        {
            Vector2 spawnPos = new Vector2();
            spawnPos.x = center.x + UnityEngine.Random.Range(-radius, radius);
            spawnPos.y = center.y + UnityEngine.Random.Range(-radius, radius);
            return spawnPos;
        }

        public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
        {
            var dir = (body.transform.position - explosionPosition);
            float wearoff = 1 - (dir.magnitude / explosionRadius);
            body.AddForce(dir.normalized * explosionForce * wearoff);
        }

        public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier)
        {
            var dir = (body.transform.position - explosionPosition);
            float wearoff = 1 - (dir.magnitude / explosionRadius);
            Vector3 baseForce = dir.normalized * explosionForce * wearoff;
            body.AddForce(baseForce);

            float upliftWearoff = 1 - upliftModifier / explosionRadius;
            Vector3 upliftForce = Vector2.up * explosionForce * upliftWearoff;
            body.AddForce(upliftForce);
        }

        public static Vector3 GetTotalSize(this Camera camera)
        {
            float height = 2f * camera.orthographicSize;
            float width = height * camera.aspect;

            return new Vector3(width, height);
        }

        public static Vector3 GetRandomPointOnScreen(this Camera camera, bool worldPoint = true)
        {
            Vector3 cameraSize = camera.GetTotalSize();
            float y = cameraSize.y / 2;
            float x = cameraSize.x / 2;

            float randomY = UnityEngine.Random.Range(-y, y);
            float randomX = UnityEngine.Random.Range(-x, x);

            Vector3 randomPosition = new Vector3(randomX, randomY);

            return worldPoint ? randomPosition : camera.WorldToScreenPoint(randomPosition);
        }
    }

    public static class NoarUtils
    {
        public static bool Approximately(float a, float b, float threshold = 0.005f)
        {
            return ((a - b) < 0 ? ((a - b) * -1) : (a - b)) <= threshold;
        }

        public static Color GetRandomColor(int minVal = 0, int maxVal = 255, float alpha = 1)
        {
            return new Color(UnityEngine.Random.Range(minVal, maxVal), UnityEngine.Random.Range(minVal, maxVal), UnityEngine.Random.Range(minVal, maxVal), 255 * alpha);
        }

        /// <summary>
        /// Serializes an object into Binary format, returning an array of bytes corresponding to the object.
        /// </summary>
        /// <typeparam name="T"> The type of the object to serialize.</typeparam>
        /// <param name="toSerialize"> The object to serialize.</param>
        /// <returns></returns>
        public static byte[] SerializeToBinary<T>(T toSerialize)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream mw = new MemoryStream();
            bf.Serialize(mw, toSerialize);
            byte[] toReturn = mw.ToArray();
            mw.Close();
            return toReturn;
        }

        /// <summary>
        ///  Deserialize a Binary formatted object (array of bytes) into an object of class T.
        /// </summary>
        /// <typeparam name="T">The type of object to be returned.</typeparam>
        /// <param name="bytes">The array of bytes of the serialized object.</param>
        /// <returns></returns>
        public static T DeserializeFromBinary<T>(byte[] bytes) where T : class
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream mw = new MemoryStream(bytes);
            T toReturn = bf.Deserialize(mw) as T;
            mw.Close();
            return toReturn;
        }

        public static string Capitalize(this string toCapitalize)
        {
            return char.ToUpper(toCapitalize[0]) + toCapitalize.Substring(1);
        }
        
        public static double ToEpoch(this DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            double toReturn = Math.Floor(diff.TotalSeconds);
            return toReturn <= 0 ? 0 : toReturn;
        }
        
        public static DateTime ToDateTime(this double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return timestamp < 0 ? origin : origin.AddSeconds(timestamp);
        }
    }
}

public static class EnumUtil
{
    public static IEnumerable<T> GetValues<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }
}
