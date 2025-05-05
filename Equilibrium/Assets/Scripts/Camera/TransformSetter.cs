using UnityEngine;

namespace Equilibrium
{
    public class TransformSetter : MonoBehaviour, IWritable<Vector3>
    {
        public void Write(Vector3 pos) => SetPos(pos);

        public void SetPos(Vector3 pos)
        {
            transform.localPosition = pos;
        }

        public void SetRot(Vector3 rot) 
        {
            transform.localEulerAngles = rot;
        }

        public void SetScale(Vector3 scale) 
        { 
            transform.localScale = scale;
        }
    }
}