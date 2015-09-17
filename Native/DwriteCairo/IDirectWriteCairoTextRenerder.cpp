#include "IDirectWriteCairoTextRenderer.h"
#include <cairo/cairo.h>
#include "ICairoTessellationSink.h"
#include "DebugUtility.h"

const GUID IDirectWriteCairoTextRenderer::IID_IDirectWriteCairoTextRenderer =
{ 0xf5b028d5, 0x86fd, 0x4332, { 0xad, 0x5e, 0xe8, 0x6c, 0xf1, 0x1c, 0xec, 0xd4 } };

IDirectWriteCairoTextRenderer::IDirectWriteCairoTextRenderer()
	: count_{ 1 }
{
	HRESULT hr = D2D1CreateFactory(D2D1_FACTORY_TYPE_SINGLE_THREADED, &pD2DFactory_);
	DebugAssert(SUCCEEDED(hr), "D2D1CreateFactoryÊ§°Ü£¡");

}

IDirectWriteCairoTextRenderer::~IDirectWriteCairoTextRenderer()
{
	if (pD2DFactory_ != nullptr)
	{
		pD2DFactory_->Release();
	}
}

HRESULT IDirectWriteCairoTextRenderer::IsPixelSnappingDisabled(void*, BOOL*)
{
	return S_OK;
}

HRESULT IDirectWriteCairoTextRenderer::GetCurrentTransform(void*, DWRITE_MATRIX*)
{
	return S_OK;
}

HRESULT IDirectWriteCairoTextRenderer::GetPixelsPerDip(void*, FLOAT*)
{
	return S_OK;
}

HRESULT IDirectWriteCairoTextRenderer::DrawGlyphRun(
	void* clientDrawingContext,
	FLOAT baselineOriginX,
	FLOAT baselineOriginY,
	DWRITE_MEASURING_MODE measuringMode,
	const DWRITE_GLYPH_RUN* glyphRun,
	const DWRITE_GLYPH_RUN_DESCRIPTION* glyphRunDescription,
	IUnknown* clientDrawingEffect)
{
	HRESULT hr;

	cr = static_cast<cairo_t*>(clientDrawingContext);
	surface = cairo_get_target(cr);

	ID2D1PathGeometry* pPathGeometry = nullptr;
	hr = pD2DFactory_->CreatePathGeometry(&pPathGeometry);
	DebugAssert(SUCCEEDED(hr), "CreatePathGeometryÊ§°Ü£¡");

	{
		ID2D1GeometrySink *pD2DSink = nullptr;
		hr = pPathGeometry->Open(&pD2DSink);
		DebugAssert(SUCCEEDED(hr), "OpenÊ§°Ü£¡");

		hr = glyphRun->fontFace->GetGlyphRunOutline(
			             glyphRun->fontEmSize,
			             glyphRun->glyphIndices,
			             glyphRun->glyphAdvances,
			             glyphRun->glyphOffsets,
			             glyphRun->glyphCount,
			             glyphRun->isSideways,
			             glyphRun->bidiLevel % 2,
						 pD2DSink);
		DebugAssert(SUCCEEDED(hr), "GetGlyphRunOutlineÊ§°Ü£¡");

		hr = pD2DSink->Close();
		DebugAssert(SUCCEEDED(hr), "CloseÊ§°Ü£¡");

		pD2DSink->Release();
	}

	{
		ICairoTessellationSink* pSink = new ICairoTessellationSink(cr, baselineOriginX, baselineOriginY);
		if (!pSink)
		{
			hr = E_OUTOFMEMORY;
		}
		DebugAssert(SUCCEEDED(hr), "ICairoGeometrySink ´´½¨Ê§°Ü£¡");
		hr = pPathGeometry->Tessellate(nullptr, pSink);
		DebugAssert(SUCCEEDED(hr), "TessellateÊ§°Ü£¡Error: %d\n", hr);
		hr = pSink->Close();
		DebugAssert(SUCCEEDED(hr), "CloseÊ§°Ü£¡");
		pSink->Release();
	}

	pPathGeometry->Release();

	return S_OK;
}

HRESULT IDirectWriteCairoTextRenderer::DrawUnderline(void*, FLOAT, FLOAT, const DWRITE_UNDERLINE*, IUnknown*)
{ return S_OK; }

HRESULT IDirectWriteCairoTextRenderer::DrawStrikethrough(void*, FLOAT, FLOAT, const DWRITE_STRIKETHROUGH*, IUnknown*)
{ return S_OK; }

HRESULT IDirectWriteCairoTextRenderer::DrawInlineObject(void*, FLOAT, FLOAT, IDWriteInlineObject*, BOOL, BOOL, IUnknown*)
{ return S_OK; }

#pragma region IUnknown interface
ULONG IDirectWriteCairoTextRenderer::AddRef()
{ return InterlockedIncrement(&count_); }

ULONG IDirectWriteCairoTextRenderer::Release()
{
	const auto count = InterlockedDecrement(&count_);
	if (!count) {
		delete this;
	}
	return count;
}

HRESULT IDirectWriteCairoTextRenderer::QueryInterface(const IID& riid, void** object)
{
	if (object == nullptr)
		return E_POINTER;
	if (__uuidof(IDWriteTextRenderer) == riid || IID_IDirectWriteCairoTextRenderer == riid || __uuidof(IUnknown) == riid) {
		*object = this;
		this->AddRef();
		return S_OK;
	}
	*object = nullptr;
	return E_FAIL;
}
#pragma endregion

#pragma region test
int main(int argc, char *argv[])
{
#if 0
	cairo_surface_t *surface =
		cairo_image_surface_create(CAIRO_FORMAT_ARGB32, 240, 80);
	cairo_t *cr =
		cairo_create(surface);

	cairo_select_font_face(cr, "serif", CAIRO_FONT_SLANT_NORMAL, CAIRO_FONT_WEIGHT_BOLD);
	cairo_set_font_size(cr, 32.0);
	cairo_set_source_rgb(cr, 0.0, 0.0, 1.0);
	cairo_move_to(cr, 10.0, 50.0);
	cairo_show_text(cr, "Hello, world");

	cairo_destroy(cr);
	cairo_surface_write_to_png(surface, "hello.png");
	cairo_surface_destroy(surface);
	return 0;
#endif


}
#pragma endregion
