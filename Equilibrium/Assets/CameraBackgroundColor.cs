using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Equilibrium
{
    public class CameraBackgroundColor : MonoBehaviour, IWritable<BaseTerrain>
    {
        [SerializeField] private Camera cam = default;

        public void Write(BaseTerrain terrain) => cam.backgroundColor = terrain.backgroundColor;
    }
}