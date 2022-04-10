using KomeijiRai.ContingencyProtocol.Controllers;
using KomeijiRai.ContingencyProtocol.Maps;
using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Attachments
{
    public class PlayerSenserForNode : MonoBehaviour
    {
        private NodeBase node;
        private void Awake()
        {
            node = GetComponentInParent<NodeBase>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                PlayerInputManager.Instance.PlayerRobot.CurNode = node;
        }
    }
}
