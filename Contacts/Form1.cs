using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contacts
{
    public partial class Form1 : Form
    {
        public SqlConnection constr = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ContactsDB.mdf;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void peopleBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.peopleBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.contactsDBDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "contactsDBDataSet.People". При необходимости она может быть перемещена или удалена.
            this.peopleTableAdapter.Fill(this.contactsDBDataSet.People);

        }

        private void Search_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbClientID.Text != "")
                {
                    SqlCommand com = new SqlCommand(@"SELECT * FROM People WHERE ContactID=" + tbClientID.Text, constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
                else
                { throw new Exception(); }
            }
            catch(Exception)
            {
                MessageBox.Show("Неверный ввод! Повторите попытку.","Error", MessageBoxButtons.OK);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                gbEdit.Enabled = true;
            }
            else
                gbEdit.Enabled = false;
        }

    }
}
