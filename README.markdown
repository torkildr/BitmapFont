Bitmap Font Export Utility
==========================

Author: Torkild Retvedt
This software is Licensed under [GPLv3] [2]

Utility for creating variable width bitmap font, created for Atmega 328, to be
displayed on a 128x8 dot matrix led display ([this project] [1]).

The source should be able to compile both with Visual Studio or mono:

    msbuild.exe BitmapFont.sln

or

    xbuild BitmapFont.sln

The program can be run, thusly:

    ./bin/Debug/BitmapFont.exe ./alphabet.png ./alphabet.txt ./header.h

[1]: https://github.com/torkildr/display/
[2]: http://www.gnu.org/licenses/gpl-3.0.txt

