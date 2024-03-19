# Lancet
Lancet is used for slicing Mesh, named after "lancet," which is the direct translation of "柳叶刀." It is based on the Earclipping algorithm and is suitable for use during editing rather than runtime.<br/>
**Lancet 用于切割 Mesh，名称取自于「柳叶刀」。它基于 Earclipping 算法实现，适合在编辑时而非运行时使用。**

# Examples
```
Vector3[] vertices;
List<int> indices;

void Refresh(){
    vertices = vertices_temp;
    indices = Triangulator.Triangulate(vertices);
}

void OnDrawGizmos() {
    if (vertices == null || vertices.Length < 3) {
        return;
    }

    // 绘制多边形边界
    Gizmos.color = Color.blue;
    for (int i = 0; i < vertices.Length; i++) {
        Gizmos.DrawLine(vertices[i], vertices[(i + 1) % vertices.Length]);
    }
}
```

# UPM URL
ssh://git@github.com/onovich/Lancet.git?path=/Assets/com.mortise.lancet#main
