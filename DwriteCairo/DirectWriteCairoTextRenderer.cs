using System;
using System.Runtime.InteropServices;

namespace ZWCloud.DWriteCairo
{
    [ComImport]
    [Guid("ef8a8135-5cc6-45fe-8825-c5a0724eb819")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDWriteTextRenderer { }

    public class DirectWriteCairoTextRenderer :IDisposable
    {
        internal IDirectWriteCairoTextRenderer comObject;

        [ComImport]
        [Guid(DirectXUtil.DirectWrite.IID_IDirectWriteCairoTextRenderer)]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IDirectWriteCairoTextRenderer : IDWriteTextRenderer { }

        internal static DirectWriteCairoTextRenderer Create()
        {
            return new DirectWriteCairoTextRenderer(DirectXUtil.DirectWrite.CreateTextRender());
        }

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

        #region Implementation of IDisposable

        public void Dispose()
        {
            this.Release();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}