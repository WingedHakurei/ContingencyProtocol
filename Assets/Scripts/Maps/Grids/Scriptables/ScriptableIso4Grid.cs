using System.Collections.Generic;
using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Maps
{
    [CreateAssetMenu(fileName = "Scriptable Iso4 Grid")]
    public class ScriptableIso4Grid : ScriptableGrid
    {
        [SerializeField, Range(1, 50)] private int gridWidth = 16;
        [SerializeField, Range(1, 50)] private int gridHeight = 9;
        public override Dictionary<Vector3, NodeBase> GenerateGrid()
        {
            var ret = new Dictionary<Vector3, NodeBase>();
            var root = new GameObject("Grid").transform;

            int yOffset = gridHeight / 2;
            int xOffset = gridWidth / 2;
            float size = nodePrefab.Size;

            #region 空气墙
            float horizontalBorderOffset = (gridHeight % 2 == 0) ? -size / 2f : 0f;
            float verticalBorderOffset = (gridWidth % 2 == 0) ? -size / 2f : 0f;
            GameObject leftBorder = Instantiate(borderPrefab, root);
            leftBorder.name = "Iso4LeftBorder";
            leftBorder.transform.position = new Vector3(-(xOffset + 1) * size, 0, horizontalBorderOffset);
            leftBorder.transform.localScale = new Vector3(1, 1, gridHeight * size);

            GameObject rightBorder = Instantiate(borderPrefab, root);
            rightBorder.name = "Iso4RightBorder";
            rightBorder.transform.rotation = Quaternion.Euler(0, 180, 0);
            rightBorder.transform.position = new Vector3((gridWidth - xOffset) * size, 0, horizontalBorderOffset);
            rightBorder.transform.localScale = new Vector3(1, 1, gridHeight * size);

            GameObject backBorder = Instantiate(borderPrefab, root);
            backBorder.name = "Iso4BackBorder";
            backBorder.transform.rotation = Quaternion.Euler(0, -90, 0);
            backBorder.transform.position = new Vector3(verticalBorderOffset, 0, -(yOffset + 1) * size);
            backBorder.transform.localScale = new Vector3(1, 1, gridWidth * size);

            GameObject forwardBorder = Instantiate(borderPrefab, root);
            forwardBorder.name = "Iso4ForwardBorder";
            forwardBorder.transform.rotation = Quaternion.Euler(0, 90, 0);
            forwardBorder.transform.position = new Vector3(verticalBorderOffset, 0, (gridHeight - yOffset) * size);
            forwardBorder.transform.localScale = new Vector3(1, 1, gridWidth * size);
            #endregion

            for (int y = -yOffset; y < gridHeight - yOffset; ++y)
            {
                for (int x = -xOffset; x < gridWidth - xOffset; ++x)
                {
                    var node = Instantiate(nodePrefab, root.transform);
                    node.name = $"Iso4Node_({x},{y})";
                    bool isObstacle = false;
                    if (x != 0 || y != 0)
                        isObstacle = DecideIfObstacle();
                    GameObject model =
                        isObstacle ?
                        Instantiate(obstacleNodeModelPrefab, node.transform) :
                        Instantiate(groundNodeModelPrefab, node.transform);
                    node.Init(new Iso4Coord(x, y, size), isObstacle);
                    ret.Add(node.Coord.RealPosition, node);
                }
            }

            return ret;
        }
    }
}
