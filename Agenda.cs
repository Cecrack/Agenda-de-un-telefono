using DevExpress.Utils.CommonDialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Agenda
{

    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnContactos_Click(object sender, EventArgs e)
        {
            StreamWriter agenda;
            agenda = new StreamWriter("Contactos.txt", true);
            agenda.WriteLine(String.Format(txtNombre.Text + "/" + txtTelefono.Text + "/" + txtCorreo.Text + "/" + txtDomicilio.Text));
            agenda.Flush();
            treeAgenda.SelectedNode.Nodes.Add(txtTelefono.Text, txtNombre.Text, txtCorreo.Text, txtDomicilio.Text);
            dataGridView1.Rows.Add(txtNombre.Text, txtTelefono.Text, txtCorreo.Text, txtDomicilio.Text);
            txtNombre.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDomicilio.Clear();
            agenda.Close();
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            //saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    File.WriteAllText(saveFileDialog.FileName, txtNombre.Text+"/"+ txtTelefono.Text + "/" + txtCorreo.Text + "/" + txtDomicilio.Text);
            //}
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Eliminar"].Index)
            {
                DialogResult respuesta = MessageBox.Show("¿Estás seguro de que deseas eliminar este registro?", "Eliminar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                }
            }
        } 

        private void btneliminar_Click(object sender, EventArgs e)
        {
           
            TreeNode nodo = treeAgenda.Nodes[0].Nodes[0];
            treeAgenda.Nodes[0].Nodes.Remove(nodo);
           
                
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow filaSeleccionada = dataGridView1.SelectedRows[0];
                    dataGridView1.Rows.Remove(filaSeleccionada);
                }
                else
                {
                    MessageBox.Show("Debes seleccionar una fila para eliminar.");
                }
            
            string[] lineas = File.ReadAllLines("Contactos.txt");
            int lineaAEliminar = 0;
            List<string> nuevasLineas = new List<string>(lineas);
            nuevasLineas.RemoveAt(lineaAEliminar);
            File.WriteAllLines("Contactos.txt", nuevasLineas);
            MessageBox.Show("Se ha eliminado el archivo de texto.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            treeAgenda.SelectedNode.Nodes.Clear();
            dataGridView1.Rows.Clear();
        }
    }
}
    