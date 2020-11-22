using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopMS
{
    class Employee
    {
        public int EmployeeId{ get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public float Salary { get; set; }
        public long PhoneNo { get; set; }
        public string EmailID { get; set; }
        public string Dob { get; set; }
        public long Uid { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
        public int Approval { get; set; }
        public string Role { get; set; }

        public Employee()
        {
            PhoneNo = -1;
            Uid = -1;
            Approval = 0;
        }
    }

    class Customer
    {
        public int CustomerID { get; set; }
        public string Name{ get; set; }
        public string Dob{ get; set; }
        public long PhoneNo{ get; set; }
        public string Address{ get; set; }
        public string Sex { get; set; }

        public Customer()
        {
            PhoneNo = -1;
        }
    }

    class Product 
    {
        public int ProductID{ get; set; }
        public string Name{ get; set; }
        public string Brand{ get; set; }
        public string Description{ get; set; }
        public int Quantity{ get; set; }
        public float CostPrice{ get; set; }
        public float SellPrice{ get; set; }
        public string PurchacedDate{ get; set; }
        public string MFGDate{ get; set; }
        public string ExpiryDate{ get; set; }
        public string Contents{ get; set; }
        public int DefenctiveItems{ get; set; }
        public string type { get; set; }
    }
    class Order
    {
        public int OrderID{ get; set; }
        public int ProductID{ get; set; }
        public int CustomerID{ get; set; }
        public int EmployeeID{ get; set; }
        public string Date { get; set; }
        public string time { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
    }
}
