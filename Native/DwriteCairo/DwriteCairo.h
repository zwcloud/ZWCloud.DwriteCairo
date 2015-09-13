#pragma once
#include "IDirectWriteCairoTextRenderer.h"
#include <cairo\cairo.h>

/// <summary>
/// Creates a DirectWrite factory object that is used for subsequent creation of individual DirectWrite objects.
/// </summary>
/// <param name="factoryType">Identifies whether the factory object will be shared or isolated.</param>
/// <param name="iid">Identifies the DirectWrite factory interface, such as __uuidof(IDWriteFactory).</param>
/// <param name="factory">Receives the DirectWrite factory object.</param>
/// <returns>
/// Standard HRESULT error code.
/// </returns>
/// <remarks>
/// Obtains DirectWrite factory object that is used for subsequent creation of individual DirectWrite classes.
/// DirectWrite factory contains internal state such as font loader registration and cached font data.
/// In most cases it is recommended to use the shared factory object, because it allows multiple components
/// that use DirectWrite to share internal DirectWrite state and reduce memory usage.
/// However, there are cases when it is desirable to reduce the impact of a component,
/// such as a plug-in from an untrusted source, on the rest of the process by sandboxing and isolating it
/// from the rest of the process components. In such cases, it is recommended to use an isolated factory for the sandboxed
/// component.
/// </remarks>
extern "C" HRESULT __stdcall DwriteCairoCreateTextRender(
	_In_ REFIID iid,
	_COM_Outptr_ IUnknown **textRender
	);
