using UnityEngine;
using System.Collections;

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


        Quaternion q;
        public override void Activate()
        {

            StartCoroutine(Rotator());

            if (hasGoalRotation)
            {
                if (q.eulerAngles.Equals(goalRotation))
                    IsActive = true;
                else IsActive = false;
                
            }

        }

        IEnumerator Rotator()
        {
            //q = transform.rotation;
            Vector3 qEu = new Vector3();
            
            Vector3 endRot = q.eulerAngles - rotationAmount;
            //q = Quaternion.Euler(endRot);
            //transform.rotation = q;

            Vector3 endRount = new Vector3(Mathf.Round(endRot.x), 
                Mathf.Round(endRot.y),
                Mathf.Round(endRot.z));

            float ass = q.eulerAngles.x + rotationAmount.x;


            float a = Vector3.Angle(Vector3.right, endRount);

            /*while (qEu != endRount)
            {
                
                qEu.x = Mathf.Round(q.eulerAngles.x);
                qEu.y = Mathf.Round(q.eulerAngles.y);
                qEu.z = Mathf.Round(q.eulerAngles.z);


                if(a<=180)
                    q = Quaternion.Lerp(q, Quaternion.Euler(endRount),
                speed * Time.deltaTime);
                else if (a > 180)
                    q = Quaternion.Lerp(q, Quaternion.Euler(-endRount),
                speed * Time.deltaTime);


            q = Quaternion.Euler(endRount);

                print(ass);

                transform.rotation = q;

                
                
            }*/

            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x + rotationAmount.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
            //q = Quaternion.Euler(endRount);
            //transform.rotation = q;

            yield return null;
        }

    }
}
