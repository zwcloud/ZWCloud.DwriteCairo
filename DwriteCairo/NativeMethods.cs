using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DwriteCairo
{
    public static class NativeMethods
    {
        private const string NativeDwriteCairo = "NativeCairo.dll";


        private static extern TextFormat CreateTextFormat(
            string fontFamilyName,
            IntPtr fontCollection,
            FontWeight fontWeight, FontStyle fontStyle, FontStretch fontStretch,
            float fontSize,
            string localeName
            );
    }
}
