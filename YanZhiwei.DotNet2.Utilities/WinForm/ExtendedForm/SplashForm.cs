namespace YanZhiwei.DotNet2.Utilities.WinForm.ExtendedForm
{
    using System.Threading;
    using System.Windows.Forms;

    using YanZhiwei.DotNet2.Utilities.Models;
    /// <summary>
    /// 模态提示框
    /// </summary>
    public partial class SplashForm : Form
    {
        #region Fields

        private static SplashForm splashForm;

        #endregion Fields

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="SplashForm"/> class.
        /// </summary>
        public SplashForm()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Delegates

        private delegate void ProcessClose();

        private delegate void ProcessRefresh(string message);

        private delegate void ProcessRefreshFull(string caption, string message);

        #endregion Delegates

        #region Methods        
        /// <summary>
        /// Closes the form.
        /// </summary>
        public static void CloseForm()
        {
            if (splashForm != null)
            {
                if (splashForm.InvokeRequired)
                {
                    splashForm.Invoke(new ProcessClose(DoCloseFormWork));
                    return;
                }
                DoCloseFormWork();
            }
        }
        /// <summary>
        /// Does the refresh work.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="message">The message.</param>
        public static void DoRefreshWork(string caption, string message)
        {
            if (splashForm != null && !splashForm.IsDisposed)
            {
                if (!string.IsNullOrEmpty(caption))
                    splashForm.lblCaption.Text = caption;
                if (!string.IsNullOrEmpty(message))
                    splashForm.lblMessage.Text = message;
                splashForm.Refresh();
            }
        }
        /// <summary>
        /// Does the refresh work.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void DoRefreshWork(string message)
        {
            if (splashForm != null && !splashForm.IsDisposed)
            {
                if (!string.IsNullOrEmpty(message))
                    splashForm.lblMessage.Text = message;
                splashForm.Refresh();
            }
        }
        /// <summary>
        /// Refreshes the specified caption.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="message">The message.</param>
        public static void Refresh(string caption, string message)
        {
            if (splashForm != null)
            {
                if (splashForm.InvokeRequired)
                {
                    splashForm.Invoke(new ProcessRefreshFull(DoRefreshWork), caption, message);
                    return;
                }
                DoRefreshWork(caption, message);
            }
        }
        /// <summary>
        /// Refreshes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Refresh(string message)
        {
            if (splashForm != null)
            {
                if (splashForm.InvokeRequired)
                {
                    splashForm.Invoke(new ProcessRefresh(DoRefreshWork), message);
                    return;
                }
                DoRefreshWork(message);
            }
        }
        /// <summary>
        /// Shows the splash screen.
        /// </summary>
        public static void ShowSplashScreen()
        {
            Thread _thread = new Thread(new ThreadStart(SplashForm.ShowForm));
            _thread.IsBackground = true;
            _thread.SetApartmentState(ApartmentState.STA);
            _thread.Start();
        }
        /// <summary>
        /// Shows the splash screen.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="message">The message.</param>
        public static void ShowSplashScreen(string caption, string message)
        {
            Thread _thread = new Thread(new ParameterizedThreadStart(SplashForm.ShowForm));
            _thread.IsBackground = true;
            _thread.SetApartmentState(ApartmentState.STA);
            _thread.Start(new TipMessage() { Caption = caption, Message = message });
        }
        /// <summary>
        /// Does the close form work.
        /// </summary>
        private static void DoCloseFormWork()
        {
            if (splashForm != null && !splashForm.IsDisposed)
            {
                splashForm.Dispose();
                return;
            }
        }
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
        }
        /// <summary>
        /// Shows the form.
        /// </summary>
        private static void ShowForm()
        {
            ShowForm(null);
        }
        /// <summary>
        /// Shows the form.
        /// </summary>
        /// <param name="tipMessage">The tip message.</param>
        private static void ShowForm(object tipMessage)
        {
            if (splashForm == null || splashForm.IsDisposed)
            {
                splashForm = new SplashForm();
                splashForm.TopMost = true;
                if (tipMessage != null)
                {
                    TipMessage _tipMessage = (TipMessage)tipMessage;
                    splashForm.lblCaption.Text = _tipMessage.Caption.Trim();
                    splashForm.lblMessage.Text = _tipMessage.Message.Trim();
                    splashForm.Refresh();
                }
                splashForm.StartPosition = FormStartPosition.CenterScreen;
                Application.Run(splashForm);
            }
        }

        #endregion Methods
    }
}