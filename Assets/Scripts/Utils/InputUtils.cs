using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Utils
{
    public class InputUtils
    {

        public static Vector3 GetMouseWorldPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 200, ConstUtils.GROUND_LAYER))
                return hitInfo.point;
            else
                return Vector3.zero;
        }
    }
}
