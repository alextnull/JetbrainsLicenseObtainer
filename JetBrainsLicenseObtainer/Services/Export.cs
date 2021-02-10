using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace JetBrainsLicenseObtainer.Services
{
    public static class Export
    {
        public static void ToCsv<T, M>(List<T> modelList) where M: ClassMap
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Csv file (*.csv)|*.csv"
            };

            if (saveFileDialog.ShowDialog() == false)
                return;

            using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
            {
                using (CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<M>();
                    csv.WriteRecords(modelList);
                }
            }
        }
    }
}
