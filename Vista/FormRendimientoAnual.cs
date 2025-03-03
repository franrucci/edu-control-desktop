﻿using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Controladora;

namespace Vista
{
    public partial class FormRendimientoAnual : Form
    {
        Alumno alumno;
        public FormRendimientoAnual(Alumno alumno1)
        {
            InitializeComponent();
            alumno = alumno1;
            CargarGrafico();
        }

        public void CargarGrafico()
        {
            var cicloAcademico = ControladoraCiclosAcademicos.Instancia.ObtenerCicloAcademico(alumno.CicloAcademicoId);
            var boletinAlumno = ControladoraBoletines.Instancia.RecuperarBoletinAlumno(alumno, cicloAcademico.Año);
            var boletinCompleto = ControladoraBoletines.Instancia.RecuperarBoletinCompleto(boletinAlumno);

            // Creo el modelo para el gráfico
            var plotModel = new PlotModel { Title = "Promedio de cada Trimestre" };

            // Configuro la serie de barras
            var barSeries = new BarSeries
            {
                Title = "Promedios",
                FillColor = OxyColors.SkyBlue,
                LabelFormatString = "{0:F1}"
            };

            // Agrego los promedios del boletín
            barSeries.Items.Add(new BarItem { Value = (double)boletinCompleto.PromedioTrimestre1 }); // Trimestre 1
            barSeries.Items.Add(new BarItem { Value = (double)boletinCompleto.PromedioTrimestre2 }); // Trimestre 2
            barSeries.Items.Add(new BarItem { Value = (double)boletinCompleto.PromedioTrimestre3 }); // Trimestre 3

            // Agrego la serie al modelo
            plotModel.Series.Add(barSeries);

            // Agrego etiquetas a cada trimestre en el eje Y
            plotModel.Axes.Add(new OxyPlot.Axes.CategoryAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Left, // Eje Y en la parte izquierda
                Key = "Trimestres",
                ItemsSource = new[] { "Trimestre 1", "Trimestre 2", "Trimestre 3" }
            });

            // Agrego el eje X para los valores de promedio
            plotModel.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Bottom, // Eje X en la parte inferior
                Minimum = 0, // Valor mínimo en el eje X
                Maximum = 10, // Valor máximo en el eje X
                Title = "Promedio"
            });

            // Creo el control de gráfico y le asigno el modelo
            var plotView = new PlotView
            {
                Model = plotModel,
                Size = new Size(700, 400), // Tamaño fijo
                Location = new Point(10, 10) // Posición fija
            };

            var controller = new PlotController();
            controller.UnbindAll(); // Esto deshabilita todas las interacciones
            plotView.Controller = controller;

            this.Controls.Add(plotView);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
