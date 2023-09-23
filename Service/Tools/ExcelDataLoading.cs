using Data.Entity;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Service.Tools
{
    public class ExcelDataLoading
    {
        public void generateTemplate()
        {
            SLDocument sl = null;
            try
            {
                sl = new SLDocument();

                sl.SetCellValue(1, 1, "Numero de Arma");
                sl.SetCellValue(1, 2, "Nombre de arma");
                sl.SetCellValue(1, 3, "Descripción");

                sl.AutoFitColumn(1);
                sl.AutoFitColumn(2);
                sl.AutoFitColumn(3);

                DateTime currentDate = DateTime.Now;

                string fechaFormateada = currentDate.ToString("yyyyMMddhhmmss");

                string fileName = $@"C:\Users\Brayan\Downloads\WeaponsNoValidos_{fechaFormateada}.xlsx";

                sl.SaveAs(fileName);

                Process.Start(fileName);

                MessageBox.Show("El template de Excel se ha generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el template de Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void generateReport(List<Weapon> weapons)
        {
            try
            {
                SLDocument newSL = new SLDocument();

                newSL.SetCellValue("A1", "Numero de arma");
                newSL.SetCellValue("B1", "Nombre de arma");
                newSL.SetCellValue("C1", "Description");
                newSL.SetCellValue("D1", "Error");

                int rowIndex = 2;
                foreach (var weapon in weapons)
                {
                    newSL.SetCellValue("A" + rowIndex, weapon.WeaponNumber);
                    newSL.SetCellValue("B" + rowIndex, weapon.WeaponName);
                    newSL.SetCellValue("C" + rowIndex, weapon.Description);
                    newSL.SetCellValue("D" + rowIndex, "El numero de arma ya esta en la base de datos");
                    rowIndex++;
                }
                newSL.AutoFitColumn(1);
                newSL.AutoFitColumn(2);
                newSL.AutoFitColumn(3);
                newSL.AutoFitColumn(4);

                DateTime currentDate = DateTime.Now;

                string fechaFormateada = currentDate.ToString("yyyyMMddhhmmss");

                string fileName = $@"C:\Users\Brayan\Downloads\WeaponsNoValidos_{fechaFormateada}.xlsx";

                newSL.SaveAs(fileName);

                Process.Start(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el template de Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<Weapon> uploadExcel(OpenFileDialog openFileDialog)
        {
            string filePath = openFileDialog.FileName;

            try
            {
                SLDocument sl = new SLDocument(filePath);
                var weaponList = new List<Weapon>();
                int fila = 2;

                while (!string.IsNullOrEmpty(sl.GetCellValueAsString(fila, 1)))
                {
                    var weapon = new Weapon();
                    weapon.WeaponNumber = sl.GetCellValueAsString(fila, 1);
                    weapon.WeaponName = sl.GetCellValueAsString(fila, 2);
                    weapon.Description = sl.GetCellValueAsString(fila, 3);

                    weaponList.Add(weapon);

                    fila++;
                }
                
                return weaponList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el archivo de Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }
    }
}
