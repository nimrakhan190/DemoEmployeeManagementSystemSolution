﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class EmployeeRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Employee>
    {
        public async Task<GeneralResponse> DeleteById(int  id)
        {
            var item = await appDbContext.Employees.FindAsync(id);
            if (item is null) return NotFound();

            appDbContext.Employees.Remove(item);
            await Commit();
            return Success();
        }

        public async Task<List<Employee>> GetAll()
        {
            Console.WriteLine("DEBUG: Starting to fetch employees...");

            var employees = await appDbContext.Employees
               .AsNoTracking()
               .Include(t => t.Town)
               .ThenInclude(b => b.City)
               .ThenInclude(c => c.Country)
               .Include(b => b.Branch)
               .ThenInclude(d => d.Department)
               .ThenInclude(gd => gd.GeneralDepartment)
               .ToListAsync();

            Console.WriteLine($"DEBUG: Found {employees.Count} employees in database");
            if (employees.Count > 0)
            {
                Console.WriteLine($"DEBUG: First employee - {employees[0].Name}, ID: {employees[0].Id}");
            }

            return employees;
        }
        public async Task<Employee> GetById(int id)
        {
            var employee = await appDbContext.Employees
                .Include(t => t.Town)
                .ThenInclude(b => b.City)
                .ThenInclude(c => c.Country)
                .Include(b => b.Branch)
                .ThenInclude(d => d.Department)
                .ThenInclude(gd => gd.GeneralDepartment).FirstOrDefaultAsync(ei => ei.Id == id)!;
                 return employee!;
        }

        public async Task<GeneralResponse> Insert(Employee item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Employee with this name already exists");
            appDbContext.Employees.Add(item); // Add this line
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Employee employee)
        {
            var findUser = await appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == employee.Id);
            if (findUser is null) return new GeneralResponse(false, "Sorry employee not found");

            findUser.Name = employee.Name;
            findUser.Other = employee.Other;
            findUser.Address = employee.Address;
            findUser.TelephoneNumber = employee.TelephoneNumber;
            findUser.BranchId = employee.BranchId;
            findUser.TownId = employee.TownId;
            findUser.CivilId = employee.CivilId;
            findUser.FileNumber = employee.FileNumber;
            findUser.JobName = employee.JobName;
            findUser.Photo = employee.Photo;
            await appDbContext.SaveChangesAsync();
            await Commit();
            return Success();
        }



        private async Task Commit() => await appDbContext.SaveChangesAsync();
        private static GeneralResponse NotFound() => new(false, "Sorry branch not found");

        private static GeneralResponse Success() => new(true, "Process completed");

        private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext.Employees.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return item is null ? true : false;

                
        }

    }
    
}
