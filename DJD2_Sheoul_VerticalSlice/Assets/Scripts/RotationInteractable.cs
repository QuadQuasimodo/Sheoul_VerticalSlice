using UnityEngine;
using UnityEditor;

namespace Assets.Scripts
{
    public class RotationInteractable : Interactable
    {
        [Header("Rotation Specifics")]

        [Tooltip("How much will be added to the rotation of the object")]
        [SerializeField] private Vector3 rotationAmount;

        [Tooltip("Speed of the rotation")]
        [SerializeField] private float speed;

        [Tooltip("Will only set itself activated when it rotates to this" +
            "specific vector.")]
        [SerializeField] private bool hasGoalRotation = true;
        [SerializeField] private Vector3 goalRotation;
        


        public override void Activate()
        {
            
            Quaternion q = transform.rotation;
            Vector3 endRot = transform.rotation.eulerAngles + rotationAmount;
            q = Quaternion.Slerp(q, Quaternion.Euler(endRot),
                speed * Time.deltaTime);

            if (hasGoalRotation)
            {
                if (q.eulerAngles.Equals(goalRotation))
                    IsActive = true;
                else IsActive = false;

            }

            if (IsActive)
            {
                if (MyInterGroup != null)
                    MyInterGroup.ActiveCount++;
            }


        }


    }
}
