using System.Collections.Generic;
using ExpensemanagmentAPi.Models;

namespace Expense_Tracker.Models;

public class DashboardData
{
    public DashboardData()
    {
        graphData = new List<Object>();
        chartData = new List<Object>();
    }
    public List<Object> graphData { get; set; }
    public List<Object> chartData { get; set; }
    public int totalExpens { set; get; }
    public int totalIncome { set; get; }
    public int balance { set; get; }
    public User user { get; set; }
}