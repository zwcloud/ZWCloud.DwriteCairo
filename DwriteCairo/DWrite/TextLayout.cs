using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Cairo;
using ZWCloud.DWriteCairo.Internal;

namespace ZWCloud.DWriteCairo.DWrite
{
    public class TextLayout : IDisposable
    {
        #region COM internals

        [ComImport]
        [Guid("53737037-6d14-410b-9bfe-0b182bb70961")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IDWriteTextLayout { }//contains 25 method(inherited method not included)

        [ComImport]
        [Guid("ef8a8135-5cc6-45fe-8825-c5a0724eb819")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IDWriteTextRenderer { }
        
        #region signature delegates of COM method

        [ComMethod(Index = 28)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int SetMaxWidthSignature(
            IDWriteTextLayout layout,
            float maxWidth);

        [ComMethod(Index = 29)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int SetMaxHeightSignature(
            IDWriteTextLayout layout,
            float maxHeight);

        [ComMethod(Index = 42)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate float GetMaxWidthSignature(
            IDWriteTextLayout layout);

        [ComMethod(Index = 43)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate float GetMaxHeightSignature(
            IDWriteTextLayout layout);

        [ComMethod(Index=58)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int DrawSignature(
            IDWriteTextLayout layout,
            IntPtr clientDrawingContext,
            IDWriteTextRenderer renderer,
            float originX, float originY);


        #endregion

        internal IntPtr Handle { get; private set; }
        private IDWriteTextLayout comObject;
        private SetMaxWidthSignature setMaxWidth;
        private SetMaxHeightSignature setMaxHeight;
        private GetMaxWidthSignature getMaxWidth;
        private GetMaxHeightSignature getMaxHeight;
        private DrawSignature draw;

        internal TextLayout(IntPtr objPtr)
        {
            this.Handle = objPtr;
            this.comObject =
                (IDWriteTextLayout) Marshal.GetObjectForIUnknown(objPtr);

            InitComMethods();
        }
        internal void InitComMethods()
        {
            bool result;

            result = ComHelper.GetComMethod(this.comObject, 28, out this.setMaxWidth);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 28);

            result = ComHelper.GetComMethod(this.comObject, 29, out this.setMaxHeight);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 29);

            result = ComHelper.GetComMethod(this.comObject, 42, out this.getMaxWidth);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 42);

            result = ComHelper.GetComMethod(this.comObject, 43, out this.getMaxHeight);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 43);

            result = ComHelper.GetComMethod(this.comObject, 58, out this.draw);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 58); 

            
        }

        ~TextLayout()
        {
            this.Release();
        }

        private void Release()
        {
            if (this.comObject != null)
            {
                Marshal.ReleaseComObject(this.comObject);
                this.comObject = null;

                this.setMaxWidth = null;
                this.setMaxHeight = null;
                this.getMaxWidth = null;
                this.getMaxHeight = null;
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
        /// <remarks>
        /// Native signature of the COM method
        /// <code>
        /// STDMETHOD(Draw)(
        ///     _In_opt_ void* clientDrawingContext,
        ///     _In_ IDWriteTextRenderer* renderer,
        ///     FLOAT originX,
        ///     FLOAT originY
        ///     ) PURE;
        /// </code>
        /// </remarks>
        private void Draw(
            IntPtr clientDrawingContext, DirectWriteCairoTextRenderer renderer,
            float originX, float originY)
        {
            var dwriterender = (IDWriteTextRenderer) renderer.comObject; 
            Marshal.ThrowExceptionForHR(this.draw(
                this.comObject,
                clientDrawingContext, dwriterender, originX, originY));
        }
        
        /// <summary>
        /// Set layout maximum width
        /// </summary>
        /// <param name="maxWidth">Layout maximum width</param>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        /// <remarks>
        /// <code>
        /// STDMETHOD(SetMaxWidth)(
        ///     FLOAT maxWidth
        ///     ) PURE;
        /// </code>
        /// </remarks>
        private void SetMaxWidth(float maxWidth)
        {
            Marshal.ThrowExceptionForHR(this.setMaxWidth(this.comObject,
                maxWidth));
        }


        /// <summary>
        /// Set layout maximum height
        /// </summary>
        /// <param name="maxHeight">Layout maximum height</param>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        /// <remarks>
        /// <code>
        /// STDMETHOD(SetMaxHeight)(
        ///     FLOAT maxHeight
        ///     ) PURE;
        /// </code>
        /// </remarks>
        private void SetMaxHeight(float maxHeight)
        {
            Marshal.ThrowExceptionForHR(this.setMaxHeight(this.comObject,
                maxHeight));
        }

        /// <summary>
        /// Get layout maximum width
        /// </summary>
        /// STDMETHOD_(FLOAT, GetMaxWidth)() PURE;
        private float GetMaxWidth()
        {
            return this.getMaxWidth(this.comObject);
        }
        
        /// <summary>
        /// Get layout maximum height
        /// </summary>
        /// <remarks>
        /// <code>
        /// STDMETHOD_(FLOAT, GetMaxHeight)() PURE;
        /// </code>
        /// </remarks>
        private float GetMaxHeight()
        {
            return this.getMaxHeight(this.comObject);
        }

        #endregion



        #region Implementation of IDisposable

        public void Dispose()
        {
            this.Release();
            GC.SuppressFinalize(this);
        }

        #endregion

        #endregion

        #region API

        public float Width
        {
            get
            {
                return GetMaxWidth();
            }
            set
            {
                SetMaxWidth(value);
            }
        }
        
        public float Height
        {
            get
            {
                return GetMaxHeight();
            }
            set
            {
                SetMaxHeight(value);
            }
        }

        public void Show(Context context, DirectWriteCairoTextRenderer render, TextLayout textLayout)
        {
            this.Draw(context.Handle, render, 0, 0);//Do not move the origin
        }

        /*TODO*/

        public void SetText(string text)
        {

        }

        public void Update()
        {

        }

        public void XyToIndex()
        {

        }

        #endregion
    }
}