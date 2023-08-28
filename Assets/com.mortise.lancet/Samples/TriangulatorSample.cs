using System.Collections.Generic;
using UnityEngine;


namespace MortiseFrame.Lancet.Sample {

    public class TriangulatorSample : MonoBehaviour {
        Vector3[] vertices; // 定义多边形的顶点
        public Vector3[] vertices_temp; // 临时顶点
        public List<int> indexList; // 定义多边形的顶点索引

        [ContextMenu("清空")]
        void Clear() {
            vertices = null;
            indexList = null;
        }

        [ContextMenu("切")]
        void Clip() {

            if (vertices_temp == null || vertices_temp.Length < 3) {
                return;
            }

            vertices = vertices_temp;

            // 使用Triangulator类进行三角剖分
            indexList = Triangulator.Triangulate(vertices);

        }

        void DrawPolygon_Temp() {
            if (vertices_temp == null || vertices_temp.Length < 3) {
                return;
            }

            // 绘制多边形边界
            Gizmos.color = Color.white;
            for (int i = 0; i < vertices_temp.Length; i++) {
                Gizmos.DrawLine(vertices_temp[i], vertices_temp[(i + 1) % vertices_temp.Length]);
            }
        }

        void DrawPolygon() {
            if (vertices == null || vertices.Length < 3) {
                return;
            }

            // 绘制多边形边界
            Gizmos.color = Color.blue;
            for (int i = 0; i < vertices.Length; i++) {
                Gizmos.DrawLine(vertices[i], vertices[(i + 1) % vertices.Length]);
            }
        }

        void DrawGizmozTriangulator() {
            if (vertices == null || vertices.Length < 3 || indexList == null || indexList.Count < 3) {
                return;
            }

            // 使用获得的索引绘制三角形
            Gizmos.color = Color.red;
            for (int i = 0; i < indexList.Count; i += 3) {
                Gizmos.DrawLine(vertices[indexList[i]], vertices[indexList[i + 1]]);
                Gizmos.DrawLine(vertices[indexList[i + 1]], vertices[indexList[i + 2]]);
                Gizmos.DrawLine(vertices[indexList[i + 2]], vertices[indexList[i]]);
            }
        }

        void OnDrawGizmos() {

            DrawPolygon_Temp();
            DrawPolygon();
            DrawGizmozTriangulator();

        }

    }

}