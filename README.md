# Rendering a 3D Cube

## Overview
This project shows a simple rotating 3D cube made with **OpenTK** in C#.  
It was made to practice basic 3D graphics ideas like vertices, shaders, projection/view matrices, and transformations.

---

## Library Used
- **OpenTK 4.8.1** (for OpenGL and math)
- C# (.NET 8)

---

## How It Works
Used OpenTK’s `GameWindow` to open an OpenGL window.  
The cube has 8 vertices and is drawn with 36 indices (6 faces × 2 triangles × 3 vertices).  
Each vertex has a color, and the fragment shader mixes them across the faces.

For movement, I applied rotation using a model matrix that updates every frame.  
The view matrix sets the camera, and the projection matrix adds perspective so the cube looks 3D instead of flat.

---

## How to Run
```bash
dotnet restore
dotnet run
