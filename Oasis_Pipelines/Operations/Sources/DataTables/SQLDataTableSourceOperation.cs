using System.Data;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations.Sources.DataTables;


public sealed class SQLDataTableSourceOperation : BlockOperation
{

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