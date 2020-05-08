using UnityEngine;

namespace ScoreSpace.Core
{
    public class PoolDisabler : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            other.gameObject.SetActive(false);
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            other.gameObject.SetActive(false);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            other.gameObject.SetActive(false);
        }
    }
}
