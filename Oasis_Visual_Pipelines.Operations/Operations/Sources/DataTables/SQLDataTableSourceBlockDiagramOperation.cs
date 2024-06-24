using CommunityToolkit.Mvvm.Input;
using Microsoft.Data.SqlClient;
using Oasis_Visual_Pipelines.Operations.Attributes;
using Oasis_Visual_Pipelines.Operations.Classes;
using Oasis_Visual_Pipelines.Dialogs;
using Oasis_Visual_Pipelines.Operations.Enums;
using Oasis_Visual_Pipelines.Functions;
using System.Data;
using System.Windows.Input;

namespace Oasis_Visual_Pipelines.Operations.Sources.DataTables
{
    [BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGroup.Sources)]
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

            string authenticationString = settingsDialog.AuthenticationMethod switch
            {
                Authentication.None => "",
                Authentication.Default => "Authentication=Active Directory Default",
                Authentication.UsernamePassword => "Authentication=Active Directory Password",
                Authentication.Integrated => "Authentication=Active Directory Integrated",
                Authentication.Interactive => "Authentication=Active Directory Interactive",
                _ => throw new NotImplementedException(),
            };

            string tableName = settingsDialog.TableName;
            string connectionString = string.Join(';', new[] {
                $"Server={settingsDialog.ServerString}",
                $"Database={settingsDialog.DatabaseName}",
                authenticationString,
                "Encrypt=True"
            }.Where(part => !string.IsNullOrEmpty(part)));

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand($"SELECT * FROM {tableName}", connection))
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                {
                    DataTable sqlTable = new DataTable();
                    dataAdapter.Fill(sqlTable);
                    FetchedDataTable = sqlTable;
                };
            }
            catch
            {
                FetchedDataTable = null;
            }
        });
    }
}
