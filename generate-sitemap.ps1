param(
    [string]$SiteUrl = "https://giorgio160586.github.io",
    [string]$Output = "sitemap.xml",
    [string]$Root = (Get-Location).Path
)

$entries = @()

Get-ChildItem -Path $Root -Recurse -Filter "*.html" | Sort-Object FullName | ForEach-Object {
    $p = $_
    $rel = $p.FullName.Substring($Root.Length).TrimStart('\').Replace('\','/')
    $lastmod = $p.LastWriteTime.ToString("yyyy-MM-dd")
    $entries += @{ loc = "$SiteUrl/$rel"; lastmod = $lastmod }
}

$sb = New-Object System.Text.StringBuilder
$sb.AppendLine('<?xml version="1.0" encoding="UTF-8"?>') | Out-Null
$sb.AppendLine('<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">') | Out-Null

foreach ($e in $entries) {
    $sb.AppendLine('  <url>') | Out-Null
    $sb.AppendLine("    <loc>$($e.loc)</loc>") | Out-Null
    $sb.AppendLine("    <lastmod>$($e.lastmod)</lastmod>") | Out-Null
    $sb.AppendLine('  </url>') | Out-Null
}

$sb.AppendLine('</urlset>') | Out-Null

[System.IO.File]::WriteAllText((Join-Path $Root $Output), $sb.ToString(), [System.Text.Encoding]::UTF8)

Write-Host "Sitemap generated: $Output"
