using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assesment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Assesment.ViewModel;
namespace Assesment.Controllers
{
    
    public class TasksController : Controller
    {
        private readonly TaskManagerContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public TasksController(TaskManagerContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index()
        {
              return _context.Tasks != null ? 
                          View(await _context.Tasks.ToListAsync()) :
                          Problem("Entity set 'TaskManagerContext.Tasks'  is null.");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }
        [HttpGet]
        
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Tasks tasks, IFormFile MaterialPath)
        {
            tasks.TaskCreatedBy = User.Identity?.Name;
            tasks.TaskCreatedDate = DateTime.Now;
            if (MaterialPath != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "materials");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + MaterialPath.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await MaterialPath.CopyToAsync(fileStream);
                }
                tasks.Material = "/materials/" + uniqueFileName;
            }

            if (ModelState.IsValid)
            {
                
                _context.Add(tasks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tasks);
        }

        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return NotFound();
            }
            ViewBag.MaterialPath = tasks.Material;
            var viewModel = new TaskVM
            {
                ID = tasks.ID,
                Title = tasks.Title,
                Description = tasks.Description,
                DueDate = tasks.DueDate,
                AttachmentsType = tasks.AttachmentsType,
                TaskCreatedDate = tasks.TaskCreatedDate,
                TaskCreatedBy = tasks.TaskCreatedBy,
                Material = tasks.Material
            };

            return View(viewModel);
        }

     
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,DueDate,Material,AttachmentsType,TaskCreatedDate,TaskCreatedBy")] TaskVM viewModel, IFormFile MaterialPath= null)
        {
            if (id != viewModel.ID)
            {
                return NotFound();
            }
            var existingTask = await _context.Tasks.FindAsync(viewModel.ID);
            existingTask.Title = viewModel.Title;
            existingTask.Description = viewModel.Description;
            existingTask.DueDate = viewModel.DueDate;
            existingTask.AttachmentsType = viewModel.AttachmentsType;
            existingTask.TaskCreatedDate = viewModel.TaskCreatedDate;
            existingTask.TaskCreatedBy = viewModel.TaskCreatedBy;

            if (MaterialPath != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "materials");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + MaterialPath.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await MaterialPath.CopyToAsync(fileStream);
                }
                viewModel.Material = "/materials/" + uniqueFileName;
                existingTask.Material = "/materials/" + uniqueFileName;
            }
         else
            {
                viewModel.Material = existingTask.Material;
       }
            if (ModelState.IsValid)
            {
                try
                {
                    if (!_context.Tasks.Local.Any(t => t.ID == existingTask.ID))
                    {
                        _context.Attach(existingTask);
                    }

                    _context.Entry(existingTask).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TasksExists(viewModel.ID))
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
            return View(viewModel);
        }

     
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tasks == null)
            {
                return Problem("Entity set 'TaskManagerContext.Tasks'  is null.");
            }
            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks != null)
            {
                _context.Tasks.Remove(tasks);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TasksExists(int id)
        {
          return (_context.Tasks?.Any(e => e.ID == id)).GetValueOrDefault();
        }

    }
}
