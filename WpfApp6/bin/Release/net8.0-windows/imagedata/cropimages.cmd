@echo off
setlocal enabledelayedexpansion

:: Coordinates for the crop area (width x height + x + y)
set "crop_area=352x464+694+90"

:: Loop through all image files in the root directory
for %%I in (*.jpg *.jpeg *.png *.bmp *.gif) do (
    echo Cropping "%%I"...
    magick convert "%%I" -crop %crop_area% +repage "%%I"
)

echo Cropping completed.
pause