# AmiiboSN-Changer
This tool is a C# copy of AnalogMan's Python script.
http://gbatemp.net/threads/release-amiibo-bin-serial-changer.464702/

I don't like to install python... thats the reason for the C# Port..

This tool generates a new "unique" serial and write it into the Amiibo bin file.
Thats make it possible to use for example the same Amiibo multiple times in a game (for example TLoZ Breath of the Wild).

## Installation
You just need the key_retail.bin next to the asnc.exe
   Google for it. I can't include it because of the copyright.

## Usage - GUI
Open it up. Its explain itself! 

## Usage - Console
The asnc.exe have to be called with at least 1 parameter.

Available parameters:
1 = Amiibo Bin Fil Path
2 (Optional) = Amount
3 (Optional) = Output Path

Example for 3 different Mario Amiibos in my Amiibo Folder:
   > asnc.exe "C:\Amiibos\Mario.bin" 3 "C:\Amiibos\NewUnique"

## Contributing
1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request :D

## Credits
AnalogMan - Python Version and the idea for a wrapper Tool
socram888 - Amiitool 
