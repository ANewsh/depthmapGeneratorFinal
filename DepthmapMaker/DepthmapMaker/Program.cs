using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System.Runtime.InteropServices;

namespace DepthmapMaker
{
    public static class Program
    {

        [STAThread] 
        private static void Main()
        {
            #if DEBUG
            AllocConsole();
            #endif
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(800, 600),
                Title = "Model Viewer",
                Flags = ContextFlags.ForwardCompatible,
            };

            using (var window = new ModelViewer(GameWindowSettings.Default, nativeWindowSettings))
            {
                window.Run();
            }
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}