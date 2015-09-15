using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ZWCloud.DWriteCairo.Internal;

namespace ZWCloud.DWriteCairo.DWrite
{
    public sealed class TextFormat : IDisposable
    {
        #region COM internals

        private IDWriteTextFormat comObject;
        private GetFontSizeSignature getFontSize;
        
        [ComImport]
        [Guid("9c906818-31d7-4fd3-a151-7c5e225db55a")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IDWriteTextFormat { }//contains 25 method(inherited method not included)

        [ComMethod(Index = 25)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
        private delegate float GetFontSizeSignature(IDWriteTextFormat textformat);

        internal IntPtr Handle
        {
            get { return Marshal.GetIUnknownForObject(this.comObject); }
        }

        internal TextFormat(IntPtr objPtr)
        {
            this.comObject =
                (IDWriteTextFormat)Marshal.GetObjectForIUnknown(objPtr);

            InitComMethods();
        }
        internal TextFormat(IDWriteTextFormat obj)
        {
            this.comObject = obj;

            InitComMethods();
        }

        internal void InitComMethods()
        {
            bool result;

            result = ComHelper.GetComMethod(this.comObject, 25, out this.getFontSize);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 25);
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

                this.getFontSize = null;
            }
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            this.Release();
            GC.SuppressFinalize(this);
        }

        #endregion

        #region COM Method wrappers

        private float GetFontSize()
        {
            return getFontSize(this.comObject);
        }

        #endregion

        #endregion

        #region API

        public float FontSize
        {
            get { return GetFontSize(); }
        }

        #endregion
    }
}