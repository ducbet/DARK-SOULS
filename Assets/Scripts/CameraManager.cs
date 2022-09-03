using UnityEngine;

namespace TMD
{
    public class CameraManager : MonoBehaviour
    {
        public Transform target;
        [Header("Translation attributes")]
        public float translationSmoothTime = 0.1f;

        [Header("Rotation attributes")]
        public float cameraRotationSpeed = 5f;
        public float minimumVerticalAngle = -5f;
        public float maximumVerticalAngle = 20f;

        private Transform translationPivotTransform;
        private Transform rotationPivotTransform;
        private Transform cameraTransform;
        private InputManager inputManager;

        private Vector3 currentTranslationVelocity = Vector3.zero;

        private Vector3 cameraRotatelAngle;
        private Vector3 pivotRotateAngle;


        private void Awake()
        {
            if (target == null)
            {
                target = FindObjectOfType<PlayerManager>().transform;
            }

            cameraTransform = Camera.main.transform;
            rotationPivotTransform = cameraTransform.parent;
            translationPivotTransform = rotationPivotTransform.parent;

            cameraRotatelAngle = cameraTransform.localRotation.eulerAngles;
            pivotRotateAngle = rotationPivotTransform.rotation.eulerAngles;

            inputManager = target.GetComponent<InputManager>();
        }

        public void HandleCameraMovement()
        {
            HandleTranslation();
            HandleRotation();
        }

        private void HandleTranslation()
        {
            // Why SmoothDamp make jittery?
            // Vector3 newPosition = Vector3.SmoothDamp(translationPivotTransform.position, target.position, ref currentTranslationVelocity, translationSmoothTime);
            translationPivotTransform.position = target.position;
        }

        private void HandleRotation()
        {
            // rotate camera on X axis: vertical rotation (rotate camera up and down)
            cameraRotatelAngle.x -= inputManager.cameraRotationX * cameraRotationSpeed * Time.deltaTime;
            cameraRotatelAngle.x = Mathf.Clamp(cameraRotatelAngle.x, minimumVerticalAngle, maximumVerticalAngle);
            cameraTransform.localRotation = Quaternion.Euler(cameraRotatelAngle);

            // rotate pivot on Y axis: horizontal rotation (rotate camera around pivot)
            pivotRotateAngle.y += inputManager.cameraRotationY * cameraRotationSpeed * Time.deltaTime;
            rotationPivotTransform.rotation = Quaternion.Euler(pivotRotateAngle);
        }
    }

}