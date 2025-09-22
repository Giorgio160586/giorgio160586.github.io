@echo off
setlocal ENABLEDELAYEDEXPANSION

set SITE_URL=https://giorgio160586.github.io
set OUTPUT_FILE=sitemap.xml
set ROOT_PATH=%cd%

echo Generating sitemap...

(
  echo ^<?xml version="1.0" encoding="UTF-8"?^>
  echo ^<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9"^>
) > %OUTPUT_FILE%

for /r "%ROOT_PATH%" %%f in (*.html) do (
    set FILE=%%f
    set FILE=!FILE:%ROOT_PATH%=!
    set FILE=!FILE:\=/!
    echo   ^<url^> >> %OUTPUT_FILE%
    echo     ^<loc^>%SITE_URL%!FILE!^</loc^> >> %OUTPUT_FILE%
    echo   ^</url^> >> %OUTPUT_FILE%
)

(
  echo ^</urlset^>
) >> %OUTPUT_FILE%

echo Done. Sitemap saved to %OUTPUT_FILE%
pause
