using System.Collections;
using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Attachments
{
    public class CameraRotator : MonoBehaviour
    {
        [SerializeField] private Transform backupCamera;
        [SerializeField] private Transform realBackupCamera;
        public float rotateTimeScale;
        public int rotationClipCount;
        public float deltaAngle;
        public bool isTurning = false;

        private float targetAngle;
        private WaitForSeconds rotateTimeClip;
        private void Start()
        {
            targetAngle = transform.eulerAngles.y;
            rotateTimeClip = new WaitForSeconds(rotateTimeScale / rotationClipCount);
        }
        private void OnEnable()
        {
            realBackupCamera.tag = "Untagged";
            backupCamera.gameObject.SetActive(false);
        }
        private void OnDisable()
        {
            if (backupCamera != null)
            {
                backupCamera.position = transform.position;
                backupCamera.rotation = transform.rotation;
                realBackupCamera.tag = "MainCamera";
                backupCamera.gameObject.SetActive(true);
            }
        }
        public void TurnLeft()
        {
            StartCoroutine(CRTTurnLeft());
        }
        private IEnumerator CRTTurnLeft()
        {
            isTurning = true;

            targetAngle += deltaAngle;
            if (targetAngle >= 360)
                targetAngle -= 360;

            for (int i = 0; i < rotationClipCount; ++i)
            {
                transform.Rotate(new Vector3(0, deltaAngle / rotationClipCount, 0), Space.Self);
                yield return rotateTimeClip;
            }
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);

            isTurning = false;
        }
        public void TurnRight()
        {
            StartCoroutine(CRTTurnRight());
        }
        private IEnumerator CRTTurnRight()
        {
            isTurning = true;

            targetAngle -= deltaAngle;
            if (targetAngle < 0)
                targetAngle += 360;

            for (int i = 0; i < rotationClipCount; ++i)
            {
                transform.Rotate(new Vector3(0, -deltaAngle / rotationClipCount, 0), Space.Self);
                yield return rotateTimeClip;
            }
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);

            isTurning = false;
        }
    }
}
