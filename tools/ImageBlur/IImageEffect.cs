using System.Drawing;

namespace ImageBlur;

internal interface IImageEffect
{
    void Apply(Bitmap bitmap, Rectangle region, int strength);
}
