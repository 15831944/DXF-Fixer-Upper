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

void DrawUI(element *root) {
   element *page = ColumnPanel(root, root->bounds, Captures(INTERACTION_FILEDROP));
   Label(page, "Drop DXFs Here", Size(page), 60, BLACK);

   ui_dropped_files dropped_files = GetDroppedFiles(page);
   for(u32 i = 0; i < dropped_files.count; i++) {
      string file_path = dropped_files.names[i];

      //TODO: the rest of the app
   }
}

int WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow) {
   Win32CommonInit(PlatformAllocArena(Megabyte(10)));
   ui_impl_win32_window window = createWindow("DXF Fixer Upper");
   loaded_font font = loadFont(ReadEntireFile("OpenSans-Regular.ttf", true), PlatformAllocArena(Megabyte(5)));

   UIContext ui_context = {};
   ui_context.frame_arena = PlatformAllocArena(Megabyte(2));
   ui_context.persistent_arena = PlatformAllocArena(Megabyte(2));
   ui_context.filedrop_arena = PlatformAllocArena(Megabyte(2));
   ui_context.font = &font;

   Timer timer = InitTimer();
   while(PumpMessages(&window, &ui_context)) {
      Reset(&__temp_arena);
      
      element *root_element = beginFrame(window.size, &ui_context, GetDT(&timer));
      DrawUI(root_element);
      endFrame(&window, root_element);
   }

   return 0;
}