// This script has the object always turn to face the camera. For example, an enemyHealthBar faces the player

using UnityEngine;

namespace SpectraStudios.CityChamp
{
    public class FaceCamera : MonoBehaviour
    {
        public Camera Camera;

        // Faces the main camera by default
        public void Awake()
        {
            Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        private void Update()
        {
            transform.LookAt(Camera.transform, Vector3.up);
        }

        // If it should face some other camera
        public void ChangeCamera(Camera newCamera)
        {
            Camera = newCamera;
        }
    }
}