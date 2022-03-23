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
            try
            {
                this.GetTextBoxData();
                MessageBox.Show($"Connection string : {_dbConnString}, UserName : {_dbUserName}, Password : {_dbPassword}");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void GetTextBoxData()
        {
            if(String.IsNullOrEmpty(this.tbDatabaseConnString.Text) || String.IsNullOrEmpty(this.tbDatabaseUsername.Text) || String.IsNullOrEmpty(this.tbDatabasePass.Text))
            {
                throw new Exception(Constants.ErrorMessages.MultipleEmptyValues);
            }
            _dbConnString = this.tbDatabaseConnString.Text;
            _dbUserName = this.tbDatabaseUsername.Text;
            _dbPassword = this.tbDatabasePass.Text;
        }
    }
}