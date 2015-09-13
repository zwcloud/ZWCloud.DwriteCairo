using System;
using System.Runtime.InteropServices;

namespace DwriteCairo
{
    public class DirectWriteCairoTextRenderer :IDisposable
    {
        private IDirectWriteCairoTextRenderer comObject;

        [ComImport]
        [Guid(DirectXUtil.DirectWrite.IID_IDirectWriteCairoTextRenderer)]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IDirectWriteCairoTextRenderer { }

        public static DirectWriteCairoTextRenderer Create()
        {
            return new DirectWriteCairoTextRenderer(DirectXUtil.DirectWrite.CreateTextRender());
        }

        private DirectWriteCairoTextRenderer(IDirectWriteCairoTextRenderer obj)
        {
            this.comObject = obj;
        }


        #region COM Method wrappers
        //None
        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}