namespace EpisconApi
{
    public partial class DataBaseConfig : Form
    {
        private string _dbConnString;
        private string _dbUserName;
        private string _dbPassword; 
        public DataBaseConfig()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.GetTextBoxData();
        }

        private void GetTextBoxData()
        {
            if(String.IsNullOrEmpty(this.tbDatabaseConnString.Text) || String.IsNullOrEmpty(this.tbDatabaseUsername.Text) || String.IsNullOrEmpty(this.tbDatabasePass.Text))
            {
                throw new ApplicationException("None of the values can be empty");
            }
            throw new NotImplementedException();
        }
    }
}