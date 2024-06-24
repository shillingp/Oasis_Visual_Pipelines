using Oasis_Visual_Pipelines.Operations.Enums;
using System.Windows.Controls;

namespace Oasis_Visual_Pipelines.Dialogs
{
    /// <summary>
    /// Interaction logic for SQLConnectionSettingsDialog.xaml
    /// </summary>
    public partial class SQLConnectionSettingsDialog : UserControl
    {
        public string ServerString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string TableName { get; set; } = string.Empty;
        public Authentication AuthenticationMethod { get; set; } = Authentication.None;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public SQLConnectionSettingsDialog()
        {
            InitializeComponent();
        }
    }
}
