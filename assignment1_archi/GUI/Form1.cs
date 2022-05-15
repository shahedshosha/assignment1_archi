using project.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment1_archi
{
    public partial class Form1 : Form
    {
        private static string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\pc\\source\\repos\\assignment1_archi\\assignment1_archi\\bin\\Debug\\Assignment.accdb";

        private static OleDbConnection conn = new OleDbConnection(connectionString);
        Stores currentStore;

        public Form1()
        {
            InitializeComponent();
        }
        public static void InsertCommand(Stores store)
        {
            string commandText = string.Format("Insert into Stores(StoreID,StoreName,StorePhone)" +
            "values('{0}','{1}','{2}')", store.StoreID, store.StoreName, store.StorePhone);

            OleDbCommand command = new OleDbCommand(commandText, conn);

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void UpdateCommand(Stores store)
        {
            string updateCommand = string.Format("Update Stores Set StoreID = '{1}', StoreName = '{2}',StorePhone = '{3}'" +
                " where StId = {0}", store.StId, store.StoreID, store.StoreName, store.StorePhone);

            OleDbCommand command = new OleDbCommand(updateCommand, conn);

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void DeleteCommand(Stores store)
        {
            string commandText = string.Format("Delete from Stores where StId = {0}"
                , store.StId);

            OleDbCommand command = new OleDbCommand(commandText, conn);

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static DataTable GetAllCommand()
        {
            string commandText = string.Format("Select * from Stores");

            OleDbCommand command = new OleDbCommand(commandText, conn);

            OleDbDataAdapter da = new OleDbDataAdapter(command);

            DataTable dt = new DataTable();

            da.Fill(dt);

            return dt;
        }
        private void RefreshdataGridViewStoreManage()
        {
            dataGridViewStoreManage.DataSource = GetAllCommand();
        }
        private void ClearForm()
        {
            txtStoreName.Text = "";
            txtStoreID.Text = "";
            txtStorePhone.Text = "";
        }

        private void dataGridViewStoreManage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentStore = new Stores();
            DataGridViewRow row = new DataGridViewRow();

            row = dataGridViewStoreManage.Rows[e.RowIndex];
            currentStore.StId = int.Parse(row.Cells[0].Value.ToString());
            currentStore.StoreID = row.Cells[1].Value.ToString();
            currentStore.StoreName = row.Cells[2].Value.ToString();
            currentStore.StorePhone = row.Cells[3].Value.ToString();
            txtStoreID.Text = currentStore.StoreID;
            txtStoreName.Text = currentStore.StoreName;
            txtStorePhone.Text = currentStore.StorePhone;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Stores store = new Stores();
            store.StoreName = txtStoreName.Text;
            store.StoreID = txtStoreID.Text;
            store.StorePhone = txtStorePhone.Text;
            InsertCommand(store);
            RefreshdataGridViewStoreManage();
            MessageBox.Show("Store Successfully Added");
            ClearForm();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            currentStore.StoreName = txtStoreName.Text;
            currentStore.StoreID = txtStoreID.Text;
            currentStore.StorePhone = txtStorePhone.Text;
            UpdateCommand(currentStore);
            RefreshdataGridViewStoreManage();
            MessageBox.Show("Store Successfully Updated");
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteCommand(currentStore);

            RefreshdataGridViewStoreManage();

            MessageBox.Show("Store Successfully Deleted");

            ClearForm();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshdataGridViewStoreManage();
        }
    }
}
