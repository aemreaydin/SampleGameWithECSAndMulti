using UnityEngine;

namespace Player
{
    public class PlayerCameraController : MonoBehaviour
    {
        public Transform player;
        [SerializeField] private Vector3 offset = Vector3.up * 10.0f;

        private void LateUpdate()
        {
            transform.position = player.position + offset;
            transform.LookAt(player);
        }
    }
}