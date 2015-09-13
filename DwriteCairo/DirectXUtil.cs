using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DwriteCairo
{
    public static class DirectXUtil
    {
        public static class D2D
        {
            public const string IID_ID2D1Factory =
                "06152247-6f50-465a-9245-118bfd3b6007";

            public enum FactoryType
            {
                SingleThreaded,
                MultiThreaded,
            }

            public struct FactoryOptions
            {
                D2D1DebugLevel debugLevel;
            }

            public enum D2D1DebugLevel
            {
                D2D1_DEBUG_LEVEL_NONE = 0,
                D2D1_DEBUG_LEVEL_ERROR = 1,
                D2D1_DEBUG_LEVEL_WARNING = 2,
                D2D1_DEBUG_LEVEL_INFORMATION = 3,
            }

            [DllImport("d2d1.dll", PreserveSig = false)]
            [return: MarshalAs(UnmanagedType.Interface)]
            private static extern object D2D1CreateFactory(FactoryType factoryType,
                [MarshalAs(UnmanagedType.LPStruct)] Guid riid,
                FactoryOptions pFactoryOptions);

            public static D2D1Factory.ID2D1Factory CreateFactory()
            {
                FactoryOptions opts = new FactoryOptions();
                object factory = D2D1CreateFactory(
                    FactoryType.SingleThreaded,
                    new Guid(IID_ID2D1Factory), opts);
                if (factory != null)
                {
                    Debug.WriteLine("ID2D1Factory created.");
                }
                return (D2D1Factory.ID2D1Factory)factory;
            }
        }

        public static class DirectWrite
        {
            #region DwriteFactory

            public const string IID_IDwriteFactory =
                "b859ee5a-d838-4b5b-a2e8-1adc7d93db48";

            private enum FactoryType
            {
                Shared,
                Isolated
            }

            [DllImport("dwrite.dll", PreserveSig = false)]
            [return: MarshalAs(UnmanagedType.Interface)]
            private static extern object DWriteCreateFactory(FactoryType factoryType,
                [MarshalAs(UnmanagedType.LPStruct)] Guid riid);

            public static DWriteFactory.IDWriteFactory CreateFactory()
            {
                object factory = DWriteCreateFactory(
                    FactoryType.Shared,
                    new Guid(IID_IDwriteFactory));
                if (factory != null)
                {
                    Debug.WriteLine("IDwriteFactory created.");
                }
                return (DWriteFactory.IDWriteFactory)factory;
            }

            #endregion

            #region DirectCairo
            public const string IID_IDirectWriteCairoTextRenderer =
                "F5B028D5-86FD-4332-AD5E-E86CF11CECD4";
            [DllImport("NativeDwriteCairo.dll", PreserveSig = false)]
            [return: MarshalAs(UnmanagedType.Interface)]
            private static extern object DwriteCairoCreateTextRender(
                [MarshalAs(UnmanagedType.LPStruct)] Guid riid);


            public static DirectWriteCairoTextRenderer.IDirectWriteCairoTextRenderer CreateTextRender()
            {
                object render = DwriteCairoCreateTextRender(new Guid(IID_IDirectWriteCairoTextRenderer));
                if (render != null)
                {
                    Debug.WriteLine("IDirectWriteCairoTextRenderer created.");
                }
                return (DirectWriteCairoTextRenderer.IDirectWriteCairoTextRenderer)render;
            }
            #endregion
        }

    }
}