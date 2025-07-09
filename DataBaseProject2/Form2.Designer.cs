namespace DataBaseProject2
{
    partial class Form2
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
            dataGridView1 = new DataGridView();
            Order_ID = new DataGridViewTextBoxColumn();
            Item_Type = new DataGridViewTextBoxColumn();
            Item_Name = new DataGridViewTextBoxColumn();
            Qty = new DataGridViewTextBoxColumn();
            Price_Each = new DataGridViewTextBoxColumn();
            Customer_ID = new DataGridViewTextBoxColumn();
            comboBox1 = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = Color.FromArgb(180, 128, 80);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Order_ID, Item_Type, Item_Name, Qty, Price_Each, Customer_ID });
            dataGridView1.Location = new Point(63, 183);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(645, 159);
            dataGridView1.TabIndex = 16;
            // 
            // Order_ID
            // 
            Order_ID.HeaderText = "Order_ID";
            Order_ID.Name = "Order_ID";
            // 
            // Item_Type
            // 
            Item_Type.HeaderText = "Item_Type";
            Item_Type.Name = "Item_Type";
            // 
            // Item_Name
            // 
            Item_Name.HeaderText = "Item_Name";
            Item_Name.Name = "Item_Name";
            // 
            // Qty
            // 
            Qty.HeaderText = "Qty";
            Qty.Name = "Qty";
            // 
            // Price_Each
            // 
            Price_Each.HeaderText = "Price_Each";
            Price_Each.Name = "Price_Each";
            // 
            // Customer_ID
            // 
            Customer_ID.HeaderText = "Customer_ID";
            Customer_ID.Name = "Customer_ID";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(63, 116);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(68, 23);
            comboBox1.TabIndex = 17;
            comboBox1.SelectionChangeCommitted += comboBox1_SelectionChangeCommitted;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 192, 128);
            ClientSize = new Size(800, 450);
            Controls.Add(comboBox1);
            Controls.Add(dataGridView1);
            Name = "Form2";
            Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DataGridViewTextBoxColumn Order_ID;
        private DataGridViewTextBoxColumn Item_Type;
        private DataGridViewTextBoxColumn Item_Name;
        private DataGridViewTextBoxColumn Qty;
        private DataGridViewTextBoxColumn Price_Each;
        private DataGridViewTextBoxColumn Customer_ID;
        public DataGridView dataGridView1;
        public ComboBox comboBox1;
    }
}