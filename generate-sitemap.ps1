# Ottieni tutti i file .html nella directory corrente e sottodirectory
$htmlFiles = Get-ChildItem -Path . -Recurse -Include *.html

# Inizio contenuto sitemap XML con namespace xhtml
$sitemapContent = @"
<?xml version="1.0" encoding="UTF-8"?>
<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9"
        xmlns:xhtml="http://www.w3.org/1999/xhtml">

"@

# Raggruppa i file per nome relativo senza prefisso lingua (es. resume.html)
$groupedFiles = $htmlFiles | ForEach-Object {
    $relativePath = $_.FullName.Substring($PWD.Path.Length + 1).Replace("\", "/")
    $parts = $relativePath.Split("/")
    if ($parts.Count -ge 2 -and ($parts[0] -eq "en" -or $parts[0] -eq "it")) {
        [PSCustomObject]@{
            Lang = $parts[0]
            File = $_
            Relative = $relativePath
            Key = ($parts[1..($parts.Count-1)] -join "/") # es. resume.html
        }
    }
} | Group-Object Key

foreach ($group in $groupedFiles) {
    foreach ($entry in $group.Group) {
        $url = "https://giorgio160586.github.io/" + $entry.Relative
        $lastmod = (Get-Item $entry.File.FullName).LastWriteTime.ToString("yyyy-MM-dd")

        $sitemapContent += "  <url>`n"
        $sitemapContent += "    <loc>$url</loc>`n"
        $sitemapContent += "    <lastmod>$lastmod</lastmod>`n"

        # Aggiungi link alternati per tutte le lingue disponibili dello stesso file
        foreach ($alt in $group.Group) {
            $altUrl = "https://giorgio160586.github.io/" + $alt.Relative
            $sitemapContent += "    <xhtml:link rel=`"alternate`" hreflang=`"$($alt.Lang)`" href=`"$altUrl`" />`n"
        }

        $sitemapContent += "  </url>`n"
    }
}

# Chiudi il tag urlset
$sitemapContent += "</urlset>"

# Scrivi il contenuto nel file sitemap.xml con codifica UTF-8 senza BOM
[System.IO.File]::WriteAllText("sitemap.xml", $sitemapContent, (New-Object System.Text.UTF8Encoding($false)))

Write-Output "Il file sitemap.xml è stato generato correttamente con hreflang."
