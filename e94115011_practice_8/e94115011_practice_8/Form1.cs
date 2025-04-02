using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e94115011_practice_8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'e94115011_dbDataSet.Items' table. You can move, or remove it, as needed.
            this.itemsTableAdapter.Fill(this.e94115011_dbDataSet.Items);
            // TODO: This line of code loads data into the 'e94115011_dbDataSet.Customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter.Fill(this.e94115011_dbDataSet.Customers);
            // TODO: This line of code loads data into the 'e94115011_dbDataSet.TransactionHistory' table. You can move, or remove it, as needed.
            this.transactionHistoryTableAdapter.Fill(this.e94115011_dbDataSet.TransactionHistory);
            // TODO: This line of code loads data into the 'e94115011_dbDataSet.Customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter.Fill(this.e94115011_dbDataSet.Customers);
            // TODO: This line of code loads data into the 'e94115011_dbDataSet.Customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter.Fill(this.e94115011_dbDataSet.Customers);
            // TODO: This line of code loads data into the 'e94115011_dbDataSet.Items' table. You can move, or remove it, as needed.
            this.itemsTableAdapter.Fill(this.e94115011_dbDataSet.Items);
            // TODO: This line of code loads data into the 'e94115011_dbDataSet.Customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter.Fill(this.e94115011_dbDataSet.Customers);
            // TODO: This line of code loads data into the 'e94115011_dbDataSet.Items' table. You can move, or remove it, as needed.
            this.itemsTableAdapter.Fill(this.e94115011_dbDataSet.Items);
            // TODO: This line of code loads data into the 'e94115011_dbDataSet.Customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter.Fill(this.e94115011_dbDataSet.Customers);
            // TODO: This line of code loads data into the 'e94115011_dbDataSet.Items' table. You can move, or remove it, as needed.
            this.itemsTableAdapter.Fill(this.e94115011_dbDataSet.Items);
            // TODO: This line of code loads data into the 'e94115011_dbDataSet.Customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter.Fill(this.e94115011_dbDataSet.Customers);
            // TODO: This line of code loads data into the 'e94115011_dbDataSet.TransactionHistory' table. You can move, or remove it, as needed.
            this.transactionHistoryTableAdapter.Fill(this.e94115011_dbDataSet.TransactionHistory);
            // TODO: This line of code loads data into the 'e94115011_dbDataSet.TransactionHistory' table. You can move, or remove it, as needed.
            this.transactionHistoryTableAdapter.Fill(this.e94115011_dbDataSet.TransactionHistory);

        }
    }
}
