﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Stock;
using Excepciones;

namespace Formularios
{
    public partial class frmAltaGorrito : Form
    {
        public Colonia catalinas;
        public Gorrito ingresante;
        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public frmAltaGorrito()
        {
            InitializeComponent();
        }
        /// <summary>
        /// COnstructor un parámetro que recibe una colonia.
        /// </summary>
        /// <param name="catalinas"></param>
        public frmAltaGorrito(Colonia catalinas) : this()
        {
            this.catalinas = catalinas;
        }

        /// <summary>
        /// Carga los combobox. Hardcodeo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAltaGorrito_Load(object sender, EventArgs e)
        {
            this.cmbColores.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach(EColores color in Enum.GetValues(typeof(EColores)))
            {
                this.cmbColores.Items.Add(color.ToString());
            }

        }
        /// <summary>
        /// Acepta el alta del gorrito. Obtiene sus datos desde formulario, agrega el gorrito a la 
        /// colonia.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntAceptar_Click(object sender, EventArgs e)
        {

            EColores color = (EColores)this.cmbColores.SelectedIndex;

            try
            {
                double precio = Validaciones.Validar.ValidarSoloNumeros(this.textBoxPrecio.Text);
                ingresante = new Gorrito(color, precio);
                this.catalinas.AumentarStock(this.catalinas, ingresante, 1);
                this.DialogResult = DialogResult.OK;
            }

            catch(ValidacionIncorrectaException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}