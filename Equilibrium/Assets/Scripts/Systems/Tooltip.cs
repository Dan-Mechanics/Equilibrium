using System.Collections;
using UnityEngine;
using TMPro;

namespace Equilibrium
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] private RectTransform rect = default;
        [SerializeField] private Vector2 restPosition = default;
        [SerializeField] private Vector2 activePosition = default;
        [SerializeField] private float duration = default;
        [SerializeField] private EasyTextWriter easyTextWriter = default;

        private bool isMoving;
        private float timer;

        private void Start() 
        {
            rect.anchoredPosition = restPosition;
            easyTextWriter.Write(string.Empty);
        }

        public void Show(string text) 
        {
            if (isMoving)
                return;

            isMoving = true;

            print(text);
            easyTextWriter.Write(text);
            StartCoroutine(DoAnimation());
        }

        private IEnumerator DoAnimation()
        {
            rect.anchoredPosition = restPosition;
            timer = 0f;

            while (timer <= duration)
            {
                rect.anchoredPosition = Vector2.Lerp(restPosition, activePosition, timer / duration);

                timer += Time.deltaTime;

                yield return null;
            }

            rect.anchoredPosition = activePosition;
            timer = 0f;

            yield return new WaitForSeconds(3f);

            while (timer <= duration)
            {
                rect.anchoredPosition = Vector2.Lerp(activePosition, restPosition, timer / duration);

                timer += Time.deltaTime;

                yield return null;
            }

            rect.anchoredPosition = restPosition;
            isMoving = false;
        }
    }
}