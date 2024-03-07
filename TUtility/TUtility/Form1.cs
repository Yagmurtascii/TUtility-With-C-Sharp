using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class TUtility : Form
    {

        ComponentSettings settings = new ComponentSettings();


        public TUtility()
        {
            InitializeComponent();
            settings.FillComboBox(databaseComboBox, null);
            settings.LoadCategoriesToComboBox(dataGridView1);

        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                ((ComboBox)e.Control).AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            }
        }



        private void save_click(object sender, EventArgs e)
        {
            settings.Save(dataGridView1, databaseComboBox, schemaComboBox, procedureTextBox);
        }

        private void clear_click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            databaseComboBox.ResetText();
            schemaComboBox.Items.Clear();
            schemaComboBox.ResetText();
            procedureTextBox.Text = "";
            settings.count = 0;
        }

        private void databaseComboBox_Leave(object sender, EventArgs e)
        {
            if (databaseComboBox.SelectedItem == null)
                MessageBox.Show("Lütfen database seçiniz.");
            else
            {
                settings.FillComboBox(schemaComboBox, databaseComboBox.SelectedItem.ToString());

            }

        }

        private void databaseComboBox_TextChanged(object sender, EventArgs e)
        {
            if (databaseComboBox.SelectedItem != null)
            {
                schemaComboBox.Items.Clear();
                schemaComboBox.ResetText();

            }
        }
    }
}
