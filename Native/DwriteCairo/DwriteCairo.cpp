#include "DwriteCairo.h"
#include <dwrite.h>
#include "DebugUtility.h"

HRESULT __stdcall DwriteCairoCreateTextRender(
	_In_ REFIID iid,
	_COM_Outptr_ IUnknown **textRender
	)
{
	if (textRender == nullptr)
		return E_POINTER;

	DebugAssert(iid == IDirectWriteCairoTextRenderer::IID_IDirectWriteCairoTextRenderer,
		"IID must be GUID of IDirectWriteCairoTextRenderer");

	*textRender = new IDirectWriteCairoTextRenderer();

	return S_OK;
}