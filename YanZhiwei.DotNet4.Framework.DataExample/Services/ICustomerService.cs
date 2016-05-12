using System.Collections.Generic;
using YanZhiwei.DotNet4.Framework.Data.Example.Contract;

namespace YanZhiwei.DotNet4.Framework.Data.Example.Services
{
    internal interface ICustomerService
    {
        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="user">The user.</param>
        void DeleteCustomer(Customer user);

        /// <summary>
        /// Gets the user by id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>User.</returns>
        Customer GetCustomerById(int userId);

        /// <summary>
        /// Inserts the user.
        /// </summary>
        /// <param name="user">The user.</param>
        void InsertCustomer(Customer user);

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        void UpdateCustomer(Customer user);

        /// <summary>
        /// Get Users by identifiers
        /// </summary>
        /// <param name="userIds">User identifiers</param>
        /// <returns>Users</returns>
        IList<Customer> GetCustomerByIds(int[] userIds);
    }
}