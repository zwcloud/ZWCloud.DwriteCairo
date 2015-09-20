using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using ZWCloud.DWriteCairo.Internal;

namespace ZWCloud.DWriteCairo
{
    public sealed class DWriteFactory : IDisposable
    {
        #region COM internals

        [ComImport]
        [Guid(DirectXUtil.DirectWrite.IID_IDwriteFactory)]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IDWriteFactory { }

        #region signature delegates of COM interface method

        [ComMethod(Index = 15)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
        private delegate int CreateTextFormatSignature(
            IDWriteFactory factory,
            string fontFamilyName,
            IntPtr fontCollection,
            FontWeight fontWeight, FontStyle fontStyle, FontStretch fontStretch,
            float fontSize,
            string localeName,
            out IntPtr textFormat);

        [ComMethod(Index = 18)]
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
        private delegate int CreateTextLayoutSignature(
            IDWriteFactory factory,
            string text,
            int textLength,
            IntPtr textFormat,
            float maxWidth, float maxHeight,
            out IntPtr textLayout);

        #endregion

        public static DWriteFactory Create()
        {
            return new DWriteFactory(DirectXUtil.DirectWrite.CreateFactory());
        }

        private IDWriteFactory comObject;
        private CreateTextFormatSignature createTextFormat;
        private CreateTextLayoutSignature createTextLayout;

        private DWriteFactory(IDWriteFactory obj)
        {
            this.comObject = obj;

            bool result;

            result = ComHelper.GetMethod(this.comObject, 15,
                                       out this.createTextFormat);
            if(!result)
            {
                Debug.WriteLine("Fail to get COM method at index {0}", 15);
            }

            result = ComHelper.GetMethod(this.comObject, 18,
                                       out this.createTextLayout);
            if (!result)
            {
                Debug.WriteLine("Fail to get COM method at index {0}", 18);
            }

        }

        ~DWriteFactory()
        {
            this.Release();
        }

        private void Release()
        {
            if (this.comObject != null)
            {
                Marshal.ReleaseComObject(this.comObject);
                this.comObject = null;
                this.createTextFormat = null;
                this.createTextLayout = null;
            }
        }

        #region COM Method wrappers

        /// <summary>
        /// Create a text format object used for text layout.
        /// </summary>
        /// <param name="fontFamilyName">Name of the font family</param>
        /// <param name="fontCollection">Font collection. NULL indicates the system font collection.</param>
        /// <param name="fontWeight">Font weight</param>
        /// <param name="fontStyle">Font style</param>
        /// <param name="fontStretch">Font stretch</param>
        /// <param name="fontSize">Logical size of the font in DIP units. A DIP ("device-independent pixel") equals 1/96 inch.</param>
        /// <param name="localeName">Locale name</param>
        /// <returns> newly created text format object </returns>
        /// <remarks>
        /// <code>
        /// STDMETHOD(CreateTextFormat)(
        ///     _In_z_ WCHAR const* fontFamilyName,
        ///     _In_opt_ IDWriteFontCollection* fontCollection,
        ///     DWRITE_FONT_WEIGHT fontWeight,
        ///     DWRITE_FONT_STYLE fontStyle,
        ///     DWRITE_FONT_STRETCH fontStretch,
        ///     FLOAT fontSize,
        ///     _In_z_ WCHAR const* localeName,
        ///     _COM_Outptr_ IDWriteTextFormat** textFormat
        ///     ) PURE;
        /// </code>
        /// </remarks>
        internal IntPtr CreateTextFormat(
            string fontFamilyName,
            IntPtr fontCollection,
            FontWeight fontWeight, FontStyle fontStyle, FontStretch fontStretch,
            float fontSize,
            string localeName)
        {
            IntPtr textFormat;
            var hr = this.createTextFormat(
                this.comObject,
                fontFamilyName, fontCollection, fontWeight, fontStyle,
                fontStretch, fontSize, localeName, out textFormat);
            Marshal.ThrowExceptionForHR(hr);
            return textFormat;
        }

        /// <summary>
        /// CreateTextLayout takes a string, format, and associated constraints
        /// and produces an object representing the fully analyzed
        /// and formatted result.
        /// </summary>
        /// <param name="text">The text to layout.</param>
        /// <param name="textLength">The length of the string.</param>
        /// <param name="textFormat">The format to apply to the string.</param>
        /// <param name="maxWidth">Width of the layout box.</param>
        /// <param name="maxHeight">Height of the layout box.</param>
        /// <returns>
        /// The resultant object.
        /// </returns>
        /// <remarks>
        /// <code>
        /// STDMETHOD(CreateTextLayout)(
        ///     _In_reads_(stringLength) WCHAR const* string,
        ///     UINT32 stringLength,
        ///     _In_ IDWriteTextFormat* textFormat,
        ///     FLOAT maxWidth,
        ///     FLOAT maxHeight,
        ///     _COM_Outptr_ IDWriteTextLayout** textLayout
        /// ) PURE;
        /// </code>
        /// </remarks>
        internal IntPtr CreateTextLayout(string text, int textLength, IntPtr textFormat, float maxWidth, float maxHeight)
        {
            IntPtr textLayout;
            var hr = this.createTextLayout(
                this.comObject,
                text, textLength, textFormat, maxWidth, maxHeight,
                out textLayout);
            Marshal.ThrowExceptionForHR(hr);
            return textLayout; 
        }

        #endregion

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