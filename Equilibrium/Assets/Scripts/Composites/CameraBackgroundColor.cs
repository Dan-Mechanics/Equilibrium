using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Equilibrium
{
    public class CameraBackgroundColor : MonoBehaviour, IWritable<BaseTerrain>
    {
        [SerializeField] private Camera cam = default;

        public void Write(BaseTerrain terrain) 
        {
            if (terrain.backgroundColor.a < 1f)
                Debug.LogWarning("if (terrain.backgroundColor.a < 1f)");
            
            cam.backgroundColor = terrain.backgroundColor;
            RenderSettings.fogColor = terrain.backgroundColor;
        }
    }
}