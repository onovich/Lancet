using System.Collections.Generic;
using UnityEngine;


namespace MortiseFrame.Lancet.Sample {

    public class TriangulatorSample : MonoBehaviour {
        public Vector3[] vertices; // 定义多边形的顶点
        public List<int> indexList; // 定义多边形的顶点索引

        [ContextMenu("切")]
        void Clip() {

            if (vertices == null || vertices.Length < 3) {
                return;
            }

            // 使用Triangulator类进行三角剖分
            indexList = Triangulator.Triangulate(vertices);

        }

        void OnDrawGizmos() {

            if (vertices == null || vertices.Length < 3 || indexList == null || indexList.Count < 3) {
                return;
            }

            // 绘制多边形边界
            Gizmos.color = Color.blue;
            for (int i = 0; i < vertices.Length; i++) {
                Gizmos.DrawLine(vertices[i], vertices[(i + 1) % vertices.Length]);
            }

            // 使用获得的索引绘制三角形
            Gizmos.color = Color.red;
            for (int i = 0; i < indexList.Count; i += 3) {
                Gizmos.DrawLine(vertices[indexList[i]], vertices[indexList[i + 1]]);
                Gizmos.DrawLine(vertices[indexList[i + 1]], vertices[indexList[i + 2]]);
                Gizmos.DrawLine(vertices[indexList[i + 2]], vertices[indexList[i]]);
            }

        }

    }

}