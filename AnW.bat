             
@echo off	

set mydate=%date%
set mydate=%mydate: =_%
set mydate=%mydate:/=_%

set datetimef=%mydate%_%time:~0,2%_%time:~3,2%_%time:~6,2%

	"C:\Program Files\Gallio\bin\Gallio.Echo.exe" "E:\Project\DailyRun\AnW\_output\Regression_AnW_Suite2.dll" "/rt:html" "/rnf:AnW_BVT_%datetimef%"

set datetimef=%mydate%_%time:~0,2%_%time:~3,2%_%time:~6,2%
	"C:\Program Files\Gallio\bin\Gallio.Echo.exe" "E:\Project\DailyRun\AnW\_output\BVT_AnW_General_Suite1.dll" "/rt:html" "/rnf:AnW_BVT_%datetimef%"
	

