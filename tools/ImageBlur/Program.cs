namespace ImageBlur;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm(new GaussianBlurEffect()));
    }
}
