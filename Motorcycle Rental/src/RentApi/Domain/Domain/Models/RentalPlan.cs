using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.Domain.Models
{
    public class RentalPlan
    {
        public int Days { get; }
        public decimal DailyCost { get; }

        private RentalPlan(int days, decimal dailyCost)
        {
            Days = days;
            DailyCost = dailyCost;
        }

        public decimal TotalCost => Days * DailyCost;

        public static readonly RentalPlan Plan7 = new(7, 30.00m);
        public static readonly RentalPlan Plan15 = new(15, 28.00m);
        public static readonly RentalPlan Plan30 = new(30, 22.00m);
        public static readonly RentalPlan Plan45 = new(45, 20.00m);
        public static readonly RentalPlan Plan50 = new(50, 18.00m);

        public static IEnumerable<RentalPlan> GetAll()
            => new[] { Plan7, Plan15, Plan30, Plan45, Plan50 };

        public static RentalPlan FromDays(int days)
            => GetAll().FirstOrDefault(p => p.Days == days)
               ?? throw new ArgumentException($"Plano de {days} dias não existe.");
    }
}
