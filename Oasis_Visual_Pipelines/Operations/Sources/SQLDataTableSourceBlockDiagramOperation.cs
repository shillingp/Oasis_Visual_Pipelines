using CommunityToolkit.Mvvm.Input;
using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Dialogs;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Functions;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Input;

namespace Oasis_Visual_Pipelines.Operations
{
    [AddINotifyPropertyChangedInterface]
    [BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGroup.Sources)]
    public class SQLDataTableSourceBlockDiagramOperation : IBlockDiagramOperation
    {
        public int MaxInputs => 0;
        public string OperationTitle => "SQL Connection";

        private readonly SQLConnectionSettingsDialog settingsDialog = new SQLConnectionSettingsDialog();
        private DataTable? FetchedDataTable = null;

        public BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            //if (currentCachedTable is null && !string.IsNullOrEmpty(ConnectionString) && !string.IsNullOrEmpty(TableName))
            //{
            //    using (SqlConnection connection = new SqlConnection(ConnectionString))
            //    using (SqlCommand command = new SqlCommand($"SELECT * FROM {TableName}", connection))
            //    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
            //    {
            //        DataTable sqlTable = new DataTable();
            //        dataAdapter.Fill(sqlTable);
            //        currentCachedTable = sqlTable;
            //    };
            //}

            //return new BlockOperationResult(currentCachedTable);

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
