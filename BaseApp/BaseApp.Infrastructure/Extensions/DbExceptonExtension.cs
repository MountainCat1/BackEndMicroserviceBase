using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BaseApp.Infrastructure.Extensions;

public static class DbExceptonExtension
{
    public static bool IsDuplicateEntryViolation(this DbUpdateException ex)
    {
        const int duplicateUniqueColumnErrorCode = 2601; // This is the error code for a duplicate entry violation in SQL Server
        const int duplicatePrimaryKeyErrorCode = 2627 ; // This is the error code for a duplicate entry violation in SQL Server

        if (ex.InnerException is SqlException sqlException)
        {
            return sqlException.Number is duplicateUniqueColumnErrorCode or duplicatePrimaryKeyErrorCode;
        }

        return false;
    }
}