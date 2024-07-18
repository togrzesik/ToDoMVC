using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ToDoListMvc.Controllers
{
    public class TasksController : Controller
    {
        private static List<TaskItem> tasks = new List<TaskItem>();
        private static int nextId = 1;

        public IActionResult Index()
        {
            return View(tasks);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TaskItem task)
        {
            task.Id = nextId++;
            tasks.Add(task);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound();

            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(TaskItem task)
        {
            var existingTask = tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existingTask == null)
                return NotFound();

            existingTask.Description = task.Description;
            existingTask.IsCompleted = task.IsCompleted;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound();

            tasks.Remove(task);
            return RedirectToAction(nameof(Index));
        }
    }
}
