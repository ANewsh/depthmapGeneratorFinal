using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Timers;
using Timer = System.Timers.Timer;
using Keys = OpenTK.Windowing.GraphicsLibraryFramework.Keys;
using System.ComponentModel;

namespace DepthmapMaker
{
    public class ModelViewer : GameWindow
    {
        private const float _mouseSens = 0.1f;

        private int _vertexBufferObject;

        private int _vertexArrayObject;

        private int _elementBufferObject;

        private Shader _lightingShader;

        private Texture _texMap;

        private Camera _camera;


        private double _time;

        private float[] _vertices;
        private uint[] _indices;

        //Menu child thread

        private Menu menu = new Menu();
        private Thread menuThread;

        /* allows me to update model rotation by updating these values during OnUpdateFrame()
         * and then using them during OnRenderFrame() to apply as values for the rotation matrix
         * probably a cleaner way to do this without setting them as class properties*/
        private double _rotateX = 0;
        private double _rotateY = 0;

        //Mouse threading
        private Timer _leftmousedownTimer;
        private Timer _rightmousedownTimer;
        private bool _mouseMoved = false;
        private Vector2 _mousePosition;

        //affects lighting attenuation
        //todo let this be changed by user through GUI
        public float lightConstant = 1.0f;
        public float lightLinear = 0.1f;
        public float lightQuadratic = 0.1f;
        public float lightDiffuse = 10f;

        public ModelViewer(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }
        protected override void OnLoad()
        {
            _leftmousedownTimer = new Timer();
            _rightmousedownTimer = new Timer();
            _leftmousedownTimer.Interval = _rightmousedownTimer.Interval = 1;
            _leftmousedownTimer.Enabled = _rightmousedownTimer.Enabled = false;
            _leftmousedownTimer.Elapsed += leftMouseTimerEvent;
            _rightmousedownTimer.Elapsed += rightMouseTimerEvent;



            ObjLoader loader = new ObjLoader();
            var model = loader.LoadFile("E:/objects/cat.obj");
            _vertices = model.Vertices.ToArray();
            _indices = model.VertexIndices.ToArray();

            base.OnLoad();

            GL.ClearColor(0f, 0f, 0f, 1.0f);

            GL.Enable(EnableCap.DepthTest);

            _vertexBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _lightingShader = new Shader("E:/major proj/DepthmapGenerator/DepthmapGeneratorPrototype/PrototypeViewer/Shaders/shader.vert",
                "E:/major proj/DepthmapGenerator/DepthmapGeneratorPrototype/PrototypeViewer/Shaders/lighting.frag");

            _vertexArrayObject = GL.GenVertexArray();

            GL.BindVertexArray(_vertexArrayObject);
            var posLocation = _lightingShader.GetAttribLocation("aPos");
            GL.VertexAttribPointer(posLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);


            GL.EnableVertexAttribArray(posLocation);

            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            _texMap = Texture.LoadFromFile("E:/major proj/DepthmapGenerator/DepthmapGeneratorPrototype/PrototypeViewer/Resources/white.png");

            _camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

            //absolute paths until I figure out how the relative pathing works for vs
            _lightingShader = new Shader("E:/major proj/DepthmapGenerator/DepthmapGeneratorPrototype/PrototypeViewer/Shaders/shader.vert",
                "E:/major proj/DepthmapGenerator/DepthmapGeneratorPrototype/PrototypeViewer/Shaders/lighting.frag");


            menuThread = new Thread(new ThreadStart(runMenu));
            menuThread.SetApartmentState(ApartmentState.STA);
            menuThread.Start();

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            _time += 4.0 * e.Time;
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.BindVertexArray(_vertexArrayObject);

            _texMap.Use(TextureUnit.Texture0);
            _lightingShader.Use();

            _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
            _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            _lightingShader.SetVector3("light.position", _camera.Position);
            _lightingShader.SetVector3("light.direction", _camera.Front);
            _lightingShader.SetFloat("light.cutOff", MathF.Cos(MathHelper.DegreesToRadians(180f)));
            _lightingShader.SetFloat("light.outerCutOff", MathF.Cos(MathHelper.DegreesToRadians(180f)));
            _lightingShader.SetFloat("light.constant", lightConstant);
            _lightingShader.SetFloat("light.linear", lightLinear);
            _lightingShader.SetFloat("light.quadratic", lightQuadratic);
            _lightingShader.SetVector3("light.diffuse", new Vector3(lightDiffuse));

            var model = Matrix4.Identity * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(_rotateY)) * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(_rotateX));
            _lightingShader.SetMatrix4("model", model);

            var view = _camera.GetViewMatrix();

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
            SwapBuffers();
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            if (KeyboardState.IsKeyDown(Keys.Down))
            {
                _camera.Fov = _camera.Fov + 0.2f;
            }
            else if (KeyboardState.IsKeyDown(Keys.Up))
            {
                _camera.Fov = _camera.Fov - 0.2f;
            }
            if (MouseState.IsButtonPressed(MouseButton.Left))
            {
                CursorState = CursorState.Grabbed;
                _leftmousedownTimer.Enabled = true;
            }
            else if (MouseState.IsButtonReleased(MouseButton.Left))
            {
                _leftmousedownTimer.Enabled = false;
                _mouseMoved = false;
                CursorState = CursorState.Normal;
            }
            else if (MouseState.IsButtonPressed(MouseButton.Right))
            {
                CursorState = CursorState.Grabbed;
                _rightmousedownTimer.Enabled = true;
            }
            else if (MouseState.IsButtonReleased(MouseButton.Right))
            {
                _rightmousedownTimer.Enabled = false;
                _mouseMoved = false;
                CursorState = CursorState.Normal;
            }
            base.OnUpdateFrame(e);
            UpdateFromMenu();

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                menuThread.Abort();
            }
            catch (System.PlatformNotSupportedException ex)
            {
                //
            }
            base.OnClosing(e);
        }
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            _camera.Position += _camera.Front * (float)e.OffsetY;
        }
        private void leftMouseTimerEvent(Object source, ElapsedEventArgs e)
        {
            var mouse = MouseState;
            if (!_mouseMoved)
            {
                _mousePosition = new Vector2(mouse.X, mouse.Y);
                _mouseMoved = true;
            }
            else
            {
                var deltaX = mouse.X - _mousePosition.X;
                var deltaY = mouse.Y - _mousePosition.Y;
                _rotateX += deltaX * _mouseSens;
                _rotateY += deltaY * _mouseSens;
                _mouseMoved = false;
            }
        }
        private void rightMouseTimerEvent(Object source, ElapsedEventArgs e)
        {
            var mouse = MouseState;
            if (!_mouseMoved)
            {
                _mousePosition = new Vector2(mouse.X, mouse.Y);
                _mouseMoved = true;
            }
            else
            {
                var deltaX = mouse.X - _mousePosition.X;
                var deltaY = mouse.Y - _mousePosition.Y;
                _camera.Position -= _camera.Right * deltaX * _mouseSens * 0.1f;
                _camera.Position += _camera.Up * deltaY * _mouseSens * 0.1f;
                _mouseMoved = false;
            }
        }

        private void runMenu()
        {
            ApplicationConfiguration.Initialize();
            menu = new Menu();
            Application.Run(menu);
        }

        private void UpdateFromMenu()
        {
            lightDiffuse = menu.getDiffuse();
            lightLinear = lightQuadratic = menu.getAttenuation();
            if (menu.isbuttonPressed())
            {
                takeScreenshot(menu.GetSelectedFilepath());
            }
        }

        private void takeScreenshot(string filepath)
        {
            int width = Size.X;
            int height = Size.Y;
            Bitmap result = new Bitmap(width, height);

            System.Drawing.Imaging.BitmapData data =
                result.LockBits(new Rectangle(0, 0, width, height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.ReadPixels(0, 0, width, height, PixelFormat.Rgba, PixelType.UnsignedByte, data.Scan0);
            result.UnlockBits(data);
            result.RotateFlip(RotateFlipType.RotateNoneFlipY);
            result.Save(filepath);
        }
    }
}
