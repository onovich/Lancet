using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangulator {

    public static List<int> Triangulate(Vector3[] vertices) {

        List<int> indices = new List<int>();

        // 1. 判断顺时针还是逆时针
        float signedArea = 0;
        for (int i = 0; i < vertices.Length; i++) {
            int nextIndex = (i + 1) % vertices.Length;
            signedArea += (vertices[i].x * vertices[nextIndex].y) - (vertices[nextIndex].x * vertices[i].y);
        }
        bool clockwise = signedArea < 0;

        // 2. 找到 "耳朵“（且相连后的三角形在轮廓内的，相邻的三个点）并剪掉
        List<int> activeVertices = new List<int>();
        for (int i = 0; i < vertices.Length; i++) {
            activeVertices.Add(i);
        }

        // 失败计数器，防止死循环
        int failCount = 0;

        int v = vertices.Length - 1;
        while (activeVertices.Count > 2) {
            int v0 = activeVertices[(v + 0) % activeVertices.Count];
            int v1 = activeVertices[(v + 1) % activeVertices.Count];
            int v2 = activeVertices[(v + 2) % activeVertices.Count];

            Vector2 a = vertices[v0];
            Vector2 b = vertices[v1];
            Vector2 c = vertices[v2];

            // 确定凸角或凹角
            float cross = (b.x - a.x) * (c.y - a.y) - (c.x - a.x) * (b.y - a.y);
            bool convex = clockwise ? cross < 0 : cross > 0;

            // 凸角
            if (convex) {

                // 检查是否有任何顶点在三角形内
                bool earFound = true;
                for (int i = 0; i < vertices.Length; i++) {
                    if (i == v0 || i == v1 || i == v2)
                        continue;

                    if (PointInTriangle(vertices[i], a, b, c)) {
                        earFound = false;
                        break;
                    }
                }

                // 剪耳朵
                if (earFound) {
                    indices.Add(v0);
                    indices.Add(v1);
                    indices.Add(v2);
                    activeVertices.RemoveAt((v + 1) % activeVertices.Count);
                    v = (v + activeVertices.Count - 1) % activeVertices.Count;
                    failCount = 0;
                    continue;
                }

            }
            failCount++;
            if (failCount > activeVertices.Count * 3) {
                break;
            }

            v = (v + 1) % activeVertices.Count;
        }

        return indices;
    }

    static bool PointInTriangle(Vector2 p, Vector2 a, Vector2 b, Vector2 c) {
        float d1 = Sign(p, a, b);
        float d2 = Sign(p, b, c);
        float d3 = Sign(p, c, a);
        bool hasNeg = (d1 < 0) || (d2 < 0) || (d3 < 0);
        bool hasPos = (d1 > 0) || (d2 > 0) || (d3 > 0);
        return !(hasNeg && hasPos);
    }

    // 三角形面积的两倍, 如果是顺时针则为负数, 逆时针则为正数, 为0则三点共线
    static float Sign(Vector2 p1, Vector2 p2, Vector2 p3) {
        return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
    }

}