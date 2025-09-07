using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PayrollManagementSystem.Data;
using PayrollManagementSystem.Models;

namespace PayrollManagementSystem.Controllers
{
    public class FinancialRecordsController : Controller
    {
        private readonly PMS _context;

        public FinancialRecordsController(PMS context)
        {
            _context = context;
        }

        // GET: FinancialRecords
        public async Task<IActionResult> Index()
        {
            var pMS = _context.FinancialRecords.Include(f => f.Employee);
            return View(await pMS.ToListAsync());
        }

        // GET: FinancialRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financialRecord = await _context.FinancialRecords
                .Include(f => f.Employee)
                .FirstOrDefaultAsync(m => m.RecordID == id);
            if (financialRecord == null)
            {
                return NotFound();
            }

            return View(financialRecord);
        }

        // GET: FinancialRecords/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "Address");
            return View();
        }

        // POST: FinancialRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecordID,EmployeeID,RecordDate,Description,Amount,RecordType")] FinancialRecord financialRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(financialRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "Address", financialRecord.EmployeeID);
            return View(financialRecord);
        }

        // GET: FinancialRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financialRecord = await _context.FinancialRecords.FindAsync(id);
            if (financialRecord == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "Address", financialRecord.EmployeeID);
            return View(financialRecord);
        }

        // POST: FinancialRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecordID,EmployeeID,RecordDate,Description,Amount,RecordType")] FinancialRecord financialRecord)
        {
            if (id != financialRecord.RecordID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(financialRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinancialRecordExists(financialRecord.RecordID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "Address", financialRecord.EmployeeID);
            return View(financialRecord);
        }

        // GET: FinancialRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financialRecord = await _context.FinancialRecords
                .Include(f => f.Employee)
                .FirstOrDefaultAsync(m => m.RecordID == id);
            if (financialRecord == null)
            {
                return NotFound();
            }

            return View(financialRecord);
        }

        // POST: FinancialRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var financialRecord = await _context.FinancialRecords.FindAsync(id);
            if (financialRecord != null)
            {
                _context.FinancialRecords.Remove(financialRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FinancialRecordExists(int id)
        {
            return _context.FinancialRecords.Any(e => e.RecordID == id);
        }
    }
}
