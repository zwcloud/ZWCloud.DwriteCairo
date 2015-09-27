#pragma once
#include <dwrite.h>
#include <d2d1.h>
#include <cairo\cairo.h>

struct ICairoTessellationSink;
struct IDirectWriteCairoTextRenderer : IDWriteTextRenderer
{
	// {F5B028D5-86FD-4332-AD5E-E86CF11CECD4}
	static const GUID IID_IDirectWriteCairoTextRenderer;
	IDirectWriteCairoTextRenderer();
	virtual ~IDirectWriteCairoTextRenderer();
	STDMETHODIMP IsPixelSnappingDisabled(__maybenull void*, __out BOOL*) override;
	STDMETHODIMP GetCurrentTransform(__maybenull void*, __out DWRITE_MATRIX*) override;
	STDMETHODIMP GetPixelsPerDip(__maybenull void*, __out FLOAT*) override;;
	STDMETHODIMP DrawGlyphRun(__maybenull void*, FLOAT, FLOAT, DWRITE_MEASURING_MODE, __in const DWRITE_GLYPH_RUN*, __in const DWRITE_GLYPH_RUN_DESCRIPTION*, IUnknown*) override;
	STDMETHODIMP DrawUnderline(__maybenull void*, FLOAT, FLOAT, __in const DWRITE_UNDERLINE*, IUnknown*) override;
	STDMETHODIMP DrawStrikethrough(__maybenull void*, FLOAT, FLOAT, __in const DWRITE_STRIKETHROUGH*, IUnknown*) override;
	STDMETHODIMP DrawInlineObject(__maybenull void*, FLOAT, FLOAT, IDWriteInlineObject*, BOOL, BOOL, IUnknown*) override;

	#pragma region IUnknown interface
	STDMETHODIMP_(ULONG) AddRef() override;
	STDMETHODIMP_(ULONG) Release() override;
	STDMETHODIMP QueryInterface(const IID& riid, void** object) override;
	#pragma endregion

private:
	ULONG volatile count_;
	ID2D1Factory* pD2DFactory_;

	cairo_t *cr;
	ICairoTessellationSink* pSink;
};
