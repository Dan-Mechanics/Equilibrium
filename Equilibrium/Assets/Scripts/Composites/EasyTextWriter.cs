using TMPro;
using UnityEngine;

namespace Equilibrium
{
    [RequireComponent(typeof(TMP_Text))]
    public class EasyTextWriter : MonoBehaviour
    {
        private TMP_Text text;

        private void Awake() 
        {
            text = GetComponent<TMP_Text>();
        }

        public void Write(float value)
        {
            if (text == null)
                return;
            
            Write(value.ToString());
        }

        public void Write(object obj) 
        {
            if (text == null)
                return;

            Write(obj.ToString());
        }

        public void Write(string str) 
        {
            if (text == null)
                return;

            text.text = str;
        }
    }
}