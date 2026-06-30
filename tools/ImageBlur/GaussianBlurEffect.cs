using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ImageBlur;

sealed class GaussianBlurEffect : IImageEffect
{
    public void Apply(Bitmap bmp, Rectangle r, int radius)
    {
        var data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                                ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
        int len = Math.Abs(data.Stride) * bmp.Height;
        var buf = new byte[len];
        var tmp = new byte[len];
        Marshal.Copy(data.Scan0, buf, 0, len);
        for (int p = 0; p < 2; p++)
        {
            BoxH(buf, tmp, data.Stride, r, radius, bmp.Width, bmp.Height);
            BoxV(tmp, buf, data.Stride, r, radius, bmp.Width, bmp.Height);
        }
        Marshal.Copy(buf, 0, data.Scan0, len);
        bmp.UnlockBits(data);
    }

    static void BoxH(byte[] src, byte[] dst, int stride, Rectangle r, int rad, int W, int H)
    {
        src.AsSpan().CopyTo(dst);
        for (int y = r.Top; y < r.Bottom && y < H; y++)
            for (int x = r.Left; x < r.Right && x < W; x++)
            {
                long b = 0, g = 0, rv = 0; int n = 0;
                for (int kx = Math.Max(r.Left, x - rad); kx <= Math.Min(r.Right - 1, x + rad) && kx < W; kx++)
                    { int o = y * stride + kx * 4; b += src[o]; g += src[o+1]; rv += src[o+2]; n++; }
                int od = y * stride + x * 4;
                dst[od] = (byte)(b/n); dst[od+1] = (byte)(g/n); dst[od+2] = (byte)(rv/n);
            }
    }

    static void BoxV(byte[] src, byte[] dst, int stride, Rectangle r, int rad, int W, int H)
    {
        src.AsSpan().CopyTo(dst);
        for (int x = r.Left; x < r.Right && x < W; x++)
            for (int y = r.Top; y < r.Bottom && y < H; y++)
            {
                long b = 0, g = 0, rv = 0; int n = 0;
                for (int ky = Math.Max(r.Top, y - rad); ky <= Math.Min(r.Bottom - 1, y + rad) && ky < H; ky++)
                    { int o = ky * stride + x * 4; b += src[o]; g += src[o+1]; rv += src[o+2]; n++; }
                int od = y * stride + x * 4;
                dst[od] = (byte)(b/n); dst[od+1] = (byte)(g/n); dst[od+2] = (byte)(rv/n);
            }
    }
}
