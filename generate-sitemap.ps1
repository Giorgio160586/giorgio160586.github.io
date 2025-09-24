param(
    [string]$SiteUrl = "https://giorgio160586.github.io",
    [string]$Output = "sitemap.xml",
    [string]$Root = (Get-Location).Path
)

$SiteUrl = $SiteUrl.TrimEnd('/')
$entries = @()

function Escape-Xml([string]$str) {
    $str -replace '&','&amp;' -replace '<','&lt;' -replace '>','&gt;' -replace '"','&quot;' -replace "'","&apos;"
}

Get-ChildItem -Path $Root -Recurse -Filter "*.html" | Sort-Object FullName | ForEach-Object {
    $p = $_
    $rel = $p.FullName.Substring($Root.Length).TrimStart('\').Replace('\','/')
    $lastmod = $p.LastWriteTime.ToString("yyyy-MM-ddTHH:mm:sszzz")
    $entries += @{ loc = "$SiteUrl/$($rel | Escape-Xml)"; lastmod = $lastmod }
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

# Convert CRLF to LF
[System.IO.File]::WriteAllText((Join-Path $Root $Output), ($sb.ToString() -replace "`r`n", "`n"), [System.Text.Encoding]::UTF8)

Write-Host "Sitemap generated: $Output"
