(function () {
  /* Blurs a rectangular region of a canvas using a temporary off-screen canvas.
     Padding avoids the soft halo that ctx.filter bleeds outside rect bounds. */
  function blurRegion(ctx, source, x, y, w, h, px) {
    var pad = px * 2;
    var tmp = document.createElement('canvas');
    tmp.width  = w + pad * 2;
    tmp.height = h + pad * 2;
    var tctx = tmp.getContext('2d');
    tctx.filter = 'blur(' + px + 'px)';
    tctx.drawImage(source,
      x - pad, y - pad, w + pad * 2, h + pad * 2,
      0,       0,       w + pad * 2, h + pad * 2);
    ctx.save();
    ctx.beginPath();
    ctx.rect(x, y, w, h);
    ctx.clip();
    ctx.drawImage(tmp, x - pad, y - pad);
    ctx.restore();
  }

  function processContainer(container) {
    var img   = container.querySelector('img');
    var zones = Array.from(container.querySelectorAll('.blur-zone'));
    if (!img || !zones.length) return;

    function render() {
      var W = img.naturalWidth;
      var H = img.naturalHeight;
      if (!W || !H) return;

      var canvas = document.createElement('canvas');
      canvas.width  = W;
      canvas.height = H;
      canvas.style.display      = 'block';
      canvas.style.width        = '100%';
      canvas.style.borderRadius = '.375rem';

      var ctx = canvas.getContext('2d');
      ctx.drawImage(img, 0, 0);

      zones.forEach(function (zone) {
        var x = parseFloat(zone.style.left)   / 100 * W;
        var y = parseFloat(zone.style.top)    / 100 * H;
        var w = parseFloat(zone.style.width)  / 100 * W;
        var h = parseFloat(zone.style.height) / 100 * H;
        blurRegion(ctx, img, x, y, w, h, 14);
      });

      img.parentNode.replaceChild(canvas, img);
      zones.forEach(function (z) { z.remove(); });
    }

    if (img.complete && img.naturalWidth > 0) {
      render();
    } else {
      img.addEventListener('load', render);
    }
  }

  document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.img-blur-container').forEach(processContainer);
  });
})();
