namespace ImageBlur;

public partial class MainForm : Form
{
    readonly IImageEffect _effect;
    ImageSession? _session;
    Point _dragStart;
    Rectangle _sel;
    bool _dragging;

    public MainForm(IImageEffect effect)
    {
        _effect = effect;
        InitializeComponent();
        txtFolder.Text = @"C:\GitHub\giorgio160586.github.io\assets\images\projects\webprice2026";
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
        using var dlg = new FolderBrowserDialog { SelectedPath = txtFolder.Text };
        if (dlg.ShowDialog() == DialogResult.OK) { txtFolder.Text = dlg.SelectedPath; OpenFolder(); }
    }

    private void btnOpen_Click(object sender, EventArgs e) => OpenFolder();

    private void btnPrev_Click(object sender, EventArgs e) { _session!.Prev(); UpdateUI(); }
    private void btnNext_Click(object sender, EventArgs e) { _session!.Next(); UpdateUI(); }

    private void btnUndo_Click(object sender, EventArgs e)
    {
        _session!.TryUndo();
        picBox.Image    = _session.Image;
        btnUndo.Enabled = _session.CanUndo;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        _session!.Save();
        MessageBox.Show($"Saved: {_session.FileName}", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void trackBlur_ValueChanged(object sender, EventArgs e) => lblBlurVal.Text = trackBlur.Value.ToString();

    private void MainForm_KeyDown(object sender, KeyEventArgs e)
    {
        switch (e.KeyData)
        {
            case Keys.Control | Keys.Z: if (_session?.CanUndo == true) btnUndo_Click(sender, e);  break;
            case Keys.Control | Keys.S: if (_session != null)           btnSave_Click(sender, e);  break;
            case Keys.Control | Keys.O: btnBrowse_Click(sender, e);                                break;
            case Keys.Left:             if (_session?.CanPrev == true)  btnPrev_Click(sender, e);  break;
            case Keys.Right:            if (_session?.CanNext == true)  btnNext_Click(sender, e);  break;
            case Keys.Home:             if (_session != null) { _session.GoTo(0);                      UpdateUI(); } break;
            case Keys.End:              if (_session != null) { _session.GoTo(_session.Total - 1);     UpdateUI(); } break;
            case Keys.Oemplus:
            case Keys.Add:              trackBlur.Value = Math.Min(trackBlur.Maximum, trackBlur.Value + 1); break;
            case Keys.OemMinus:
            case Keys.Subtract:         trackBlur.Value = Math.Max(trackBlur.Minimum, trackBlur.Value - 1); break;
            case Keys.Escape:           _sel = default; picBox.Invalidate(); break;
        }
    }

    private void picBox_MouseDown(object sender, MouseEventArgs e)
    {
        if (_session == null) return;
        (_dragging, _dragStart, _sel) = (true, e.Location, default);
    }

    private void picBox_MouseMove(object sender, MouseEventArgs e)
    {
        if (!_dragging) return;
        _sel = MakeRect(_dragStart, e.Location);
        picBox.Invalidate();
    }

    private void picBox_MouseUp(object sender, MouseEventArgs e)
    {
        if (!_dragging || _session == null) return;
        _dragging = false;
        var region = ToImageCoords(_sel, _session.Image);
        if (region.Width > 2 && region.Height > 2)
        {
            _session.Apply(_effect, region, trackBlur.Value);
            picBox.Image    = _session.Image;
            btnUndo.Enabled = true;
            picBox.Refresh();
        }
        _sel = default;
        picBox.Invalidate();
    }

    private void picBox_Paint(object sender, PaintEventArgs e)
    {
        if (_sel.Width < 2) return;
        using var pen = new Pen(Color.Red, 2) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
        e.Graphics.DrawRectangle(pen, _sel);
    }

    private void OpenFolder()
    {
        var s = ImageSession.TryLoad(txtFolder.Text.Trim(), out var err);
        if (s == null) { MessageBox.Show(err); return; }
        _session?.Dispose();
        _session = s;
        UpdateUI();
    }

    private void UpdateUI()
    {
        picBox.Image    = _session!.Image;
        lblInfo.Text    = _session.Info;
        btnPrev.Enabled = _session.CanPrev;
        btnNext.Enabled = _session.CanNext;
        btnUndo.Enabled = _session.CanUndo;
        btnSave.Enabled = true;
        Text = $"Image Blur Tool – {_session.FileName}";
    }

    private static Rectangle MakeRect(Point a, Point b) =>
        new(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y), Math.Abs(b.X - a.X), Math.Abs(b.Y - a.Y));

    private Rectangle ToImageCoords(Rectangle r, Bitmap img)
    {
        float s  = Math.Min((float)picBox.Width / img.Width, (float)picBox.Height / img.Height);
        float ox = (picBox.Width  - img.Width  * s) / 2f;
        float oy = (picBox.Height - img.Height * s) / 2f;
        int x = Math.Clamp((int)((r.X - ox) / s), 0, img.Width  - 1);
        int y = Math.Clamp((int)((r.Y - oy) / s), 0, img.Height - 1);
        return new(x, y, Math.Clamp((int)(r.Width  / s), 0, img.Width  - x),
                         Math.Clamp((int)(r.Height / s), 0, img.Height - y));
    }
}
