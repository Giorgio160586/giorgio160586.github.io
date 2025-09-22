param(
    [string]$SiteUrl = "https://giorgio160586.github.io",
    [string]$Output = "sitemap.xml",
    [string]$Root = (Get-Location).Path
)

$excludePrefix = "google"
$entries = @()

Get-ChildItem -Path $Root -Recurse -Filter "*.html" | Sort-Object FullName | ForEach-Object {
    $p = $_
    if ($p.Name.ToLower().StartsWith($excludePrefix)) { return }
    $rel = $p.FullName.Substring($Root.Length).TrimStart('\').Replace('\','/')
    $lastmod = $p.LastWriteTime.ToString("yyyy-MM-ddTHH:mm:sszzz")
    if ($p.Name -ieq "index.html") {
        $parent = Split-Path $rel
        if ([string]::IsNullOrEmpty($parent)) {
            $entries += @{loc = "$SiteUrl/"; lastmod=$lastmod; priority="0.6"}
            $entries += @{loc = "$SiteUrl/index.html"; lastmod=$lastmod; priority="1.0"}
        } else {
            $entries += @{loc = "$SiteUrl/$parent/"; lastmod=$lastmod; priority="1.0"}
            $entries += @{loc = "$SiteUrl/$parent/index.html"; lastmod=$lastmod; priority="1.0"}
        }
    } else {
        $entries += @{loc = "$SiteUrl/$rel"; lastmod=$lastmod; priority="1.0"}
    }
}

$sb = New-Object System.Text.StringBuilder
$sb.AppendLine('<?xml version="1.0" encoding="UTF-8"?>') | Out-Null
$sb.AppendLine("		<!--	created with www.mysitemapgenerator.com	-->") | Out-Null
$sb.AppendLine('		<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">') | Out-Null
foreach ($e in $entries) {
    $sb.AppendLine('<url>') | Out-Null
    $sb.AppendLine("	<loc>$($e.loc)</loc>") | Out-Null
    $sb.AppendLine("	<lastmod>$($e.lastmod)</lastmod>") | Out-Null
    $sb.AppendLine("	<priority>$($e.priority)</priority>") | Out-Null
    $sb.AppendLine('</url>') | Out-Null
}
$sb.AppendLine('</urlset>') | Out-Null
[System.IO.File]::WriteAllText((Join-Path $Root $Output), $sb.ToString(), [System.Text.Encoding]::UTF8)
Write-Host "Written $Output"
