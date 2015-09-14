using System;
using System.Runtime.InteropServices;
using ZWCloud.DWriteCairo.DWrite;

namespace ZWCloud.DWriteCairo
{
    public class DirectWriteCairoTextRenderer :IDisposable
    {
        internal IDirectWriteCairoTextRenderer comObject;

        [ComImport]
        [Guid(DirectXUtil.DirectWrite.IID_IDirectWriteCairoTextRenderer)]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IDirectWriteCairoTextRenderer : TextLayout.IDWriteTextRenderer { }

        public static DirectWriteCairoTextRenderer Create()
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