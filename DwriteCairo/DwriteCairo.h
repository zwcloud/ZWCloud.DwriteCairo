#pragma once
#include <cairo\cairo.h>
#include "DwriteLayout.h"

DwriteLayout* dwrite_cairo_create_layout(cairo_t* cr);
void dwrite_cairo_update_layout(cairo_t* cr, DwriteLayout* layout);
void dwrite_cairo_show_layout(cairo_t *cr, DwriteLayout* layout);