# AmiiboSN-Changer
This tool is a C# copy of AnalogMan's Python script.
http://gbatemp.net/threads/release-amiibo-bin-serial-changer.464702/
All prop to him.
I just don't like to install python.

This tool generates new "unique" keys and write them into the Amiibo bin files.
Thats make it possible to use for example the same Amiibo multiple times in a game (for example Zelda Breath of the Wild).

## Installation
You just need the key_retail.bin.
Google for it. It is Nintendo copyrighted, so i can't include it.

## Usage
The asnc.exe have to be called with 1 or 2 parameters.
First parameter is the path to the Amiibo.bin file.

- Examples for 5 different SSB4_Link Amiibo's.
   > asnc.exe "SSB4_Link.bin" 5

## Contributing
1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request :D

## Credits
AnalogMan - Who had the idea for the wrapper application
socram888 - Amiitool 
