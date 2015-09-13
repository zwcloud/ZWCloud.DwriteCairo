using System;
using System.Runtime.InteropServices;

namespace DwriteCairo
{
    public sealed class TextFormat : IDisposable
    {
        private IDWriteTextFormat comObject;

        private const string IID_IDWriteTextFormat = "9c906818-31d7-4fd3-a151-7c5e225db55a";

        [ComImport]
        [Guid(IID_IDWriteTextFormat)]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IDWriteTextFormat { }

        TextFormat(IDWriteTextFormat obj)
        {
            this.comObject = obj;
        }

        ~TextFormat()
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

        #region Implementation of IDisposable

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}