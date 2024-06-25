using CommunityToolkit.Mvvm.Input;
using Microsoft.Data.SqlClient;
using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Dialogs;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Functions;
using System.Data;
using System.Windows.Input;

namespace Oasis_Visual_Pipelines.Operations.Sources.DataTables
{
    [BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGrouping.Sources)]
    public class SQLDataTableSourceBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 0;
        public override string OperationTitle => "SQL Connection";

        private readonly SQLConnectionSettingsDialog settingsDialog = new SQLConnectionSettingsDialog();
        private DataTable? FetchedDataTable = null;

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            if (FetchedDataTable is not null)
                return new BlockOperationResult(FetchedDataTable);

            return BlockOperationResult.NullOperation;
        }

        public ICommand UpdateConnectionSettingsCommand => new RelayCommand(async () =>
        {
            bool? closeStateResult = (bool?)await DialogHostFunctions.CreateAndShowDialog(settingsDialog, null, true);
            if (closeStateResult != true) return;

            string authenticationString = "Authentication=" + settingsDialog.AuthenticationMethod switch
            {
                Authentication.None => "None",
                Authentication.Default => "Active Directory Default",
                Authentication.UsernamePassword => "Active Directory Password",
                Authentication.Integrated => "Active Directory Integrated",
                Authentication.Interactive => "Active Directory Interactive",
                _ => throw new NotImplementedException(),
            };

            string tableName = settingsDialog.TableName;
            string connectionString = string.Join(';',
                $"Server={settingsDialog.ServerString}",
                $"Database={settingsDialog.DatabaseName}",
                authenticationString,
                "Encrypt=True"
            );

            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM [{tableName}]", connection))
                using (SqlCommand command = sqlCommand)
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                {
                    DataTable sqlTable = new DataTable();
                    dataAdapter.Fill(sqlTable);
                    FetchedDataTable = sqlTable;
                }
            }
            catch
            {
                FetchedDataTable = null;
            }
        });
    }
}
