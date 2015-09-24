using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Cairo;
using ZWCloud.DWriteCairo.Internal;

namespace ZWCloud.DWriteCairo
{
    /// <summary>
    /// DirectWrite TextLayout
    /// </summary>
    public class TextLayout : IDisposable
    {
        #region API

        /// <summary>
        /// Layout maximum width
        /// </summary>
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

        /// <summary>
        /// Layout maximum height
        /// </summary>
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

        public FontWeight FontWeight
        {
            get { return GetFontWeight(); }
            set { SetFontWeight(value); }
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

        #region COM internals

        [ComImport]
        [Guid("53737037-6d14-410b-9bfe-0b182bb70961")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IDWriteTextLayout { }//contains 25 method(inherited method not included)

        #region COM Method
        #region signature delegates of COM method

        [ComMethod(Index = 28)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int SetMaxWidthSignature(
            IDWriteTextLayout layout,
            float maxWidth);
        private SetMaxWidthSignature setMaxWidth;

        [ComMethod(Index = 29)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int SetMaxHeightSignature(
            IDWriteTextLayout layout,
            float maxHeight);
        private SetMaxHeightSignature setMaxHeight;
        
        [ComMethod(Index = 31)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int SetFontFamilyNameSignature(
            IDWriteTextLayout layout,
            TextRange textRange);
        private SetFontFamilyNameSignature setFontFamilyName;
        
        [ComMethod(Index = 32)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int SetFontWeightSignature(
            IDWriteTextLayout layout,
            FontWeight fontWeight,
            TextRange textRange);
        private SetFontWeightSignature setFontWeight;

        [ComMethod(Index = 33)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int SetFontStyleSignature(
            IDWriteTextLayout layout,
            FontStyle fontStyle,
            TextRange textRange);
        private SetFontStyleSignature setFontStyle;

        [ComMethod(Index = 34)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int SetFontStretchSignature(
            IDWriteTextLayout layout,
            FontStretch fontStretch,
            TextRange textRange);
        private SetFontStretchSignature setFontStretch;

        [ComMethod(Index = 35)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int SetFontSizeSignature(
            IDWriteTextLayout layout,
            float fontSize,
            TextRange textRange);
        private SetFontSizeSignature setFontSize;

        [ComMethod(Index = 42)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate float GetMaxWidthSignature(
            IDWriteTextLayout layout);
        private GetMaxWidthSignature getMaxWidth;

        [ComMethod(Index = 43)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate float GetMaxHeightSignature(
            IDWriteTextLayout layout);
        private GetMaxHeightSignature getMaxHeight;
        
        [ComMethod(Index = 45)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int GetFontFamilyNameLengthSignature(
            IDWriteTextLayout layout,
            uint currentPosition,
            out uint nameLength,
            out TextRange textRange);
        private GetFontFamilyNameLengthSignature getFontFamilyNameLength;

        [ComMethod(Index = 46)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int GetFontFamilyNameSignature(
            IDWriteTextLayout layout,
            uint currentPosition,
            string name,
            uint nameSize,
            out TextRange textRange);
        private GetFontFamilyNameSignature getFontFamilyName;

        [ComMethod(Index = 47)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int GetFontWeightSignature(
            IDWriteTextLayout layout,
            uint currentPosition,
            out FontWeight fontWeight,
            out TextRange textRange);
        private GetFontWeightSignature getFontWeight;

        [ComMethod(Index = 48)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int GetFontStyleSignature(
            IDWriteTextLayout layout,
            uint currentPosition,
            out FontStyle fontStyle,
            out TextRange textRange);
        private GetFontStyleSignature getFontStyle;
        
        [ComMethod(Index = 49)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int GetFontStretchSignature(
            IDWriteTextLayout layout,
            uint currentPosition,
            out FontStretch fontStretch,
            out TextRange textRange);
        private GetFontStretchSignature getFontStretch;
        
        [ComMethod(Index = 50)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int GetFontSizeSignature(
            IDWriteTextLayout layout,
            out float fontSize,
            out TextRange textRange);
        private GetFontSizeSignature getFontSize;

        [ComMethod(Index = 58)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int DrawSignature(
            IDWriteTextLayout layout,
            IntPtr clientDrawingContext,
            IDWriteTextRenderer renderer,
            float originX, float originY);
        private DrawSignature draw;

        [ComMethod(Index = 64)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int HitTestPointSignature(
            IDWriteTextLayout layout,
            float pointX,
            float pointY,
            out bool isTrailingHit,
            out bool isInside,
            out HitTestMetrics hitTestMetrics);
        private HitTestPointSignature hitTestPoint;
        
        [ComMethod(Index = 65)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int HitTestTextPositionSignature(
            IDWriteTextLayout layout,
            uint textPosition,
            bool isTrailingHit,
            out float pointX,
            out float pointY,
            out HitTestMetrics hitTestMetrics);
        private HitTestTextPositionSignature hitTestTextPosition;

        #endregion

        internal IntPtr Handle { get; private set; }
        private IDWriteTextLayout comObject;

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

            result = ComHelper.GetMethod(this.comObject, 28, out this.setMaxWidth);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 28);

            result = ComHelper.GetMethod(this.comObject, 29, out this.setMaxHeight);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 29);

            result = ComHelper.GetMethod(this.comObject, 31, out this.setFontFamilyName);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 31);

            result = ComHelper.GetMethod(this.comObject, 32, out this.setFontWeight);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 32);

            result = ComHelper.GetMethod(this.comObject, 33, out this.setFontStyle);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 33);

            result = ComHelper.GetMethod(this.comObject, 34, out this.setFontStretch);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 34);

            result = ComHelper.GetMethod(this.comObject, 35, out this.setFontSize);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 35);
            
            result = ComHelper.GetMethod(this.comObject, 42, out this.getMaxWidth);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 42);

            result = ComHelper.GetMethod(this.comObject, 43, out this.getMaxHeight);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 43);
            
            result = ComHelper.GetMethod(this.comObject, 45, out this.getFontFamilyNameLength);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 45);
            
            result = ComHelper.GetMethod(this.comObject, 46, out this.getFontFamilyName);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 46);
            
            result = ComHelper.GetMethod(this.comObject, 47, out this.getFontWeight);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 47);
            
            result = ComHelper.GetMethod(this.comObject, 48, out this.getFontStyle);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 48);
            
            result = ComHelper.GetMethod(this.comObject, 49, out this.getFontStretch);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 49);
            
            result = ComHelper.GetMethod(this.comObject, 50, out this.getFontSize);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 50);

            result = ComHelper.GetMethod(this.comObject, 58, out this.draw);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 58);

            result = ComHelper.GetMethod(this.comObject, 64, out this.hitTestPoint);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 64);

            result = ComHelper.GetMethod(this.comObject, 65, out this.hitTestTextPosition);
            if (!result) Debug.WriteLine("Fail to get COM method at index {0}", 65);
            
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

        #endregion

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

        private void SetFontWeight(FontWeight fontWeight)
        {
            var hr = this.setFontWeight(comObject, fontWeight, new TextRange());
            Marshal.ThrowExceptionForHR(hr);
        }

        private FontWeight GetFontWeight()
        {
            FontWeight fontWeight;
            TextRange textRangDummy;//Not used
            var hr = this.getFontWeight(comObject, 0, out fontWeight, out textRangDummy);
            Marshal.ThrowExceptionForHR(hr);
            return fontWeight;
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

        //TODO Clean these Extra wrapper methods
        internal void Show(Context context, DirectWriteCairoTextRenderer render, TextLayout textLayout)
        {
            this.Draw(context.Handle, render, (float)context.CurrentPoint.X, (float)context.CurrentPoint.Y);
        }

    }


    /// <summary>
    /// Specifies a range of text positions where format is applied.
    /// </summary>
    internal struct TextRange
    {
        /// <summary>
        /// The start text position of the range.
        /// </summary>
        public uint StartPosition;

        /// <summary>
        /// The number of text positions in the range.
        /// </summary>
        public uint Length;
    };


    /// <summary>
    /// Geometry enclosing of text positions.
    /// </summary>
    internal struct HitTestMetrics
    {
        /// <summary>
        /// First text position within the geometry.
        /// </summary>
        public uint TextPosition;

        /// <summary>
        /// Number of text positions within the geometry.
        /// </summary>
        public uint Length;

        /// <summary>
        /// Left position of the top-left coordinate of the geometry.
        /// </summary>
        public float Left;

        /// <summary>
        /// Top position of the top-left coordinate of the geometry.
        /// </summary>
        public float Top;

        /// <summary>
        /// Geometry's width.
        /// </summary>
        public float Width;

        /// <summary>
        /// Geometry's height.
        /// </summary>
        public float Height;

        /// <summary>
        /// Bidi level of text positions enclosed within the geometry.
        /// </summary>
        public uint BidiLevel;

        /// <summary>
        /// Geometry encloses text?
        /// </summary>
        public bool IsText;

        /// <summary>
        /// Range is trimmed.
        /// </summary>
        public bool IsTrimmed;
    };
}