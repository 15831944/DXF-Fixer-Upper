@echo off
pushd Source

call vcvarsall x86_amd64
cl -wd4838 -Zi -Od -Fedxf_fixer_upper_win32 dxf_fixer_upper_win32.cpp user32.lib gdi32.lib opengl32.lib shell32.lib
copy dxf_fixer_upper_win32.exe "../build/dxf_fixer_upper_win32.exe"
copy dxf_fixer_upper_win32.pdb "../build/dxf_fixer_upper_win32.pdb"
del *.obj *.pdb *.ilk *.exe

popd
pause
exit