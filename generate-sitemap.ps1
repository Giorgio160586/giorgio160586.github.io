
# Ottieni tutti i file .html nella directory corrente e sottodirectory
$htmlFiles = Get-ChildItem -Path . -Recurse -Include *.html

# Inizio contenuto sitemap XML
$sitemapContent = @"
<?xml version="1.0" encoding="UTF-8"?>
<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">

"@

# Aggiungi ogni file HTML come entry nel sitemap
foreach ($file in $htmlFiles) {
    $url = "https://giorgio160586.github.io/" + $file.FullName.Substring($PWD.Path.Length + 1).Replace("\", "/")
    $lastmod = (Get-Item $file.FullName).LastWriteTime.ToString("yyyy-MM-dd")
    $sitemapContent += "  <url>
"
    $sitemapContent += "    <loc>$url</loc>
"
    $sitemapContent += "    <lastmod>$lastmod</lastmod>
"
    $sitemapContent += "  </url>
"
}

# Chiudi il tag urlset
$sitemapContent += "</urlset>"

# Scrivi il contenuto nel file sitemap.xml con codifica UTF-8 senza BOM
[System.IO.File]::WriteAllText("sitemap.xml", $sitemapContent, (New-Object System.Text.UTF8Encoding($false)))

Write-Output "Il file sitemap.xml è stato generato correttamente."
