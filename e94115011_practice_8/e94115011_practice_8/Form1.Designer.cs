namespace e94115011_practice_8
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.transactionHistoryDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.e94115011_dbDataSet = new e94115011_practice_8.e94115011_dbDataSet();
            this.transactionHistoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.transactionHistoryTableAdapter = new e94115011_practice_8.e94115011_dbDataSetTableAdapters.TransactionHistoryTableAdapter();
            this.tIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customerIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transactionHistoryCustomersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.customersTableAdapter = new e94115011_practice_8.e94115011_dbDataSetTableAdapters.CustomersTableAdapter();
            this.cIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transactionHistoryItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.itemsTableAdapter = new e94115011_practice_8.e94115011_dbDataSetTableAdapters.ItemsTableAdapter();
            this.itemIDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.transactionHistoryDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e94115011_dbDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transactionHistoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transactionHistoryCustomersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transactionHistoryItemsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // transactionHistoryDataGridView
            // 
            this.transactionHistoryDataGridView.AutoGenerateColumns = false;
            this.transactionHistoryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.transactionHistoryDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tIDDataGridViewTextBoxColumn,
            this.customerIDDataGridViewTextBoxColumn,
            this.itemIDDataGridViewTextBoxColumn,
            this.countDataGridViewTextBoxColumn});
            this.transactionHistoryDataGridView.DataSource = this.transactionHistoryBindingSource;
            this.transactionHistoryDataGridView.Location = new System.Drawing.Point(79, 34);
            this.transactionHistoryDataGridView.Name = "transactionHistoryDataGridView";
            this.transactionHistoryDataGridView.RowHeadersWidth = 62;
            this.transactionHistoryDataGridView.RowTemplate.Height = 28;
            this.transactionHistoryDataGridView.Size = new System.Drawing.Size(1068, 305);
            this.transactionHistoryDataGridView.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cIDDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.transactionHistoryCustomersBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(79, 442);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(629, 150);
            this.dataGridView1.TabIndex = 1;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.itemIDDataGridViewTextBoxColumn1,
            this.nameDataGridViewTextBoxColumn1,
            this.priceDataGridViewTextBoxColumn});
            this.dataGridView2.DataSource = this.transactionHistoryItemsBindingSource;
            this.dataGridView2.Location = new System.Drawing.Point(763, 442);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 62;
            this.dataGridView2.RowTemplate.Height = 28;
            this.dataGridView2.Size = new System.Drawing.Size(778, 150);
            this.dataGridView2.TabIndex = 2;
            // 
            // e94115011_dbDataSet
            // 
            this.e94115011_dbDataSet.DataSetName = "e94115011_dbDataSet";
            this.e94115011_dbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // transactionHistoryBindingSource
            // 
            this.transactionHistoryBindingSource.DataMember = "TransactionHistory";
            this.transactionHistoryBindingSource.DataSource = this.e94115011_dbDataSet;
            // 
            // transactionHistoryTableAdapter
            // 
            this.transactionHistoryTableAdapter.ClearBeforeFill = true;
            // 
            // tIDDataGridViewTextBoxColumn
            // 
            this.tIDDataGridViewTextBoxColumn.DataPropertyName = "TID";
            this.tIDDataGridViewTextBoxColumn.HeaderText = "TID";
            this.tIDDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.tIDDataGridViewTextBoxColumn.Name = "tIDDataGridViewTextBoxColumn";
            this.tIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.tIDDataGridViewTextBoxColumn.Width = 150;
            // 
            // customerIDDataGridViewTextBoxColumn
            // 
            this.customerIDDataGridViewTextBoxColumn.DataPropertyName = "CustomerID";
            this.customerIDDataGridViewTextBoxColumn.HeaderText = "CustomerID";
            this.customerIDDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.customerIDDataGridViewTextBoxColumn.Name = "customerIDDataGridViewTextBoxColumn";
            this.customerIDDataGridViewTextBoxColumn.Width = 150;
            // 
            // itemIDDataGridViewTextBoxColumn
            // 
            this.itemIDDataGridViewTextBoxColumn.DataPropertyName = "ItemID";
            this.itemIDDataGridViewTextBoxColumn.HeaderText = "ItemID";
            this.itemIDDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.itemIDDataGridViewTextBoxColumn.Name = "itemIDDataGridViewTextBoxColumn";
            this.itemIDDataGridViewTextBoxColumn.Width = 150;
            // 
            // countDataGridViewTextBoxColumn
            // 
            this.countDataGridViewTextBoxColumn.DataPropertyName = "Count";
            this.countDataGridViewTextBoxColumn.HeaderText = "Count";
            this.countDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.countDataGridViewTextBoxColumn.Name = "countDataGridViewTextBoxColumn";
            this.countDataGridViewTextBoxColumn.Width = 150;
            // 
            // transactionHistoryCustomersBindingSource
            // 
            this.transactionHistoryCustomersBindingSource.DataMember = "TransactionHistory_Customers";
            this.transactionHistoryCustomersBindingSource.DataSource = this.transactionHistoryBindingSource;
            // 
            // customersTableAdapter
            // 
            this.customersTableAdapter.ClearBeforeFill = true;
            // 
            // cIDDataGridViewTextBoxColumn
            // 
            this.cIDDataGridViewTextBoxColumn.DataPropertyName = "CID";
            this.cIDDataGridViewTextBoxColumn.HeaderText = "CID";
            this.cIDDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.cIDDataGridViewTextBoxColumn.Name = "cIDDataGridViewTextBoxColumn";
            this.cIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.cIDDataGridViewTextBoxColumn.Width = 150;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.Width = 150;
            // 
            // transactionHistoryItemsBindingSource
            // 
            this.transactionHistoryItemsBindingSource.DataMember = "TransactionHistory_Items";
            this.transactionHistoryItemsBindingSource.DataSource = this.transactionHistoryBindingSource;
            // 
            // itemsTableAdapter
            // 
            this.itemsTableAdapter.ClearBeforeFill = true;
            // 
            // itemIDDataGridViewTextBoxColumn1
            // 
            this.itemIDDataGridViewTextBoxColumn1.DataPropertyName = "ItemID";
            this.itemIDDataGridViewTextBoxColumn1.HeaderText = "ItemID";
            this.itemIDDataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.itemIDDataGridViewTextBoxColumn1.Name = "itemIDDataGridViewTextBoxColumn1";
            this.itemIDDataGridViewTextBoxColumn1.ReadOnly = true;
            this.itemIDDataGridViewTextBoxColumn1.Width = 150;
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            this.nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn1.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            this.nameDataGridViewTextBoxColumn1.Width = 150;
            // 
            // priceDataGridViewTextBoxColumn
            // 
            this.priceDataGridViewTextBoxColumn.DataPropertyName = "Price";
            this.priceDataGridViewTextBoxColumn.HeaderText = "Price";
            this.priceDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
            this.priceDataGridViewTextBoxColumn.Width = 150;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1575, 691);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.transactionHistoryDataGridView);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.transactionHistoryDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e94115011_dbDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transactionHistoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transactionHistoryCustomersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transactionHistoryItemsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView transactionHistoryDataGridView;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private e94115011_dbDataSet e94115011_dbDataSet;
        private System.Windows.Forms.BindingSource transactionHistoryBindingSource;
        private e94115011_dbDataSetTableAdapters.TransactionHistoryTableAdapter transactionHistoryTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn tIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn countDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource transactionHistoryCustomersBindingSource;
        private e94115011_dbDataSetTableAdapters.CustomersTableAdapter customersTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn cIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource transactionHistoryItemsBindingSource;
        private e94115011_dbDataSetTableAdapters.ItemsTableAdapter itemsTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemIDDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
    }
}