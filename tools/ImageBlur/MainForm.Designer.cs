namespace ImageBlur;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        pnlTop = new Panel();
        txtFolder = new TextBox();
        btnBrowse = new Button();
        btnOpen = new Button();
        pnlBottom = new Panel();
        btnPrev = new Button();
        btnNext = new Button();
        lblInfo = new Label();
        lblBlur = new Label();
        trackBlur = new TrackBar();
        lblBlurVal = new Label();
        btnUndo = new Button();
        btnSave = new Button();
        picBox = new PictureBox();
        toolTip = new ToolTip(components);
        pnlTop.SuspendLayout();
        pnlBottom.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)trackBlur).BeginInit();
        ((System.ComponentModel.ISupportInitialize)picBox).BeginInit();
        SuspendLayout();
        // 
        // pnlTop
        // 
        pnlTop.Controls.Add(txtFolder);
        pnlTop.Controls.Add(btnBrowse);
        pnlTop.Controls.Add(btnOpen);
        pnlTop.Dock = DockStyle.Top;
        pnlTop.Location = new Point(0, 0);
        pnlTop.Name = "pnlTop";
        pnlTop.Size = new Size(601, 42);
        pnlTop.TabIndex = 2;
        // 
        // txtFolder
        // 
        txtFolder.Location = new Point(5, 10);
        txtFolder.Name = "txtFolder";
        txtFolder.Size = new Size(440, 23);
        txtFolder.TabIndex = 0;
        // 
        // btnBrowse
        // 
        btnBrowse.Location = new Point(450, 8);
        btnBrowse.Name = "btnBrowse";
        btnBrowse.Size = new Size(30, 26);
        btnBrowse.TabIndex = 1;
        btnBrowse.Text = "...";
        toolTip.SetToolTip(btnBrowse, "Browse folder");
        btnBrowse.Click += btnBrowse_Click;
        // 
        // btnOpen
        // 
        btnOpen.Location = new Point(485, 8);
        btnOpen.Name = "btnOpen";
        btnOpen.Size = new Size(55, 26);
        btnOpen.TabIndex = 2;
        btnOpen.Text = "Open";
        btnOpen.Click += btnOpen_Click;
        // 
        // pnlBottom
        // 
        pnlBottom.Controls.Add(btnPrev);
        pnlBottom.Controls.Add(btnNext);
        pnlBottom.Controls.Add(lblInfo);
        pnlBottom.Controls.Add(lblBlur);
        pnlBottom.Controls.Add(trackBlur);
        pnlBottom.Controls.Add(lblBlurVal);
        pnlBottom.Controls.Add(btnUndo);
        pnlBottom.Controls.Add(btnSave);
        pnlBottom.Dock = DockStyle.Bottom;
        pnlBottom.Location = new Point(0, 348);
        pnlBottom.Name = "pnlBottom";
        pnlBottom.Size = new Size(601, 50);
        pnlBottom.TabIndex = 1;
        // 
        // btnPrev
        // 
        btnPrev.Enabled = false;
        btnPrev.Location = new Point(5, 10);
        btnPrev.Name = "btnPrev";
        btnPrev.Size = new Size(55, 26);
        btnPrev.TabIndex = 0;
        btnPrev.Text = "Prev";
        toolTip.SetToolTip(btnPrev, "Previous image");
        btnPrev.Click += btnPrev_Click;
        // 
        // btnNext
        // 
        btnNext.Enabled = false;
        btnNext.Location = new Point(64, 10);
        btnNext.Name = "btnNext";
        btnNext.Size = new Size(55, 26);
        btnNext.TabIndex = 1;
        btnNext.Text = "Next";
        toolTip.SetToolTip(btnNext, "Next image");
        btnNext.Click += btnNext_Click;
        // 
        // lblInfo
        // 
        lblInfo.Location = new Point(124, 14);
        lblInfo.Name = "lblInfo";
        lblInfo.Size = new Size(160, 18);
        lblInfo.TabIndex = 2;
        lblInfo.Text = "No folder open";
        // 
        // lblBlur
        // 
        lblBlur.Location = new Point(289, 14);
        lblBlur.Name = "lblBlur";
        lblBlur.Size = new Size(44, 18);
        lblBlur.TabIndex = 3;
        lblBlur.Text = "Radius:";
        // 
        // trackBlur
        // 
        trackBlur.LargeChange = 2;
        trackBlur.Location = new Point(333, 4);
        trackBlur.Maximum = 20;
        trackBlur.Minimum = 1;
        trackBlur.Name = "trackBlur";
        trackBlur.Size = new Size(110, 45);
        trackBlur.TabIndex = 4;
        trackBlur.TickFrequency = 2;
        trackBlur.Value = 2;
        trackBlur.ValueChanged += trackBlur_ValueChanged;
        // 
        // lblBlurVal
        // 
        lblBlurVal.Location = new Point(447, 14);
        lblBlurVal.Name = "lblBlurVal";
        lblBlurVal.Size = new Size(24, 18);
        lblBlurVal.TabIndex = 5;
        lblBlurVal.Text = "2";
        // 
        // btnUndo
        // 
        btnUndo.Enabled = false;
        btnUndo.Location = new Point(474, 10);
        btnUndo.Name = "btnUndo";
        btnUndo.Size = new Size(55, 26);
        btnUndo.TabIndex = 6;
        btnUndo.Text = "Undo";
        toolTip.SetToolTip(btnUndo, "Undo last blur");
        btnUndo.Click += btnUndo_Click;
        // 
        // btnSave
        // 
        btnSave.Enabled = false;
        btnSave.Location = new Point(533, 10);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(55, 26);
        btnSave.TabIndex = 7;
        btnSave.Text = "Save";
        toolTip.SetToolTip(btnSave, "Save image");
        btnSave.Click += btnSave_Click;
        // 
        // picBox
        // 
        picBox.BackColor = Color.FromArgb(45, 45, 48);
        picBox.Cursor = Cursors.Cross;
        picBox.Dock = DockStyle.Fill;
        picBox.Location = new Point(0, 42);
        picBox.Name = "picBox";
        picBox.Size = new Size(601, 306);
        picBox.SizeMode = PictureBoxSizeMode.Zoom;
        picBox.TabIndex = 0;
        picBox.TabStop = false;
        picBox.Paint += picBox_Paint;
        picBox.MouseDown += picBox_MouseDown;
        picBox.MouseMove += picBox_MouseMove;
        picBox.MouseUp += picBox_MouseUp;
        // 
        // MainForm
        // 
        ClientSize = new Size(601, 398);
        Controls.Add(picBox);
        Controls.Add(pnlBottom);
        Controls.Add(pnlTop);
        KeyPreview  = true;
        MinimumSize = new Size(400, 300);
        Name        = "MainForm";
        Text        = "Image Blur Tool";
        KeyDown    += MainForm_KeyDown;
        pnlTop.ResumeLayout(false);
        pnlTop.PerformLayout();
        pnlBottom.ResumeLayout(false);
        pnlBottom.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)trackBlur).EndInit();
        ((System.ComponentModel.ISupportInitialize)picBox).EndInit();
        ResumeLayout(false);
    }

    private Panel      pnlTop;
    private TextBox    txtFolder;
    private Button     btnBrowse;
    private Button     btnOpen;
    private Panel      pnlBottom;
    private Button     btnPrev;
    private Button     btnNext;
    private Label      lblInfo;
    private Label      lblBlur;
    private TrackBar   trackBlur;
    private Label      lblBlurVal;
    private Button     btnUndo;
    private Button     btnSave;
    private PictureBox picBox;
    private ToolTip    toolTip;
}
