This Tool generates and updates the json file that is required to host Packages for Brew.

This Tool uses https://github.com/maxton/LibOrbisPkg

To create a new File from Scratch: 
pkginfo.exe "path/to/pkg" "developer1,developer2" "http://server.com/pkgs/" "category1,category2" "Description,line2,line3"

To add packages to an existing  json file:
pkginfo.exe "path/to/pkg" "developer1,developer2" "http://server.com/pkgs/" "category1,category2" "Description,line2,line3" "path/to/pkgs.json"

The Program will generate an icon File, a JSON File and a param.sfo file. The param.sfo file can be deleted after the program has finished (I was too lazy to implement that part)
The icon file needs to go into the same folder where the packages are located. In case of the example this would be a folder called "pkgs" because the server url is http://server.com/pkgs/ the json file needs to be put into /data/store on the PS4 for this to work
