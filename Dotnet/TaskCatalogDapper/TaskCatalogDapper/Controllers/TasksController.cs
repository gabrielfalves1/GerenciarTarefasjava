using Microsoft.AspNetCore.Mvc;
using TaskCatalogDapper.Database;
using TaskCatalogDapper.Models;
using TaskCatalogDapper.Repositories;

namespace TaskCatalogDapper.Controllers
{
    public class TasksController : Controller
    {
        private readonly TaskRepository _repository;

        public TasksController(DapperContext context)
        {
            _repository = new TaskRepository(context);
        }
        public async Task<IActionResult> Index()
        {
            var tasks = await _repository.GetAllAsync();
            return View(tasks);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(TaskItem task)
        {
            if (!ModelState.IsValid) return View(task);
            await _repository.CreateAsync(task);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            return task == null ? NotFound() : View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskItem task)
        {
            if (!ModelState.IsValid) return View(task);
            await _repository.UpdateAsync(task);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
