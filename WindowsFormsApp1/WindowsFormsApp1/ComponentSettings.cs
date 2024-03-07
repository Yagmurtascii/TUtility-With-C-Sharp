using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public class ComponentSettings
    {
        ConfigurationSettings settings = new ConfigurationSettings();
        public int count = 0;

        public ComponentSettings()
        {
            settings.CreateDatabase();
        }


        public void Save(DataGridView dataGridView, ComboBox database, ComboBox schema, TextBox procedure)
        {
            using (ISession session = settings.SessionFactory().OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                Console.WriteLine(dataGridView.Rows.Count);
                try
                {

                    for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
                    {
                        ProcedureDefinition procedureDefinition = new ProcedureDefinition();
                        if (database.SelectedItem == null || schema.SelectedItem == null || procedure.Text == null ||
                            dataGridView.Rows[i].Cells["ParamName"].Value == null ||
                            dataGridView.Rows[i].Cells["ParamType"].Value == null ||
                            dataGridView.Rows[i].Cells["order"].Value == null ||
                            dataGridView.Rows[i].Cells["Status"].Value == null ||
                            dataGridView.Rows[i].Cells["InOutOrder"].Value == null)
                        {
                            MessageBox.Show("Please enter values in the fields");
                            return;
                        }
                        else
                        {
                            procedureDefinition.DatabaseName = database.SelectedItem.ToString();
                            procedureDefinition.SchemaName = schema.SelectedItem.ToString();
                            procedureDefinition.ProcedureName = procedure.Text;
                            procedureDefinition.ParamName = dataGridView.Rows[i].Cells["ParamName"].Value.ToString();
                            procedureDefinition.ParamType = dataGridView.Rows[i].Cells["ParamType"].Value.ToString();
                            procedureDefinition.OrderValue = int.Parse(dataGridView.Rows[i].Cells["order"].Value.ToString());
                            procedureDefinition.Status = int.Parse(dataGridView.Rows[i].Cells["Status"].Value.ToString());
                            procedureDefinition.InOutOrder = int.Parse(dataGridView.Rows[i].Cells["InOutOrder"].Value.ToString());
                            Console.WriteLine(procedureDefinition.ProcedureName);
                            session.Save(procedureDefinition);
                            count++;
                        }
                    }
                    if (count > 0)
                    {
                        transaction.Commit();
                        MessageBox.Show("Record added to database");


                    }
                    else
                        MessageBox.Show("Please enter values in the fields");

                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    MessageBox.Show("The record could not be added to the database. Please check the values entered in the fields.");
                }

            }

        }

        public string[] GetAllResult(string name, string param)
        {
            using (ISession session = settings.SessionFactory().OpenSession())
            {
                List<string> list = new List<string>();
                if (param == null)
                {
                    if (name == "Database")
                    {
                        list = session.Query<ProcedureDefinition>()
                                               .Select(x => x.DatabaseName)
                                               .Distinct()
                                               .ToList();
                    }

                    if (name == "Paramtype")
                    {
                        list = session.Query<ProcedureDefinition>()
                                               .Select(x => x.ParamType)
                                               .Distinct()
                                               .ToList();
                    }
                }
                else
                {
                    if (name == "Schema")
                    {
                        list = session.Query<ProcedureDefinition>()
                        .Where(x => x.DatabaseName == param)
                       .Select(x => x.SchemaName)
                       .Distinct()
                       .ToList();
                    }
                }



                return list.ToArray();
            }
        }

        public void FillComboBox(ComboBox combo, string param)
        {
            if (param == null)
            {
                if (combo.Name == "databaseComboBox")
                    combo.Items.AddRange(GetAllResult("Database", null));
            }
            else
            {
                if (combo.Name == "schemaComboBox")
                    combo.Items.AddRange(GetAllResult("Schema", param));
            }

        }

        public void LoadCategoriesToComboBox(DataGridView dataGridView)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                var comboBoxCell = row.Cells["ParamType"] as DataGridViewComboBoxCell;
                comboBoxCell.Items.AddRange(GetAllResult("Paramtype", null));

            }

            dataGridView.RowsAdded += (sender, e) =>
            {
                DataGridViewRow newRow = dataGridView.Rows[e.RowIndex];
                var comboBoxCell = newRow.Cells["ParamType"] as DataGridViewComboBoxCell;
                comboBoxCell.Items.AddRange(GetAllResult("Paramtype", null));
            };


        }
    }
}
