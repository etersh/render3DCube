# Rendering a 3D Cube

## Overview
This project shows a simple rotating 3D cube made with **OpenTK** in C#.  
It was made to practice basic 3D graphics ideas like vertices, shaders, projection/view matrices, and transformations.

<img width="1908" height="1564" alt="CleanShot 2025-10-19 at 23 28 02@2x" src="https://github.com/user-attachments/assets/78a6f365-e813-44f6-b015-03767e87acfe" />
<img width="1908" height="1564" alt="CleanShot 2025-10-19 at 23 28 05@2x" src="https://github.com/user-attachments/assets/9e98c1fa-ef21-493c-8e5a-17efbebca10a" />


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
