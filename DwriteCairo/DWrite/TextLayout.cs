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
        public interface IDWriteTextLayout { }

        [ComImport]
        [Guid("ef8a8135-5cc6-45fe-8825-c5a0724eb819")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IDWriteTextRenderer { }
        
        #region signature delegates of COM method
        [ComMethod(Index=58)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int DrawSignature(
            IDWriteTextLayout layout,
            IntPtr clientDrawingContext,
            IDWriteTextRenderer renderer,
            float originX, float originY);

        #endregion

        internal IntPtr Handle
        {
            get { return Marshal.GetIUnknownForObject(this.comObject); }
        }
        private IDWriteTextLayout comObject;
        private DrawSignature draw;

        internal TextLayout(IntPtr objPtr)
        {
            this.comObject =
                (IDWriteTextLayout) Marshal.GetObjectForIUnknown(objPtr);

            InitComMethods();
        }
        internal TextLayout(IDWriteTextLayout obj)
        {
            this.comObject = obj;
            InitComMethods();
        }
        internal void InitComMethods()
        {
            bool result;

            result = ComHelper.GetComMethod(this.comObject, 58/*index of the draw signature in the interface*/,
                                       out this.draw);
            if (!result)
            {
                Debug.WriteLine("Fail to get COM method at index {0}", 58);
            }
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
                //TODO pass cairo context here into the renderer and use cairo_get_target to get the surface in native codes
                clientDrawingContext, dwriterender, originX, originY));
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

        public float width;
        public float Width
        {
            //TODO implement this from COM method IDWriteTextLayout::GetMaxHeight
            get { return width; }
            //TODO implement this from COM method IDWriteTextLayout::SetMaxHeight
            set { width = value; }
        }


        public void Show(Context context, DirectWriteCairoTextRenderer render, TextLayout textLayout)
        {
            var surface = context.GetTarget();
            //this.Draw(surface.Handle, render, textLayout.);
            
        }

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