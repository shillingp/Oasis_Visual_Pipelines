﻿namespace Oasis_Visual_Pipelines.Enums
{
    [Flags]
    public enum BlockOperationType
    {
        None = 0,
        Number = 1,
        Text = 2,
        DataTable = 4,
        Array = 8,
        DateTime = 16,
        Boolean = 32,
    }
}
