using System.Collections;
using KomeijiRai.ContingencyProtocol.Attachments;
using KomeijiRai.ContingencyProtocol.Entities.Units.Robots;
using KomeijiRai.ContingencyProtocol.Utils;
using TMPro;
using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Controllers
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager Instance { get; private set; }
        private PlayerInputManager() { }
        private void Awake()
        {
            Instance = this;
        }
        [SerializeField] private TextMeshProUGUI tmpro;
        private void Start()
        {
            playerRobot.OnRobotDeath += DoOnPlayerDeath;
        }
        [SerializeField] private Robot playerRobot;
        public Robot PlayerRobot => playerRobot;
        [SerializeField] private CameraRotator cameraRotator;
        private bool isTurningTurret = false;
        public bool isPlaying = true;
        public void DoOnPlayerDeath()
        {
            isPlaying = false;
        }
        public void DoOnPlayerReborn()
        {
            isPlaying = true;
        }

        private void Update()
        {
            if (!isPlaying)
                return;
            TurnCameraInput();
            MoveInput();
            SwitchTurretInput();
            FireInput();
            tmpro.text = playerRobot.CurHP + " / " + playerRobot.GetData.hp;
        }
        private void FixedUpdate()
        {
            if (!isPlaying)
                return;
            if (!isTurningTurret)
                StartCoroutine(CRTTurnTurretInput());
        }

        private void MoveInput()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            if (Mathf.Approximately(x, 0f) &&
                Mathf.Approximately(y, 0f))
            {
                playerRobot.Hold();
            }
            else
            {
                if (!Mathf.Approximately(x, 0f) &&
                    !Mathf.Approximately(y, 0f))
                {
                    x /= ConstUtils.SQRT_2;
                    y /= ConstUtils.SQRT_2;
                }

                Vector3 dir =
                    Quaternion.AngleAxis(cameraRotator.transform.eulerAngles.y, Vector3.up) *
                    new Vector3(x, 0, y);
                playerRobot.Move(dir);
            }
        }

        private IEnumerator CRTTurnTurretInput()
        {
            isTurningTurret = true;
            Vector3 target = InputUtils.GetMouseWorldPosition();
            playerRobot.TurretLookAt(target);
            yield return ConstUtils.WAIT_FOR_10_MS;
            isTurningTurret = false;
        }
        private void FireInput()
        {
            if (Input.GetMouseButton(0))
                playerRobot.TurretFire();
            else if (Input.GetMouseButtonUp(0))
                playerRobot.TurretHoldFire();
        }

        private void SwitchTurretInput()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll > 0.01f)
                playerRobot.SwitchTurret(true);
            else if (scroll < -0.01f)
                playerRobot.SwitchTurret(false);
        }
        private void TurnCameraInput()
        {
            if (!cameraRotator.isTurning)
            {
                if (Input.GetKey(KeyCode.Q))
                    cameraRotator.TurnLeft();
                else if (Input.GetKey(KeyCode.E))
                    cameraRotator.TurnRight();
            }

        }
    }
}

