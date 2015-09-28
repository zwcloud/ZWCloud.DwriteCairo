#include "IDirectWriteCairoTextRenderer.h"
#include <cairo/cairo.h>
#include "ICairoTessellationSink.h"
#include "DebugUtility.h"

//#define DEBUG_PRINT_GLYPH_INFO

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

	auto new_cr = static_cast<cairo_t*>(clientDrawingContext);
	if (new_cr != cr)
	{
		cr = new_cr;
		pSink = new ICairoTessellationSink(cr);
		if (!pSink)
		{
			hr = E_OUTOFMEMORY;
			return hr;
		}
	}
	//DebugPrintf(L"X %.3f Y %.3f\n", baselineOriginX, baselineOriginY);
	cairo_save(cr);
	cairo_translate(cr, baselineOriginX, baselineOriginY);

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

#ifdef  DEBUG_PRINT_GLYPH_INFO
	static wchar_t* enumStringsOfmeasuringMode[] =
	{
		L"DWRITE_MEASURING_MODE_NATURAL",
		L"DWRITE_MEASURING_MODE_GDI_CLASSIC",
		L"DWRITE_MEASURING_MODE_GDI_NATURAL"
	};
	DebugPrintf(L"Baseline X,Y: (%.3f, %.3f)\n", baselineOriginX, baselineOriginY);
	DebugPrintf(L"Measuring Mode: %s\n", enumStringsOfmeasuringMode[measuringMode]);

	for (int i = 0; i < glyphRun->glyphCount; ++i)
	{
		DebugPrintf(L"----GlyphRun %d----\n", i);
		DebugPrintf(L"Glyph Index: %d\n", glyphRun->glyphIndices[i]);
		DebugPrintf(L"Glyph Advance: %d\n", glyphRun->glyphAdvances[i]);
		DebugPrintf(L"Glyph Advance offset: %d Ascender offset:%d\n", glyphRun->glyphOffsets[i].advanceOffset,
			glyphRun->glyphOffsets[i].ascenderOffset);
	}

	DebugPrintf(L"----GlyphRun Description----\n");
	DebugPrintf(L"	Locale name: %s\n", glyphRunDescription->localeName);
	DebugPrintf(L"	String: %s\n", glyphRunDescription->string);
	DebugPrintf(L"	String length: %d\n", glyphRunDescription->stringLength);
	DebugPrintf(L"	Cluster map:\n		");
	for (int i = 0; i < glyphRunDescription->stringLength; ++i)
	{
		DebugPrintf(L"%d, ", glyphRunDescription->clusterMap[i]);
	}
	DebugPrintf(L"\n");
	DebugPrintf(L"	Text position: %d\n", glyphRunDescription->textPosition);
#endif

	{
		DebugAssert(SUCCEEDED(hr), "ICairoGeometrySink ´´½¨Ê§°Ü£¡");
		hr = pPathGeometry->Tessellate(nullptr, pSink);
		DebugAssert(SUCCEEDED(hr), "TessellateÊ§°Ü£¡Error: %d\n", hr);
		hr = pSink->Close();
		DebugAssert(SUCCEEDED(hr), "CloseÊ§°Ü£¡");

		pSink->Release();
	}

	pPathGeometry->Release();

	cairo_restore(cr);
	//DebugPrintf("%s\n", cairo_status_to_string(cairo_status(cr)));
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
	if (__uuidof(IDWriteTextRenderer) == riid || IID_IDirectWriteCairoTextRenderer == riid || __uuidof(IUnknown) == riid)
	{
		*object = this;
		this->AddRef();
		return S_OK;
	}
	*object = nullptr;
	return E_FAIL;
}
#pragma endregion
