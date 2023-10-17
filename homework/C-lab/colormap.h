// Header File for Bullseye.c
#ifndef COLORMAP_H
#define COLORMAP_H
//#include <colormap.c>
#include <png.h>

typedef struct {
    png_byte red;
    png_byte green;
    png_byte blue;
} rgb;

//struct rgb value2color(float v, float vmin, float vmax);
rgb value2color(float v, float vmin, float vmax);

//  according to chatgpt this is an include guard
#endif // COLORMAP_H