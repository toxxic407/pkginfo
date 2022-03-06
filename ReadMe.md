This Tool generates and updates the json file that is required to host Packages for Brew.

This Tool uses https://github.com/maxton/LibOrbisPkg

To create a new File from Scratch: 
pkginfo.exe "path/to/pkg" "developer1,developer2" "http://server.com/pkgs/" "category1,category2" "Description,line2,line3"

To add packages to an existing  json file:
pkginfo.exe "path/to/pkg" "developer1,developer2" "http://server.com/pkgs/" "category1,category2" "Description,line2,line3" "path/to/pkgs.json"
