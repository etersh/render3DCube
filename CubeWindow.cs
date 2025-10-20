using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

public class CubeWindow : GameWindow
{
    private int _vao, _vbo, _ebo, _shader, _uMvp;
    private float _time;

    public CubeWindow()
        : base(GameWindowSettings.Default, new NativeWindowSettings
        {
            Title = "3D Cube",
            Size = new Vector2i(800, 600)
        })
    { }

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(0.05f, 0.05f, 0.08f, 1.0f);
        GL.Enable(EnableCap.DepthTest);

        float[] verts =
        {
            -0.5f,-0.5f,-0.5f, 1,0,0,
             0.5f,-0.5f,-0.5f, 0,1,0,
             0.5f, 0.5f,-0.5f, 0,0,1,
            -0.5f, 0.5f,-0.5f, 1,1,0,
            -0.5f,-0.5f, 0.5f, 1,0,1,
             0.5f,-0.5f, 0.5f, 0,1,1,
             0.5f, 0.5f, 0.5f, 1,1,1,
            -0.5f, 0.5f, 0.5f, 0.2f,0.7f,0.9f
        };

        uint[] idx =
        {
            0,1,2, 2,3,0,
            4,5,6, 6,7,4,
            0,4,7, 7,3,0,
            1,5,6, 6,2,1,
            3,2,6, 6,7,3,
            0,1,5, 5,4,0
        };

        _shader = CreateShaderProgram("Shaders/vertex.glsl", "Shaders/fragment.glsl");
        _uMvp = GL.GetUniformLocation(_shader, "uMVP");

        _vao = GL.GenVertexArray();
        _vbo = GL.GenBuffer();
        _ebo = GL.GenBuffer();

        GL.BindVertexArray(_vao);

        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, verts.Length * sizeof(float), verts, BufferUsageHint.StaticDraw);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ebo);
        GL.BufferData(BufferTarget.ElementArrayBuffer, idx.Length * sizeof(uint), idx, BufferUsageHint.StaticDraw);

        int stride = 6 * sizeof(float);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, stride, 0);
        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, stride, 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);

        GL.BindVertexArray(0);
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
        _time += (float)args.Time;
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        Matrix4 proj = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60f), Size.X / (float)Size.Y, 0.1f, 100f);
        Matrix4 view = Matrix4.LookAt(new Vector3(1.8f, 1.8f, 1.8f), Vector3.Zero, Vector3.UnitY);
        Matrix4 model = Matrix4.CreateRotationY(_time) * Matrix4.CreateRotationX(_time * 0.6f);
        Matrix4 mvp = model * view * proj;

        GL.UseProgram(_shader);
        GL.UniformMatrix4(_uMvp, false, ref mvp);
        GL.BindVertexArray(_vao);
        GL.DrawElements(PrimitiveType.Triangles, 36, DrawElementsType.UnsignedInt, 0);
        GL.BindVertexArray(0);

        SwapBuffers();
    }

    private int CreateShaderProgram(string vertPath, string fragPath)
    {
        string vs = System.IO.File.ReadAllText(vertPath);
        string fs = System.IO.File.ReadAllText(fragPath);

        int v = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(v, vs);
        GL.CompileShader(v);
        GL.GetShader(v, ShaderParameter.CompileStatus, out int okV);
        if (okV == 0) throw new System.Exception(GL.GetShaderInfoLog(v));

        int f = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(f, fs);
        GL.CompileShader(f);
        GL.GetShader(f, ShaderParameter.CompileStatus, out int okF);
        if (okF == 0) throw new System.Exception(GL.GetShaderInfoLog(f));

        int p = GL.CreateProgram();
        GL.AttachShader(p, v);
        GL.AttachShader(p, f);
        GL.LinkProgram(p);
        GL.GetProgram(p, GetProgramParameterName.LinkStatus, out int okP);
        if (okP == 0) throw new System.Exception(GL.GetProgramInfoLog(p));

        GL.DeleteShader(v);
        GL.DeleteShader(f);
        return p;
    }
}
