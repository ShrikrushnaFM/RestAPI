using System.Data;
using WebApplication1.Models;
using Dapper;
using System.Data.SqlClient;

namespace WebApplication1.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnection _dbConnection;

        public EmployeeRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Employeedata>> GetAllEmployeesAsync()
        {
            var query = "SELECT * FROM EmployeesData";

            try
            {
                var employees = await _dbConnection.QueryAsync<Employeedata>(query);

                return employees;
            }
            catch (SqlException sqlEx)
            {
                Console.Error.WriteLine($"SQL error occurred: {sqlEx.Message}");
                // Rethrow or handle the exception as needed
                throw new ApplicationException("An error occurred while retrieving employee details. Please try again later.", sqlEx);

            }
            // Use Dapper to execute the query and map the results to the Employeedata model
            
        }

        public async Task<int> AddEmployeeAsync(Employeedata employee)
        {
            var sql = @"
            INSERT INTO EmployeesData (FirstName, LastName, Email, Age, MobileNo, Designation, Domain) 
            VALUES (@FirstName, @LastName, @Email, @Age, @MobileNo, @Designation, @Domain);
            SELECT CAST(SCOPE_IDENTITY() as int);";  // Retrieve the generated EmployeeId

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", employee.FirstName);
            parameters.Add("LastName", employee.LastName);
            parameters.Add("Email", employee.Email);
            parameters.Add("Age", employee.Age);
            parameters.Add("MobileNo", employee.MobileNo);
            parameters.Add("Designation", employee.Designation);
            parameters.Add("Domain", employee.Domain);

            // Execute the SQL query and retrieve the new EmployeeId
            var employeeId = await _dbConnection.QuerySingleAsync<int>(sql, parameters);
            return employeeId;
        }
    }
}
