using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ZWCloud.DWriteCairo
{
    [ComImport]
    [Guid("ef8a8135-5cc6-45fe-8825-c5a0724eb819")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDWriteTextRenderer { }

    internal class DirectWriteCairoTextRenderer :IDisposable
    {
        public static DirectWriteCairoTextRenderer Create()
        {
            return new DirectWriteCairoTextRenderer(CreateTextRender());
        }

    #region COM internals

    #region COM interface creation
        public static IDirectWriteCairoTextRenderer CreateTextRender()
        {
            object render = Internal.NativeMethods.DwriteCairoCreateTextRender(new Guid(IID_IDirectWriteCairoTextRenderer));
            Debug.Assert(render != null, "IDirectWriteCairoTextRenderer creating failed.");
            return (IDirectWriteCairoTextRenderer)render;
        }
    #endregion

    #region COM Method
        public IDirectWriteCairoTextRenderer comObject;

        public const string IID_IDirectWriteCairoTextRenderer = "f5b028d5-86fd-4332-ad5e-e86cf11cecd4";
        [ComImport]
        [Guid(IID_IDirectWriteCairoTextRenderer)]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IDirectWriteCairoTextRenderer : IDWriteTextRenderer { }


        private DirectWriteCairoTextRenderer(IDirectWriteCairoTextRenderer obj)
        {
            this.comObject = obj;
        }

        ~DirectWriteCairoTextRenderer()
        {
            this.Release();
        }

        private void Release()
        {
            if (this.comObject != null)
            {
                Marshal.ReleaseComObject(this.comObject);
                this.comObject = null;
            }
        }

    #region COM Method wrappers
        //None
    #endregion

    #endregion

    #endregion

    #region Implementation of IDisposable

        public void Dispose()
        {
            this.Release();
            GC.SuppressFinalize(this);
        }

    #endregion

    }
}