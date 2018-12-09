#include "windows.h"
#include "gl/gl.h"
#include "lib/wglext.h"
#include "lib/glext.h"

#define STB_IMAGE_IMPLEMENTATION
#include "lib/stb_image.h"

#define STB_TRUETYPE_IMPLEMENTATION
#include "lib/stb_truetype.h"

#define COMMON_PLATFORM
#include "lib/common.cpp"
#include "lib/ui_core.cpp"
#include "lib/ui_button.cpp"
#include "lib/ui_impl_win32_opengl.cpp"

int WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow) {
   Win32CommonInit(PlatformAllocArena(Megabyte(10)));
   ui_impl_win32_window window = createWindow("DXF Fixer Upper");
   loaded_font font = loadFont(ReadEntireFile("OpenSans-Regular.ttf", true), PlatformAllocArena(Megabyte(5)));

   UIContext ui_context = {};
   ui_context.frame_arena = PlatformAllocArena(Megabyte(2));
   ui_context.persistent_arena = PlatformAllocArena(Megabyte(2));
   ui_context.filedrop_arena = PlatformAllocArena(Megabyte(2));
   ui_context.font = &font;

   LARGE_INTEGER frequency;
   QueryPerformanceFrequency(&frequency); 

   LARGE_INTEGER timer;
   QueryPerformanceCounter(&timer);
   while(PumpMessages(&window, &ui_context)) {
      Reset(&__temp_arena);
      
      LARGE_INTEGER new_time;
      QueryPerformanceCounter(&new_time);
      f32 dt = (f32)(new_time.QuadPart - timer.QuadPart) / (f32)frequency.QuadPart;
      timer = new_time;

      element *root_element = beginFrame(window.size, &ui_context, dt);
      //TODO: literally the entire app
      endFrame(&window, root_element);
   }

   return 0;
}