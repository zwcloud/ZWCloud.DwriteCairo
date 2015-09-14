#include "ICairoTessellationSink.h"
#include <cairo/cairo.h>
#include "DebugUtility.h"


const GUID ICairoTessellationSink::IID_ICairoTessellationSink =
{ 0x88de7550, 0x6f56, 0x4c4f, { 0x98, 0xdf, 0xfb, 0x58, 0x9e, 0x35, 0xdb, 0x43 } };

ICairoTessellationSink::ICairoTessellationSink(cairo_t *cr_, FLOAT x_, FLOAT y_) : m_cRefCount(0), cr(cr_)
{
	cairo_set_source_rgb(cr, 1, 0, 0);
	cairo_identity_matrix(cr);
	cairo_translate(cr, x_, y_);
}

ICairoTessellationSink::~ICairoTessellationSink(void)
{
}

STDMETHODIMP_(void) ICairoTessellationSink::AddTriangles(__in_ecount(trianglesCount) CONST D2D1_TRIANGLE *triangles, UINT trianglesCount)
{
	DebugPrintf("AddTriangles, count %d\n", trianglesCount);
	// here you can copy the triangles data...
	for (UINT i = 0; i < trianglesCount; ++i)
	{
		cairo_move_to(cr, triangles[i].point1.x, triangles[i].point1.y);
		cairo_line_to(cr, triangles[i].point2.x, triangles[i].point2.y);
		cairo_line_to(cr, triangles[i].point3.x, triangles[i].point3.y);
		cairo_close_path(cr);
		cairo_fill(cr);
	}
}

STDMETHODIMP ICairoTessellationSink::Close()
{
	DebugPrintf("ICairoTessellationSink::Close\n");
	return S_OK;
}

STDMETHODIMP_(unsigned long) ICairoTessellationSink::AddRef()
{
	return InterlockedIncrement(&m_cRefCount);
}

STDMETHODIMP_(unsigned long) ICairoTessellationSink::Release()
{
	if (InterlockedDecrement(&m_cRefCount) == 0)
	{
		delete this;
		return 0;
	}
	return m_cRefCount;
}

STDMETHODIMP ICairoTessellationSink::QueryInterface(IID const& riid, void** ppvObject)
{
	if (IID_ICairoTessellationSink == riid)
		*ppvObject = dynamic_cast<ID2D1TessellationSink*>(this);
	else
	if (__uuidof(IUnknown) == riid)
		*ppvObject = dynamic_cast<IUnknown*>(this);
	else
	{
		*ppvObject = NULL;
		return E_FAIL;
	}
	return S_OK;
}