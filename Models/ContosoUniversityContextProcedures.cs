﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;
using week1Homework_LinChin.Models.Models;

namespace week1Homework_LinChin.Models.Models
{
    public partial class ContosoUniversityContextProcedures
    {
        private readonly ContosoUniversityContext _context;

        public ContosoUniversityContextProcedures(ContosoUniversityContext context)
        {
            _context = context;
        }

        public async Task<int> Department_Delete(int? DepartmentID, byte[] RowVersion_Original, OutputParameter<int> returnValue = null)
        {
            var parameterDepartmentID = new SqlParameter
            {
                ParameterName = "DepartmentID",
                Value = DepartmentID ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var parameterRowVersion_Original = new SqlParameter
            {
                ParameterName = "RowVersion_Original",
                Value = RowVersion_Original ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Timestamp,
            };

            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var _ = await _context.Database.ExecuteSqlRawAsync("EXEC @returnValue = [dbo].[Department_Delete] @DepartmentID, @RowVersion_Original", parameterDepartmentID, parameterRowVersion_Original, parameterreturnValue);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public async Task<Department_InsertResult[]> Department_Insert(string Name, decimal? Budget, DateTime? StartDate, int? InstructorID, OutputParameter<int> returnValue = null)
        {
            var parameterName = new SqlParameter
            {
                ParameterName = "Name",
                Size = 100,
                Value = Name ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.NVarChar,
            };

            var parameterBudget = new SqlParameter
            {
                ParameterName = "Budget",
                Value = Budget ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Money,
            };

            var parameterStartDate = new SqlParameter
            {
                ParameterName = "StartDate",
                Value = StartDate ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.DateTime,
            };

            var parameterInstructorID = new SqlParameter
            {
                ParameterName = "InstructorID",
                Value = InstructorID ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var _ = await _context.SqlQuery<Department_InsertResult>("EXEC @returnValue = [dbo].[Department_Insert] @Name, @Budget, @StartDate, @InstructorID", parameterName, parameterBudget, parameterStartDate, parameterInstructorID, parameterreturnValue);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public async Task<Department_UpdateResult[]> Department_Update(int? DepartmentID, string Name, decimal? Budget, DateTime? StartDate, int? InstructorID, byte[] RowVersion_Original, OutputParameter<int> returnValue = null)
        {
            var parameterDepartmentID = new SqlParameter
            {
                ParameterName = "DepartmentID",
                Value = DepartmentID ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var parameterName = new SqlParameter
            {
                ParameterName = "Name",
                Size = 100,
                Value = Name ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.NVarChar,
            };

            var parameterBudget = new SqlParameter
            {
                ParameterName = "Budget",
                Value = Budget ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Money,
            };

            var parameterStartDate = new SqlParameter
            {
                ParameterName = "StartDate",
                Value = StartDate ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.DateTime,
            };

            var parameterInstructorID = new SqlParameter
            {
                ParameterName = "InstructorID",
                Value = InstructorID ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var parameterRowVersion_Original = new SqlParameter
            {
                ParameterName = "RowVersion_Original",
                Value = RowVersion_Original ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Timestamp,
            };

            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var _ = await _context.SqlQuery<Department_UpdateResult>("EXEC @returnValue = [dbo].[Department_Update] @DepartmentID, @Name, @Budget, @StartDate, @InstructorID, @RowVersion_Original", parameterDepartmentID, parameterName, parameterBudget, parameterStartDate, parameterInstructorID, parameterRowVersion_Original, parameterreturnValue);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }
    }
}
