using OpenTK.Graphics.ES30;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Particulas.Core.shaders
{
    public class Shader:IDisposable
    {
        public int Handle { get; set; }
        private int _VertexShader;
        private int _FragmentShader;
        private string _sourceVertex;
        private string _sourceFragment;
        private bool disposableValue = false;

        public Shader(string vertexPath, string fragmentPath)
        {
            this._sourceVertex = File.ReadAllText(vertexPath);
            this._sourceFragment = File.ReadAllText(fragmentPath);
            this._VertexShader=GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(this._VertexShader, this._sourceVertex);
            this._FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(this._FragmentShader, this._sourceFragment);
            /////////////////////////////////////////////////////////////////////
            ///Compilamos el Shade de vertices
            /////////////////////////////////////////////////////////////////////
            GL.CompileShader(this._VertexShader);
            GL.GetShader(this._VertexShader, ShaderParameter.CompileStatus, out int success);
            if(success==0)
            {
                string infoLog = GL.GetShaderInfoLog(this._VertexShader);
                Console.WriteLine(infoLog);
            }
            /////////////////////////////////////////////////////////////////////
            ///Compilamos el Shade de fragmentos
            /////////////////////////////////////////////////////////////////////
            GL.CompileShader(this._FragmentShader);
            GL.GetShader(this._FragmentShader, ShaderParameter.CompileStatus, out int success2);
            if (success2 == 0)
            {
                string infoLog = GL.GetShaderInfoLog(this._FragmentShader);
                Console.WriteLine(infoLog);
            }
            /////////////////////////////////////////////////////////////////////
            ///Creamos el programa para la GPU
            /////////////////////////////////////////////////////////////////////
            this.Handle = GL.CreateProgram();
            GL.AttachShader(this.Handle, this._VertexShader);
            GL.AttachShader(this.Handle, this._FragmentShader);
            GL.LinkProgram(this.Handle);
            GL.GetProgram(this.Handle, GetProgramParameterName.LinkStatus, out int successLink);
            if(successLink==0)
            {
                string infoLog = GL.GetProgramInfoLog(this.Handle);
                Console.WriteLine(infoLog);
            } 
        }

        public void Use()
        {
            GL.UseProgram(this.Handle);
        }

        public virtual void Dispose(bool disposing)
        {
            if(!disposableValue)
            {
                GL.DeleteProgram(this.Handle);
                disposableValue = true;
            }
        }

        ~Shader()
        {
            GL.DeleteProgram(this.Handle);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
