using UnityEngine;
using UnityEditor;

namespace Assets.Scripts
{
    public class RotationInteractable : Interactable
    {
        [Header("Rotation Specifics")]
        [SerializeField] private Vector3 RotationAmount;
        [SerializeField] private float speed;


        public override void Activate()
        {
            Quaternion q = transform.rotation;
            Vector3 endRot = transform.rotation.eulerAngles + RotationAmount;
            q = Quaternion.Slerp(q, Quaternion.Euler(endRot),
                speed * Time.deltaTime);
        }


    }
}
