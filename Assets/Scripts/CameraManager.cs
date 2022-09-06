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

        [Header("Handle collision attributes")]
        public float spherecastThickness = 0.2f;
        public float zoomSmoothTime = 0.1f;

        private Transform translationPivotTransform;
        private Transform rotationPivotTransform;
        private Transform cameraTransform;
        private InputManager inputManager;

        private Vector3 currentTranslationVelocity = Vector3.zero;
        private Vector3 currentZoomVelocity = Vector3.zero;

        private Vector3 cameraRotatelAngle;
        private Vector3 pivotRotateAngle;

        private float defaultCameraDistance;
        private bool canZoomOut = false;

        public LayerMask collisionLayers;
        public enum LayerBits
        {
            TransparentFX = 1,
            IgnoreRaycast = 2,
            Water = 4,
            UI = 5,
            Controller = 6,
            Environment = 7,
            Ground = 8,
            Player = 9,
        }

        public enum LayerMasks
        {
            TransparentFX = 1 << LayerBits.TransparentFX,
            IgnoreRaycast = 1 << LayerBits.IgnoreRaycast,
            Water = 1 << LayerBits.Water,
            UI = 1 << LayerBits.UI,
            Controller = 1 << LayerBits.Controller,
            Environment = 1 << LayerBits.Environment,
            Ground = 1 << LayerBits.Ground,
            Player = 1 << LayerBits.Player,
        }

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
            defaultCameraDistance = cameraTransform.localPosition.magnitude;

            inputManager = target.GetComponent<InputManager>();

            if (collisionLayers == 0)
            {
                collisionLayers = (int)~(LayerMasks.TransparentFX | LayerMasks.IgnoreRaycast | LayerMasks.UI | LayerMasks.Controller | LayerMasks.Environment | LayerMasks.Player);
            }

        }

        public void HandleCameraMovement()
        {
            HandleTranslation();
            HandleRotation();
            HandleCameraCollision();
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

        private void HandleCameraCollision()
        {
            Vector3 direction = cameraTransform.position - rotationPivotTransform.position;
            Vector3 offset = direction.normalized * 0.5f;  // offset is for the case player is to close to the wall -> can't detect
            float sphereCastLength = defaultCameraDistance < direction.magnitude ? defaultCameraDistance + 0.5f : (direction + offset).magnitude;
            RaycastHit hit;
            if (Physics.SphereCast(rotationPivotTransform.position - offset, spherecastThickness, direction, out hit, sphereCastLength, collisionLayers))
            {
                // hit.point is not on the cameraTransform-rotationPivotTransform line. It has small offset because the thickness of the SphereCast
                // If we zoom in to the hit.point, the forward of the camera is not pointing to the player anymore
                Vector3 preciseHitPointPrecise = rotationPivotTransform.position + Vector3.Project(hit.point - rotationPivotTransform.position, direction);
                ZoomIn(preciseHitPointPrecise + offset);  // offset is for the camera does not zoom too close to the player
            }
            else
            {
                ZoomOut(rotationPivotTransform.position + (direction.normalized * defaultCameraDistance));
            }
        }

        private void ZoomIn(Vector3 hitPoint)
        {
            Vector3 zoomedInCameraPosition = Vector3.SmoothDamp(cameraTransform.position, hitPoint, ref currentZoomVelocity, zoomSmoothTime);
            cameraTransform.position = zoomedInCameraPosition;
            canZoomOut = true;
        }

        private void ZoomOut(Vector3 defaultCameraPosition)
        {
            if (!canZoomOut)
            {
                return;
            }
            Vector3 zoomedOutCameraPosition = Vector3.SmoothDamp(cameraTransform.position, defaultCameraPosition, ref currentZoomVelocity, zoomSmoothTime);
            cameraTransform.position = zoomedOutCameraPosition;
            if ((defaultCameraPosition - zoomedOutCameraPosition).magnitude < 0.1)
            {
                canZoomOut = false;
            }
        }
    }
}