# AmiiboSN-Changer
This tool generates a new "unique" serial and write it into a selected Amiibo dumb file.
Thats make it possible to use the same Amiibo multiple times in a game (for example TLoZ Breath of the Wild).

Since the console application is written in dotnet, it should be possible to use the application on other platforms.
Currently only a portable build is provided.

Tested with N2Elite Tags and TagMo

## Installation
You require a key_retail.bin (MD5 45FD53569F5765EEF9C337BD5172F937) file. Since the file is copyrighted, i cannot be provided with the tool.
Put the key_retail.bin file into the keys directory or adjust the appsettings.json file.

## Usage - GUI
Add amiibos by clicking on the "+" Button at the top or drag&drop them into the left field.
Adjust the required settings in the lower right and press the Start button ">".

If an amiibo couldn't be decrypted while loding, the Start button will be disabled. The indicator is the lock in the grid on the left side.

![](Images/asnc-gui.png)

## Usage - Console
To use the console application you require DotNet 2.1

For the portable build, call like in this example
```
dotnet "D:\AmiiboSN-Changer\ASNC.dll" -a "D:\AmiiboSN-Changer\Amiibos\8BitMario.bin" -c 2 -o "D:\AmiiboSN-Changer\Amiibos\Output"
```

## Credits
AnalogMan - Simpler Python Version and the idea for a wrapper Tool
Falco20019 - Awesome amiibo library (libamiibo)


Feel free to open a issue if you have problems.
