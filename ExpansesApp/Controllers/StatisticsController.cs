using Microsoft.AspNetCore.Mvc;
using ExpensesApp.Repositories; // Poprawna przestrzeń nazw
using System;
using System.Collections.Generic;
using YourProjectNamespace.Repositories;

namespace ExpensesApp.Controllers // Poprawna przestrzeń nazw
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IExpenseRepository _expenseRepository;

        public StatisticsController(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        [HttpGet("total")]
        public IActionResult GetTotalExpenses()
        {
            try
            {
                var total = _expenseRepository.GetTotalExpenses();
                return Ok(new { Success = true, TotalExpenses = total });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Wystąpił błąd podczas obliczania sumy wydatków.", Error = ex.Message });
            }
        }

        [HttpGet("byCategory")]
        public IActionResult GetExpensesByCategory()
        {
            try
            {
                var expensesByCategory = _expenseRepository.GetExpensesByCategory();
                return Ok(new { Success = true, ExpensesByCategory = expensesByCategory });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Wystąpił błąd podczas pobierania wydatków według kategorii.", Error = ex.Message });
            }
        }

        [HttpGet("averagePerDay")]
        public IActionResult GetAverageExpensesPerDay()
        {
            try
            {
                var average = _expenseRepository.GetAverageExpensesPerDay();
                return Ok(new { Success = true, AverageExpensesPerDay = average });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Wystąpił błąd podczas obliczania średnich wydatków dziennych.", Error = ex.Message });
            }
        }
    }
}