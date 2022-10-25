using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Particulas.Core.shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Particulas.Core.Window
{
    public class Ventana : GameWindow
    {
        float[] vertices =
        {
            -0.2f, -0.2f, 0.0f,
            0.5f, -0.5f, 0.0f,
            0.0f, 0.5f, 0.0f
        };
        int VertexBufferObject;
        int VertexArrayObject;
        Shader shader;

        public Ventana(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings):base(gameWindowSettings, nativeWindowSettings)
        {
            
        }
        
        protected override void OnLoad()
        {
            
            GL.ClearColor(0.1f, 0.2f, 0.3f, 1f);
            //SwapBuffers();
            
            VertexBufferObject = GL.GenBuffer();

            //GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw);
            this.VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(this.VertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            shader = new Shader("shaders/shader.vert", "shaders/shader.frag"); //Compilo los programas OpenGL y los cargo al GPU
            shader.Use();

            //GL.BindVertexArray(VertexArrayObject);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            
            Console.WriteLine(GL.GetString(StringName.Version));
           
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            this.shader.Use();
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            var input = KeyboardState;
            if(input.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, e.Width,e.Height);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
            GL.DeleteBuffer(VertexBufferObject);
            GL.DeleteVertexArray(VertexArrayObject);
            shader.Dispose();
        }

        protected override void OnClosed()
        {
            base.OnClosed();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
        }
    }
}
