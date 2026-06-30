using System.Drawing;
using System.Drawing.Imaging;

namespace ImageBlur;

sealed class ImageSession : IDisposable
{
    readonly string[] _files;
    readonly Stack<Bitmap> _undo = new();

    public int Index    { get; private set; }
    public Bitmap Image { get; private set; }

    public bool CanPrev  => Index > 0;
    public bool CanNext  => Index < _files.Length - 1;
    public bool CanUndo  => _undo.Count > 0;
    public int  Total    => _files.Length;
    public string FileName => Path.GetFileName(_files[Index]);
    public string Info     => $"{Index + 1} / {Total}  –  {FileName}";

    public ImageSession(string[] files)
    {
        _files = files;
        Image  = Load(_files[0]);
    }

    public void GoTo(int index)
    {
        Image.Dispose();
        _undo.Clear();
        Index = index;
        Image = Load(_files[Index]);
    }

    static Bitmap Load(string path)
    {
        using var tmp = new Bitmap(path);
        return new Bitmap(tmp);
    }

    public void Prev() => GoTo(Index - 1);
    public void Next() => GoTo(Index + 1);

    public void Apply(IImageEffect effect, Rectangle region, int strength)
    {
        _undo.Push(new Bitmap(Image));
        effect.Apply(Image, region, strength);
    }

    public bool TryUndo()
    {
        if (!_undo.TryPop(out var prev)) return false;
        Image.Dispose();
        Image = prev;
        return true;
    }

    public void Save()
    {
        var path = _files[Index];
        var fmt  = path.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ? ImageFormat.Png : ImageFormat.Jpeg;
        Image.Save(path, fmt);
    }

    public static ImageSession? TryLoad(string folder, out string error)
    {
        error = string.Empty;
        if (!Directory.Exists(folder)) { error = "Folder not found."; return null; }
        var files = Directory.EnumerateFiles(folder)
            .Where(f => f.EndsWith(".jpg",  StringComparison.OrdinalIgnoreCase)
                     || f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)
                     || f.EndsWith(".png",  StringComparison.OrdinalIgnoreCase)
                     || f.EndsWith(".bmp",  StringComparison.OrdinalIgnoreCase))
            .Order()
            .ToArray();
        if (files.Length == 0) { error = "No images found."; return null; }
        return new ImageSession(files);
    }

    public void Dispose()
    {
        Image.Dispose();
        while (_undo.TryPop(out var bmp))
            bmp.Dispose();
    }
}
