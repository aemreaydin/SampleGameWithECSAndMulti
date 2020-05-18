using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public const float Speed = 5.0f;

        private float _centerOffset;

        private void Start()
        {
            _centerOffset = transform.position.y;
        }

        private void Update()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var moveVector = new Vector3(horizontal * Speed * Time.deltaTime,
                                         0.0f,
                                         vertical * Speed * Time.deltaTime);
            transform.Translate(moveVector);
        }

        private void Rotate()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var rayHit)) return;

            var rotatePos = rayHit.point;
            rotatePos.y = _centerOffset;

            var lookRotation = 
                transform.rotation = Quaternion.LookRotation(rotatePos - transform.position);
        }
    }
}