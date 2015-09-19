#pragma once
#include <d2d1.h>
#include <cairo/cairo.h>

struct ICairoTessellationSink : ID2D1TessellationSink
{
	// {88DE7550-6F56-4C4F-98DF-FB589E35DB43}
	static const GUID IID_ICairoTessellationSink;
	ICairoTessellationSink(__notnull cairo_t *cr);
	virtual ~ICairoTessellationSink(void);
	STDMETHOD_(void, AddTriangles)(
		__in_ecount(trianglesCount) CONST D2D1_TRIANGLE *triangles, UINT trianglesCount
		) override;
	STDMETHOD(Close)() override;

	#pragma region IUnknown interface
	unsigned long STDMETHODCALLTYPE AddRef() override;
	unsigned long STDMETHODCALLTYPE Release() override;
	HRESULT STDMETHODCALLTYPE QueryInterface(IID const& riid, void** ppvObject) override;
	#pragma endregion

private:
	LONG volatile m_cRefCount;
	cairo_t *cr;
};