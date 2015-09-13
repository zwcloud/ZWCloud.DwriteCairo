using System;
using System.Runtime.InteropServices;

namespace DwriteCairo
{
    public sealed class D2D1Factory : IDisposable
    {
        [ComImport]
        [Guid(DirectXUtil.D2D.IID_ID2D1Factory)]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface ID2D1Factory { }

        private ID2D1Factory comObject;

        private D2D1Factory(ID2D1Factory obj)
        {
            this.comObject = obj;
        }

        public static D2D1Factory Create()
        {
            return new D2D1Factory(DirectXUtil.D2D.CreateFactory());
        }

        private void Release()
        {
            if (this.comObject != null)
            {
                Marshal.ReleaseComObject(this.comObject);
                this.comObject = null;
            }
        }

        ~D2D1Factory()
        {
            this.Release();
        }


        #region Implementation of IDisposable

        public void Dispose()
        {
            this.Release();
            GC.SuppressFinalize(this);
        }

        #endregion
    }

}