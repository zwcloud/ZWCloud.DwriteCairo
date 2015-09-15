using System;
using System.Runtime.InteropServices;

namespace ZWCloud.DWriteCairo.DWrite
{
    public sealed class TextFormat : IDisposable
    {
        #region COM internals

        private IDWriteTextFormat comObject;

        private const string IID_IDWriteTextFormat = "9c906818-31d7-4fd3-a151-7c5e225db55a";

        [ComImport]
        [Guid(IID_IDWriteTextFormat)]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IDWriteTextFormat { }//contains 25 method(inherited method not included)

        internal IntPtr Handle
        {
            get { return Marshal.GetIUnknownForObject(this.comObject); }
        }
        internal TextFormat(IntPtr objPtr)
        {
            this.comObject =
                (IDWriteTextFormat)Marshal.GetObjectForIUnknown(objPtr);
        }
        internal TextFormat(IDWriteTextFormat obj)
        {
            this.comObject = obj;
        }
        internal void InitComMethods()
        {
            bool result;

            //result = ComHelper.GetComMethod(this.comObject, 58/*index of the draw signature in the interface*/,
            //                           out this.draw);
            //if (!result)
            //{
            //    Debug.WriteLine("Fail to get COM method at index {0}", 58);
            //}
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
            this.Release();
            GC.SuppressFinalize(this);
        }

        #endregion

        #endregion

        #region API



        #endregion
    }
}