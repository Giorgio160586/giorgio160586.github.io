# Ottieni tutti i file .html nella directory corrente e sottodirectory
$htmlFiles = Get-ChildItem -Path . -Recurse -Include *.html

# Inizializza contenuto sitemap TXT
$sitemapContent = ""

# Aggiungi ogni file HTML come URL nella sitemap
foreach ($file in $htmlFiles) {
    $url = "https://giorgio160586.github.io/" + $file.FullName.Substring($PWD.Path.Length + 1).Replace("\", "/")
    $sitemapContent += "$url`r`n"
}

# Scrivi il contenuto nel file sitemap.txt con codifica UTF-8 senza BOM
[System.IO.File]::WriteAllText("sitemap.txt", $sitemapContent, (New-Object System.Text.UTF8Encoding($false)))

Write-Output "Il file sitemap.txt è stato generato correttamente."
