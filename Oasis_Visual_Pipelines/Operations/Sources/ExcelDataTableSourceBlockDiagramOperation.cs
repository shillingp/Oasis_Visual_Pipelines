using CommunityToolkit.Mvvm.Input;
using ExcelDataReader;
using Microsoft.Win32;
using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;
using System.Data;
using System.IO;
using System.Windows.Input;

namespace Oasis_Visual_Pipelines.Operations
{
    [AddINotifyPropertyChangedInterface]
    [BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGroup.Sources)]
    public class ExcelDataTableSourceBlockDiagramOperation : IBlockDiagramOperation
    {
        public int MaxInputs => 0;
        public string OperationTitle => "Excel Data Source";

        private string _sourceFilePath = string.Empty;
        public string SourceFilePath
        {
            get { return _sourceFilePath; }
            set
            {
                if (value == _sourceFilePath)
                    return;

                _sourceFilePath = value;
                CachedTable = null;
            }
        }

        public DataTable? CachedTable { get; set; }

        public BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            CachedTable ??= ImportExcelToDataTable();

            return new BlockOperationResult(CachedTable);
        }

        private DataTable ImportExcelToDataTable()
        {
            if (string.IsNullOrEmpty(SourceFilePath))
                return new DataTable();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (FileStream stream = File.Open(SourceFilePath, FileMode.Open, FileAccess.Read))
            using (IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(stream))
            {
                DataSet dataset = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true,
                    }
                });
                return dataset.Tables[0];
            }
        }

        public ICommand SelectFilePathCommand => new RelayCommand(() =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".xlsx",
                Filter = "Excel Documents (.xlsx)|*.xlsx"
            };

            if (openFileDialog.ShowDialog() != true)
                return;

            SourceFilePath = openFileDialog.FileName;
        });
    }
}
