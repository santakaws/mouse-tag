echo First remove old binary files
rm *.dll
rm *.exe

echo View the list of source files
ls -l

echo Compile mouseTagUI.cs to create the file: mouseTagUI.dll
mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll -out:mouseTagUI.dll mouseTagUI.cs

echo Compile mouseTagMain.cs and link the two previously created dll files to create an executable file. 
mcs -r:System -r:System.Windows.Forms -r:mouseTagUI.dll -out:tag.exe mouseTagMain.cs

echo View the list of files in the current folder
ls -l

echo Run the Assignment 1 program.
./tag.exe

echo The script has terminated.