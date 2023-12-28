using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Oasis_Visual_Pipelines.Operations
{
    [AddINotifyPropertyChangedInterface]
    [BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGroup.Sources)]
    public class SQLDataTableSourceBlockDiagramOperation : IBlockDiagramOperation
    {
        public int MaxInputs => 0;
        public string OperationTitle => "SQL Connection";

        private DataTable? currentCachedTable;

        private string _connectionString = string.Empty;
        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                if (value == _connectionString) 
                    return;

                _connectionString = value;
                currentCachedTable = null;
            }
        }

        private string _tableName = string.Empty;
        public string TableName
        {
            get => _tableName;
            set
            {
                if (value == _tableName)
                    return;

                _tableName = value;
                currentCachedTable = null;
            }
        }

        public BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            if (currentCachedTable is null && !string.IsNullOrEmpty(ConnectionString) && !string.IsNullOrEmpty(TableName))
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand($"SELECT * FROM {TableName}", connection))
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                {
                    DataTable sqlTable = new DataTable();
                    dataAdapter.Fill(sqlTable);
                    currentCachedTable = sqlTable;
                };
            }

            return new BlockOperationResult(currentCachedTable);
        }
    }
}
