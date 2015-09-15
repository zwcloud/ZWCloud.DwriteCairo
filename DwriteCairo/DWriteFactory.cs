using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using ZWCloud.DWriteCairo.Internal;

namespace ZWCloud.DWriteCairo
{
    public sealed class DWriteFactory : IDisposable
    {
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

            result = ComHelper.GetComMethod(this.comObject, 15/*index of the createTextFormat signature in interface*/,
                                       out this.createTextFormat);
            if(!result)
            {
                Debug.WriteLine("Fail to get COM method at index {0}", 15);
            }

            result = ComHelper.GetComMethod(this.comObject, 18/*index of the createTextFormat signature in interface*/,
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
            Marshal.ThrowExceptionForHR(this.createTextFormat(
                                  this.comObject,
                                  fontFamilyName, fontCollection, fontWeight, fontStyle, fontStretch, fontSize, "en-us", out textFormat));
            if(textFormat!=IntPtr.Zero)
            {
                Debug.WriteLine("TextFormat created.");
            }
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
            Marshal.ThrowExceptionForHR(this.createTextLayout(
                                  this.comObject,
                                  text, textLength, textFormat, maxWidth, maxHeight,
                                  out textLayout));
            if (textLayout != IntPtr.Zero)
            {
                Debug.WriteLine("TextLayout created.");
            }
            return textLayout; 
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

    /// <summary>
    /// The font weight enumeration describes common values for degree of blackness or thickness of strokes of characters in a font.
    /// Font weight values less than 1 or greater than 999 are considered to be invalid, and they are rejected by font API functions.
    /// </summary>
    public enum FontWeight
    {
        /// <summary>
        /// Predefined font weight : Thin (100).
        /// </summary>
        DWRITE_FONT_WEIGHT_THIN = 100,

        /// <summary>
        /// Predefined font weight : Extra-light (200).
        /// </summary>
        DWRITE_FONT_WEIGHT_EXTRA_LIGHT = 200,

        /// <summary>
        /// Predefined font weight : Ultra-light (200).
        /// </summary>
        DWRITE_FONT_WEIGHT_ULTRA_LIGHT = 200,

        /// <summary>
        /// Predefined font weight : Light (300).
        /// </summary>
        DWRITE_FONT_WEIGHT_LIGHT = 300,

        /// <summary>
        /// Predefined font weight : Semi-light (350).
        /// </summary>
        DWRITE_FONT_WEIGHT_SEMI_LIGHT = 350,

        /// <summary>
        /// Predefined font weight : Normal (400).
        /// </summary>
        DWRITE_FONT_WEIGHT_NORMAL = 400,

        /// <summary>
        /// Predefined font weight : Regular (400).
        /// </summary>
        DWRITE_FONT_WEIGHT_REGULAR = 400,

        /// <summary>
        /// Predefined font weight : Medium (500).
        /// </summary>
        DWRITE_FONT_WEIGHT_MEDIUM = 500,

        /// <summary>
        /// Predefined font weight : Demi-bold (600).
        /// </summary>
        DWRITE_FONT_WEIGHT_DEMI_BOLD = 600,

        /// <summary>
        /// Predefined font weight : Semi-bold (600).
        /// </summary>
        DWRITE_FONT_WEIGHT_SEMI_BOLD = 600,

        /// <summary>
        /// Predefined font weight : Bold (700).
        /// </summary>
        DWRITE_FONT_WEIGHT_BOLD = 700,

        /// <summary>
        /// Predefined font weight : Extra-bold (800).
        /// </summary>
        DWRITE_FONT_WEIGHT_EXTRA_BOLD = 800,

        /// <summary>
        /// Predefined font weight : Ultra-bold (800).
        /// </summary>
        DWRITE_FONT_WEIGHT_ULTRA_BOLD = 800,

        /// <summary>
        /// Predefined font weight : Black (900).
        /// </summary>
        DWRITE_FONT_WEIGHT_BLACK = 900,

        /// <summary>
        /// Predefined font weight : Heavy (900).
        /// </summary>
        DWRITE_FONT_WEIGHT_HEAVY = 900,

        /// <summary>
        /// Predefined font weight : Extra-black (950).
        /// </summary>
        DWRITE_FONT_WEIGHT_EXTRA_BLACK = 950,

        /// <summary>
        /// Predefined font weight : Ultra-black (950).
        /// </summary>
        DWRITE_FONT_WEIGHT_ULTRA_BLACK = 950
    }

    /// <summary>
    /// The font stretch enumeration describes relative change from the normal aspect ratio
    /// as specified by a font designer for the glyphs in a font.
    /// Values less than 1 or greater than 9 are considered to be invalid, and they are rejected by font API functions.
    /// </summary>
    public enum FontStretch
    {
        /// <summary>
        /// Predefined font stretch : Not known (0).
        /// </summary>
        DWRITE_FONT_STRETCH_UNDEFINED = 0,

        /// <summary>
        /// Predefined font stretch : Ultra-condensed (1).
        /// </summary>
        DWRITE_FONT_STRETCH_ULTRA_CONDENSED = 1,

        /// <summary>
        /// Predefined font stretch : Extra-condensed (2).
        /// </summary>
        DWRITE_FONT_STRETCH_EXTRA_CONDENSED = 2,

        /// <summary>
        /// Predefined font stretch : Condensed (3).
        /// </summary>
        DWRITE_FONT_STRETCH_CONDENSED = 3,

        /// <summary>
        /// Predefined font stretch : Semi-condensed (4).
        /// </summary>
        DWRITE_FONT_STRETCH_SEMI_CONDENSED = 4,

        /// <summary>
        /// Predefined font stretch : Normal (5).
        /// </summary>
        DWRITE_FONT_STRETCH_NORMAL = 5,

        /// <summary>
        /// Predefined font stretch : Medium (5).
        /// </summary>
        DWRITE_FONT_STRETCH_MEDIUM = 5,

        /// <summary>
        /// Predefined font stretch : Semi-expanded (6).
        /// </summary>
        DWRITE_FONT_STRETCH_SEMI_EXPANDED = 6,

        /// <summary>
        /// Predefined font stretch : Expanded (7).
        /// </summary>
        DWRITE_FONT_STRETCH_EXPANDED = 7,

        /// <summary>
        /// Predefined font stretch : Extra-expanded (8).
        /// </summary>
        DWRITE_FONT_STRETCH_EXTRA_EXPANDED = 8,

        /// <summary>
        /// Predefined font stretch : Ultra-expanded (9).
        /// </summary>
        DWRITE_FONT_STRETCH_ULTRA_EXPANDED = 9
    };

    /// <summary>
    /// The font style enumeration describes the slope style of a font face, such as Normal, Italic or Oblique.
    /// Values other than the ones defined in the enumeration are considered to be invalid, and they are rejected by font API functions.
    /// </summary>
    public enum FontStyle
    {
        /// <summary>
        /// Font slope style : Normal.
        /// </summary>
        DWRITE_FONT_STYLE_NORMAL,

        /// <summary>
        /// Font slope style : Oblique.
        /// </summary>
        DWRITE_FONT_STYLE_OBLIQUE,

        /// <summary>
        /// Font slope style : Italic.
        /// </summary>
        DWRITE_FONT_STYLE_ITALIC

    };
}