using UnityEngine;

namespace OuterWilds
{
    public class GridMaker : MonoBehaviour
    {
        [SerializeField] private GameObject[] prefabs = default;

        /*[SerializeField] private float height = default;
        [SerializeField] private float width = default;*/
        [SerializeField] private float spacing = default;
        [SerializeField] private int sideCount = default;

        [SerializeField] private Texture2D texture = default;

        private void Start()
        {
            float totalLength = spacing * (sideCount - 1);

            transform.position = new Vector3(-totalLength / 2f, transform.position.y, -totalLength / 2f);

            //GameObject cube;

            sideCount = texture.width;

            for (int x = 0; x < sideCount; x++)
            {
                for (int z = 0; z < sideCount; z++)
                {
                    //Instantiate(prefabs[Mathf.Clamp(Random.Range(0, prefabs.Length + 3), 0, prefabs.Length - 1)], new Vector3(x * spacing, 0f, z * spacing) + transform.position, Quaternion.identity);
                    Instantiate(prefabs[texture.GetPixel(x, z) == Color.white ? 1 : 0], new Vector3(x * spacing, 0f, z * spacing) + transform.position, Quaternion.identity);
                    //cube.transform.localScale = new Vector3(width, height, width);
                }
            }
        }
    }
}