using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_ufficio
{
    abstract public class RestaurantEmployee
    {
        private int salary;
        public string name;
        protected int totalEarnings;

        public RestaurantEmployee(string name)
        {
            this.name = name;
            totalEarnings = 0;
        }
        public void setSalary(int salary)
        {
            this.salary = salary;
        }

        public virtual int receivePay()
        {
            totalEarnings += salary;
            return salary;
        }

        public int getTotalEarnings()
        {
            return totalEarnings;
        }
    }
}
