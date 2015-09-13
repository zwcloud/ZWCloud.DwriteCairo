using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DwriteCairo
{
    public sealed class DWriteTextLayout : IDisposable
    {
        [ComImport]
        [Guid(DirectXUtil.DirectWrite.IID_IDwriteFactory)]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IDWriteTextLayout { }
        
        #region signature delegates of COM method

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
        private delegate int DrawSignature(
            IDWriteTextLayout layout,
            IntPtr clientDrawingContext, DirectWriteCairoTextRenderer.IDirectWriteCairoTextRenderer renderer,
            float originX, float originY);

        #endregion

        private IDWriteTextLayout comObject;
        private DrawSignature draw;

        internal DWriteTextLayout(IDWriteTextLayout obj)
        {
            this.comObject = obj;

            bool result;

            result = ComHelper.GetComMethod(this.comObject, 33/*index of the draw signature in interface*/,
                                       out this.draw);
            if (!result)
            {
                Debug.WriteLine("Fail to get COM method at index {0}", 33);
            }
        }

        ~DWriteTextLayout()
        {
            this.Release();
        }

        private void Release()
        {
            if (this.comObject != null)
            {
                Marshal.ReleaseComObject(this.comObject);
                this.comObject = null;
                this.draw = null;
            }
        }

        #region COM Method wrappers

        /// <summary>
        /// Initiate drawing of the text.
        /// </summary>
        /// <param name="clientDrawingContext">An application defined value
        /// included in rendering callbacks.</param>
        /// <param name="renderer">The set of application-defined callbacks that do
        /// the actual rendering.</param>
        /// <param name="originX">X-coordinate of the layout's left side.</param>
        /// <param name="originY">Y-coordinate of the layout's top side.</param>
        void Draw(
            IntPtr clientDrawingContext, DirectWriteCairoTextRenderer.IDirectWriteCairoTextRenderer renderer,
            float originX, float originY)
        {
            Marshal.ThrowExceptionForHR(this.draw(
                this.comObject,
                clientDrawingContext, renderer, originX, originY));
        }
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