using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Lancelot
{
    public class EventOnWrap : MonoBehaviour
    {
        public UnityEvent wrapEvent;

        private bool isWrappingX = false;
        private bool isWrappingY = false;

        private void Update()
        {
            if (transform.position.x > Screen.width / 2f)
            {
                if (!isWrappingX)
                {
                    isWrappingX = true;
                    wrapEvent.Invoke();
                }
            }
            else if (transform.position.x < -Screen.width / 2f)
            {
                if (!isWrappingX)
                {
                    isWrappingX = true;
                    wrapEvent.Invoke();
                }
            }
            else
            {
                isWrappingX = false;
            }

            if (transform.position.y > Screen.height / 2f)
            {
                if (!isWrappingY)
                {
                    isWrappingY = true;
                    wrapEvent.Invoke();
                }
            }
            else if (transform.position.y < -Screen.height / 2f)
            {
                if (!isWrappingY)
                {
                    isWrappingY = true;
                    wrapEvent.Invoke();
                }
            }
            else
            {
                isWrappingY = false;
            }
        }
    }
}
