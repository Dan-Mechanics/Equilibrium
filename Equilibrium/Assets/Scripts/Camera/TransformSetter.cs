using UnityEngine;

namespace OuterWilds
{
    public class TransformSetter : MonoBehaviour, IPassable<Vector3>
    {
        public void Pass(ref Vector3 t) => SetPos(t);

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